
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
using TelventDMS.Common.DMS.Common;
using System.Runtime.Serialization;


namespace TelventDMS.Services.JobManagerService.EMSLoadFlowReport
{
    [DataContract]
    public partial class EMSLoadFlowGenerator : AsyncJob
    {
		private void FillEMSLoadFlowGeneratorProperties(List<long> gids)
		{
			long mdc = 0;
			if (gids.Count <= 0) return;
			var iteratorId = GdaQuery.GetDescendentValues(0,
				new List<ModelCode> {
					ModelCode.,
					ModelCode.,
					ModelCode.,

				}, new List<Association>(), busnodeGids, new List<Association>(), ref mdc);
			var count = GdaQuery.IteratorResourcesLeft(iteratorId);
			while (count > 0)
			{
				List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
				for (int i = 0; i < rds.Count; i++)
				{
					EMSLoadFlowRecordBean data = (hierarhyRecordData[rds[i].Id] as EMSLoadFlowRecordBean)
					if (data == null)
					{
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLF]: Error occurred while collectingEMSLoadFlowGenerator. Record data is null!");
						throw new ArgumentNullException("busnodeGids");
					}
					data.P = rds[i].GetProperty(ModelCode.).AsFloat();
					data.Loading = rds[i].GetProperty(ModelCode.).AsFloat();
					data.Q = rds[i].GetProperty(ModelCode.).AsFloat();

				count -= rds.Count;
			}
			GdaQuery.IteratorClose(iteratorId);
		}

		private void FillEMSLoadFlowConsumerProperties(List<long> gids)
		{
			long mdc = 0;
			if (gids.Count <= 0) return;
			var iteratorId = GdaQuery.GetDescendentValues(0,
				new List<ModelCode> {
					ModelCode.,
					ModelCode.,

				}, new List<Association>(), busnodeGids, new List<Association>(), ref mdc);
			var count = GdaQuery.IteratorResourcesLeft(iteratorId);
			while (count > 0)
			{
				List<ResourceDescription> rds = GdaQuery.IteratorNext(50000, iteratorId);
				for (int i = 0; i < rds.Count; i++)
				{
					EMSLoadFlowRecordBean data = (hierarhyRecordData[rds[i].Id] as EMSLoadFlowRecordBean)
					if (data == null)
					{
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLF]: Error occurred while collectingEMSLoadFlowConsumer. Record data is null!");
						throw new ArgumentNullException("busnodeGids");
					}
					data.P = rds[i].GetProperty(ModelCode.).AsFloat();
					data.PowerFactor = rds[i].GetProperty(ModelCode.).AsFloat();

				count -= rds.Count;
			}
			GdaQuery.IteratorClose(iteratorId);
		}


    }
}
