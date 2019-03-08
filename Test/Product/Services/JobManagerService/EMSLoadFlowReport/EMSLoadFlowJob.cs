
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
    public partial class JMSModel1 : AsyncJob
    {
		private void FillJMSModel1Properties(List<long> gids)
		{
			long mdc = 0;
			if (gids.Count <= 0) return;
			var iteratorId = GdaQuery.GetDescendentValues(0,
				new List<ModelCode> {

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
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLF]: Error occurred while collectingJMSModel1. Record data is null!");
						throw new ArgumentNullException("busnodeGids");
					}

				}
				count -= rds.Count;
			}
			GdaQuery.IteratorClose(iteratorId);
		}

		private void FillJMSModel2Properties(List<long> gids)
		{
			long mdc = 0;
			if (gids.Count <= 0) return;
			var iteratorId = GdaQuery.GetDescendentValues(0,
				new List<ModelCode> {

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
						DMSLogger.Log(DMSLogger.LogLevel.Error, "[EMSLF]: Error occurred while collectingJMSModel2. Record data is null!");
						throw new ArgumentNullException("busnodeGids");
					}

				}
				count -= rds.Count;
			}
			GdaQuery.IteratorClose(iteratorId);
		}


    }
}
