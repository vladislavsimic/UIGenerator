
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
    public partial class JMSModel2Results : EMSLoadFlowReportResult
    {
        #region Constructors

		public JMSModel2Results()
		{
			ReportType = EMSLoadFlowReportType.JMSModel2;
		}
      

        #endregion Constructors

        #region Properties

		[DataMember]
		public List<JMSModel2Record> JMSModel2Records { get; set; }


        #endregion Properties
    }
}
