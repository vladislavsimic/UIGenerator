
//###############################################################
//														        #
//	This code was generated by a PowerFunctionsReportDSL tool.	#
//	Changes to this file may cause incorrect behavior	        #
//	and will be lost if the code is regenerated.		        #
//														        #
//###############################################################

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TelventDMS.Common.DMS.Common;
using TelventDMS.Common.Platform.Logger;
using TelventDMS.Common.Platform.Security.Config;
using TelventDMS.Common.Platform.Utils;


namespace TelventDMS.Services.JobManagerService.EMSLoadFlowReport
{
    public partial class EMSLoadFlowReportJob : AsyncJob
    {
        #region Declarations

		private HierarchyType hierarchyType;
		private EMSLoadFlowReportType reportType;
		private List<long> selectedRecords;
		private Dictionary<long, HierarchicalRecordData> hierarchicalRecordData;
		private Dictionary<long, MeasurementValueQuality> lfMeasureQuality;
		private HierarchicalFilter containerHierarchyTree;
		private HierarchicalFilter hierarchyTreeFilter;
		private HierarchyNetworkType hierarchyNetworkType;


        #endregion Declarations

        #region Properties
    
        [DataMember]
		public List<long> SelectedRecords
		{
			get
			{
				return selectedRecords;
			}

			set
			{
				selectedRecords = value ?? new List<long>();
			}
		}

        [DataMember]
		public EMSLoadFlowReportType ReportType
		{
			get { return reportType; }
			set { reportType = value; }
		}

        [DataMember]
		public HierarchyType HierarchyType
		{
			get { return hierarchyType; }
			set { hierarchyType = value; }
		}

		[DataMember]
		public HierarchyNetworkType HierarchyNetworkType
		{
			get { return hierarchyNetworkType; }
			set { hierarchyNetworkType = value; }
		}

		public override bool IsSelfTerminating
		{
			get
			{
				return false;
			}
		}

        #endregion Properties

        public override void Start()
		{
			Dictionary<long, ElectricalHierarchyData> parentChild = GDAHelper.GetContainerHeirarchy(GdaQuery, new List<ModelCode>(), HierarchyNetworkType);
			containerHierarchyTree = new HierarchicalFilter(parentChild, HierarchyNetworkType);
			lfMeasureQuality = new Dictionary<long, MeasurementValueQuality>();

			if (NetworkModelQuery == null)
			{
				NetworkModelQuery = JobService.OpenNetworkModelQuery();
			}

			JobResult result = CreateResult();
			ResultsReady(Guid, result);
		}

		public override void Modify(JobParam param)
		{
			if (param.GetType() == typeof(EMSLoadFlowReportJobParam))
			{
				ModifyJobInit(param);
			}
			else if (param.GetType() == typeof(RefreshParam))
			{
				RefreshHierarhy();
			}

			if (selectedRecords.Count > 0)
			{
				PrepareReportData();
			}

			JobResult result = CreateResult();
			ResultsReady(Guid, result);
		}

		private void ModifyJobInit(JobParam param)
		{
			EMSLoadFlowReportJobParam reportParam = (EMSLoadFlowReportJobParam)param;
			SelectedRecords = reportParam.SelectedRecords;
			reportType = reportParam.ReportType;
			hierarchyNetworkType = reportParam.HierarchyNetworkType;
		}

		private void RefreshHierarhy()
		{
			Dictionary<long, ElectricalHierarchyData> parentChild = GDAHelper.GetContainerHeirarchy(GdaQuery, new List<ModelCode>(), HierarchyNetworkType);
			containerHierarchyTree = new HierarchicalFilter(parentChild, HierarchyNetworkType);
		}

		public override void Terminate()
		{
		}

		/// <summary>
		/// Method is used for preparing input data.
		/// </summary>
		public void PrepareReportData()
		{
			long mdc = 0;
			int iteratorId;
			int count;

			if (containerHierarchyTree == null || containerHierarchyTree.AllNodes == null)
			{
				DMSLogger.Log(DMSLogger.LogLevel.DebugLog, "[reportParam]: PrepareReReportData all nodes in hierarchy tree are null.");
				return;
			}

			HierarchicalFilter hierTree = containerHierarchyTree;

			hierarchicalRecordData = new Dictionary<long, HierarchicalRecordData>(hierTree.AllNodes.Count);

			if (selectedRecords == null)
			{
				DMSLogger.Log(DMSLogger.LogLevel.DebugLog, "[EMSLoadFlowEMSLF]: PrepareReportData selected record is null.");
				return;
			}

			foreach (long nodeID in selectedRecords.Where(nodeID => !hierarchicalRecordData.ContainsKey(nodeID)))
			{
				hierarchicalRecordData.Add(nodeID, new EMSLoadFlowRecordBean());
			}

			// Get Names for all circuits
			List<long> nameSources = new List<long>(hierarchicalRecordData.Keys);

			if (nameSources.Count <= 0)
			{
				DMSLogger.Log(DMSLogger.LogLevel.DebugLog, "[EMSLoadFlow]: PrepareReportData selected records count is not possitive.");
				return;
			}
			try
			{
				mdc = 0;
				iteratorId = GdaQuery.GetDescendentValues(0, new List<ModelCode>(new[] { ModelCode.IDOBJ_NAME }), new List<Association>(), nameSources, new List<Association>(), ref mdc);
				count = GdaQuery.IteratorResourcesLeft(iteratorId);
				while (count > 0)
				{
					List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
					for (int i = 0; i < rds.Count; i++)
					{
						hierarchicalRecordData[rds[i].Id].Name = rds[i].GetProperty(ModelCode.IDOBJ_NAME).AsString();
					}

					count -= rds.Count;
				}

				GdaQuery.IteratorClose(iteratorId);

				mdc = 0;
				iteratorId = GdaQuery.GetDescendentValues(0, new List<ModelCode>(new[] { ModelCode.LFRESVAL_QUALITY }), new List<Association>(), nameSources, new List<Association>(), ref mdc);
				count = GdaQuery.IteratorResourcesLeft(iteratorId);
				while (count > 0)
				{
					List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
					for (int i = 0; i < rds.Count; i++)
					{
						EMSLoadFlowRecordBean temp = (EMSLoadFlowRecordBean)hierarchicalRecordData[rds[i].Id];
						temp.Quality = (MeasurementValueQuality)rds[i].GetProperty(ModelCode.LFRESVAL_QUALITY).AsInt();
					}

					count -= rds.Count;
				}

				GdaQuery.IteratorClose(iteratorId);
			}
			catch (Exception ex)
			{
				DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error occurred while preparing EMSLoadFlow report data.");
				DMSLogger.DumpNonFatalExceptionToLog(DMSLogger.LogLevel.DebugLog, ex);
			}
		}

		private JobResult CreateResult()
		{
			try
			{
				hierarchyTreeFilter = containerHierarchyTree;

				if (selectedRecords.Count > 0)
				{
					GetEMSLFQuality(selectedRecords);

					HierarchicalRecordData recordData;
					if (!hierarchicalRecordData.TryGetValue(selectedRecords[0], out recordData))
						return new EMSLoadFlowReportResult(reportType);

					EMSLoadFlowRecordBean data = recordData as EMSLoadFlowRecordBean;

					if ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(selectedRecords[0]) != DMSType.NETWORKINFO)
					{
						if (CheckIsLFStatusDisabled(data.Quality))
						{
							return new EMSLoadFlowReportResult(EMSLoadFlowReportType.NoResults);
						}
					}
				}
				else
				{
					return new EMSLoadFlowReportResult(reportType);
				}

				return CreateReportResults();
			}
			catch (Exception e)
			{
				DMSLogger.DumpNonFatalExceptionToLog(DMSLogger.LogLevel.DebugLog, e);
				return new JobError(e.Message);
			}
		}

		private JobResult CreateReportResults()
		{
			switch (reportType)
			{
				case EMSLoadFlowReportType.Node:
					{
						List<EMSLoadFlowNodeRecord> records = CreateEMSLoadFlowNodeRecords();
						EMSLoadFlowNodeResults results = new EMSLoadFlowNodeResults { Records = records };
						return results;
					}
				case EMSLoadFlowReportType.Section:
					{
						List<EMSLoadFlowSectionRecord> records = CreateEMSLoadFlowSectionRecords();
						EMSLoadFlowSectionResults results = new EMSLoadFlowSectionResults { Records = records };
						return results;
					}
				default:
					{
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error while creating results for EMSLoadFlow Report.EMSLoadFlowReportType {0} is unsupported.", reportType);
						return new JobError(string.Format("Error while creating results for EMSLoadFlow Report.EMSLoadFlowReportType {0} is unsupported.", reportType));
					}
			}
		}


		#region EMSLoadFlowNodeRecord

		private List<EMSLoadFlowNodeRecord> CreateEMSLoadFlowNodeRecords()
		{
			List<EMSLoadFlowNodeRecord> records = new List<EMSLoadFlowNodeRecord>();
			try
			{
				List<long> gids = GetElementsForSelectedCircuits(selectedRecords, ModelCode.BUSNODERESTA, ModelCode.TARESVAL_PARENTCIRCUIT);
				FillEMSLoadFlowNodeProperties(gids);
				GetElementsNames(gids);
				foreach (long id in selectedRecords)
				{
					EMSLoadFlowNodeRecord circuitRecord = CreateEMSLoadFlowNodeRecord(hierarchyTreeFilter, id);
					records.Add(circuitRecord);
					if (hierarchyTreeFilter.GetNodeByLid(id).Children == null) continue;
					foreach (HierarchicalFilterNode childNode in hierarchyTreeFilter.GetNodeByLid(id).Children)
					{
						if ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(childNode.Lid) != DMSType.BUSNODE) continue;
						MeasurementValueQuality lfQuality;
						if (!lfMeasureQuality.TryGetValue(id, out lfQuality) || CheckIsLFStatusDisabled(lfQuality))
						{
							continue;
						}
						records.Add(CreateEMSLoadFlowNodeRecord(hierarchyTreeFilter, childNode.Lid));
					}
				}
			}
			catch (Exception)
			{
				DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error occurred while creating EMSLoadFlowNode records.");
				throw;
			}
			return records;
		}

		private EMSLoadFlowNodeRecord CreateEMSLoadFlowNodeRecord(HierarchicalFilter hierarchyTree, long id)
		{
			EMSLoadFlowNodeRecord rec = new EMSLoadFlowNodeRecord();
			HierarchicalRecordData recordData;
			if (!hierarchicalRecordData.TryGetValue(id, out recordData))
			{
				DMSLogger.Log(DMSLogger.LogLevel.DebugLog, "[EMSLoadFlow]: Record { 0:X} does not exists in hierarchical record data dictionary.", id);
				return rec;
			}
			if (recordData.Name != null && !recordData.Name.Equals(string.Empty))
			{
				rec.Title = string.Format("{0}", recordData.Name);
			}
			else
			{
				rec.Title = string.Empty;
			}
			rec.Lid = id;
			rec.Level = (byte)hierarchyTreeFilter.GetNodeByLid(id).Level;
			EMSLoadFlowRecordBean data = recordData as EMSLoadFlowRecordBean;
			if (data != null && (DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(id) == DMSType.BUSNODE)
			{
				rec.VoltageLevel = data.VoltageLevel;
				rec.Voltage = data.Voltage;
				rec.Pinj = data.Pinj;
			}
			return rec;
		}

		private void FillEMSLoadFlowNodeProperties(List<long> gids)
		{
			long mdc = 0;
			if (gids.Count <= 0) return;
			var iteratorId = GdaQuery.GetDescendentValues(0,
				new List<ModelCode> {
					ModelCode.BUSNODERESLF_NOMINALVOLTAGE,
					ModelCode.BUSNODERESLF_LINETOLINE_VMIN,
					ModelCode.CONDUCTINGEQRESLF_P,

				}, new List<Association>(), gids, new List<Association>(), ref mdc);
			var count = GdaQuery.IteratorResourcesLeft(iteratorId);
			while (count > 0)
			{
				List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
				for (int i = 0; i < rds.Count; i++)
				{
					EMSLoadFlowRecordBean data = (hierarchicalRecordData[rds[i].Id] as EMSLoadFlowRecordBean);
					if (data == null)
					{
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error occurred while collecting EMSLoadFlowNode. Record data is null!");
						throw new ArgumentNullException("gids");
					}
					data.VoltageLevel = rds[i].GetProperty(ModelCode.BUSNODERESLF_NOMINALVOLTAGE).AsFloat();
					data.Voltage = rds[i].GetProperty(ModelCode.BUSNODERESLF_LINETOLINE_VMIN).AsFloat();
					data.Pinj = rds[i].GetProperty(ModelCode.CONDUCTINGEQRESLF_P).AsFloat();

				}
				count -= rds.Count;
			}
			GdaQuery.IteratorClose(iteratorId);
		}

		#endregion EMSLoadFlowNodeRecord


		#region EMSLoadFlowSectionRecord

		private List<EMSLoadFlowSectionRecord> CreateEMSLoadFlowSectionRecords()
		{
			List<EMSLoadFlowSectionRecord> records = new List<EMSLoadFlowSectionRecord>();
			try
			{
				List<long> gids = GetElementsForSelectedCircuits(selectedRecords, ModelCode.SECTIONRESULTSTA, ModelCode.TARESVAL_PARENTCIRCUIT);
				FillEMSLoadFlowSectionProperties(gids);
				GetElementsNames(gids);
				foreach (long id in selectedRecords)
				{
					EMSLoadFlowSectionRecord circuitRecord = CreateEMSLoadFlowSectionRecord(hierarchyTreeFilter, id);
					records.Add(circuitRecord);
					if (hierarchyTreeFilter.GetNodeByLid(id).Children == null) continue;
					foreach (HierarchicalFilterNode childNode in hierarchyTreeFilter.GetNodeByLid(id).Children)
					{
						if ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(childNode.Lid) != DMSType.SECTION) continue;
						MeasurementValueQuality lfQuality;
						if (!lfMeasureQuality.TryGetValue(id, out lfQuality) || CheckIsLFStatusDisabled(lfQuality))
						{
							continue;
						}
						records.Add(CreateEMSLoadFlowSectionRecord(hierarchyTreeFilter, childNode.Lid));
					}
				}
			}
			catch (Exception)
			{
				DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error occurred while creating EMSLoadFlowSection records.");
				throw;
			}
			return records;
		}

		private EMSLoadFlowSectionRecord CreateEMSLoadFlowSectionRecord(HierarchicalFilter hierarchyTree, long id)
		{
			EMSLoadFlowSectionRecord rec = new EMSLoadFlowSectionRecord();
			HierarchicalRecordData recordData;
			if (!hierarchicalRecordData.TryGetValue(id, out recordData))
			{
				DMSLogger.Log(DMSLogger.LogLevel.DebugLog, "[EMSLoadFlow]: Record { 0:X} does not exists in hierarchical record data dictionary.", id);
				return rec;
			}
			if (recordData.Name != null && !recordData.Name.Equals(string.Empty))
			{
				rec.Title = string.Format("{0}", recordData.Name);
			}
			else
			{
				rec.Title = string.Empty;
			}
			rec.Lid = id;
			rec.Level = (byte)hierarchyTreeFilter.GetNodeByLid(id).Level;
			EMSLoadFlowRecordBean data = recordData as EMSLoadFlowRecordBean;
			if (data != null && (DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(id) == DMSType.SECTION)
			{
				rec.Loading = data.Loading;
				rec.PEnd1 = data.PEnd1;
			}
			return rec;
		}

		private void FillEMSLoadFlowSectionProperties(List<long> gids)
		{
			long mdc = 0;
			if (gids.Count <= 0) return;
			var iteratorId = GdaQuery.GetDescendentValues(0,
				new List<ModelCode> {
					ModelCode.BRANCHRESLF_RELATIVELOAD,
					ModelCode.CONDUCTINGEQRESLF_P,

				}, new List<Association>(), gids, new List<Association>(), ref mdc);
			var count = GdaQuery.IteratorResourcesLeft(iteratorId);
			while (count > 0)
			{
				List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
				for (int i = 0; i < rds.Count; i++)
				{
					EMSLoadFlowRecordBean data = (hierarchicalRecordData[rds[i].Id] as EMSLoadFlowRecordBean);
					if (data == null)
					{
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error occurred while collecting EMSLoadFlowSection. Record data is null!");
						throw new ArgumentNullException("gids");
					}
					data.Loading = rds[i].GetProperty(ModelCode.BRANCHRESLF_RELATIVELOAD).AsFloat();
					data.PEnd1 = rds[i].GetProperty(ModelCode.CONDUCTINGEQRESLF_P).AsFloat();

				}
				count -= rds.Count;
			}
			GdaQuery.IteratorClose(iteratorId);
		}

		#endregion EMSLoadFlowSectionRecord




        /// <summary>
		/// Get names of elements specified by element id.
		/// </summary>
		/// <param name="elementIds">List of elements id's.</param>
		private void GetElementsNames(List<long> elementIds)
		{
			long mdc = 0;
			try
			{
				if (elementIds.Count <= 0) return;
				var iteratorId = GdaQuery.GetDescendentValues(0, new List<ModelCode> { ModelCode.IDOBJ_NAME }, new List<Association>(),
					elementIds, new List<Association>(), ref mdc);
				var count = GdaQuery.IteratorResourcesLeft(iteratorId);
				while (count > 0)
				{
					List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
					for (int i = 0; i < rds.Count; i++)
					{
						EMSLoadFlowRecordBean recordData = (hierarchicalRecordData[rds[i].Id] as EMSLoadFlowRecordBean);
						if (recordData != null)
						{
							recordData.Name = rds[i].GetProperty(ModelCode.IDOBJ_NAME).AsString();
						}
					}

					count -= rds.Count;
				}

				GdaQuery.IteratorClose(iteratorId);
			}
			catch (Exception)
			{
				DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLoadFlow]: Error occurred while collecting element names for EMSLoadFlow report data.");
				throw;
			}
		}

        /// <summary>
		/// Gets elements for selected circuits.
		/// </summary>
		/// <param name="circuits">List of circuits id's.</param>
		/// <param name="elementCode">Element model code.</param>
		/// <param name="propertyModelCode">Property model code.</param>
		/// <returns>List of elements.</returns>
		private List<long> GetElementsForSelectedCircuits(List<long> circuits, ModelCode elementCode, ModelCode propertyModelCode, bool useTADomain = true)
		{
			List<Pair<long, ElectricalHierarchyData>> childParent = new List<Pair<long, ElectricalHierarchyData>>();

			long mdc = 0;
			int iteratorId;
			List<long> result = new List<long>();
			if (circuits.Count == 0)
			{
				return result;
			}

			FilterParser filterParser = new FilterParser();

			string[] circuitsArray = new string[circuits.Count];
			for (int i = 0; i < circuits.Count; i++)
			{
				circuitsArray[i] = string.Format("{0}", circuits[i]);
			}

			string circuitsFilter = string.Join(",", circuitsArray);
            string filterFormat = useTADomain ? "{0} IN ({1}) AND TARESVAL_DOMAIN = Subtransmission" : "{0} IN ({1})";
            
            FilterNode fn = filterParser.CreateFromString(elementCode, string.Format(filterFormat, propertyModelCode, circuitsFilter));
			iteratorId = GdaQuery.GetFilteredExtentValues(0, elementCode, new List<ModelCode>() { propertyModelCode }, fn, ref mdc);
			int count = GdaQuery.IteratorResourcesLeft(iteratorId);
			while (count > 0)
			{
				List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
				foreach (ResourceDescription rd in rds)
				{
					if (rd.Id > 0)
					{
						result.Add(rd.Id);
						if (!hierarchicalRecordData.ContainsKey(rd.Id))
						{
							hierarchicalRecordData.Add(rd.Id, new EMSLoadFlowRecordBean());
						}

						if (!hierarchyTreeFilter.AllNodes.Keys.Contains(rd.Id))
						{
							ElectricalHierarchyData data = new ElectricalHierarchyData();
							data.Id = rd.Id;
							data.Code = propertyModelCode;
							data.ParentId = rd.GetProperty(propertyModelCode).AsLong();

							childParent.Add(new Pair<long, ElectricalHierarchyData>(data.Id, data));
						}
					}
				}

				count = count - rds.Count;
			}

			GdaQuery.IteratorClose(iteratorId);

			hierarchyTreeFilter.AddChildren(childParent);

			return result;
		}

        /// <summary>
		/// Method is used to collect all EMSLF Quality for circuit gids.
		/// </summary>
		/// <param name="circuitGids">List of circuit gids.</param>
		private void GetEMSLFQuality(List<long> circuitGids)
		{
			try
			{
				lfMeasureQuality = new Dictionary<long, MeasurementValueQuality>(circuitGids.Count);
				long mdc = 0;
				int iteratorId = 0;
				int count;

				if (circuitGids.Count > 0)
				{
					iteratorId = GdaQuery.GetDescendentValues(0, new List<ModelCode> { ModelCode.LFRESVAL_QUALITY }, new List<Association>(), circuitGids, new List<Association>(), ref mdc);
					count = GdaQuery.IteratorResourcesLeft(iteratorId);

					while (count > 0)
					{
						List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
						for (int i = 0; i < rds.Count; i++)
						{
							lfMeasureQuality.Add(circuitGids[i], (MeasurementValueQuality)(rds[i].Properties[0].AsInt()));
						}

						count -= rds.Count;
					}

					GdaQuery.IteratorClose(iteratorId);
				}
			}
			catch (Exception)
			{
				DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLF]: Error occurred while getting EMS LF quality.");
				throw;
			}
		}

		private bool CheckIsLFStatusDisabled(MeasurementValueQuality lfQuality)
		{
			if (((uint)lfQuality & (uint)MeasurementValueQuality.ValidityInvalid) == (uint)MeasurementValueQuality.ValidityInvalid &&
				((uint)lfQuality & (uint)ValueQualityLF.DisabledLF) == (uint)ValueQualityLF.DisabledLF)
			{
				return true;
			}

			return false;
		}
    }
}
