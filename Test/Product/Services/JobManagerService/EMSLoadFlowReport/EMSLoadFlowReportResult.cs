
//###############################################################
//														        #
//	This code was generated by a PowerFunctionsReportDSL tool.	#
//	Changes to this file may cause incorrect behavior	        #
//	and will be lost if the code is regenerated.		        #
//														        #
//###############################################################

using System.Runtime.Serialization;


namespace TelventDMS.Services.JobManagerService.EMSLoadFlowReport
{

	[DataContract]
	[KnownType(typeof(DataGrid1Results))]
	[KnownType(typeof(DataGrid2Results))]

    public partial class EMSLoadFlowReportResult : JobResult
    {
        #region Constructors

		public EMSLoadFlowReportResult()
		{
		}


		public EMSLoadFlowReportResult(EMSLoadFlowReportType reportType)
		{
			EMSLoadFlowReportType = reportType;
		}


        #endregion Constructors

        #region Properties

		[DataMember]
		public EMSLoadFlowReportType EMSLoadFlowReportType { get; set; }


        #endregion Properties

    }
}
