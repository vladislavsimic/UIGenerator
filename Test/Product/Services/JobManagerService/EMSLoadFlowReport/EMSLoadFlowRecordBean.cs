
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
    public partial class EMSLoadFlowRecordBean : HierarchicalRecordData
    {
        #region Constructor

		public EMSLoadFlowRecordBean()
		{
		}


        #endregion Constructor

        #region Properties

		public MeasurementValueQuality Quality { get; set; }
		public float VoltageLevel { get; set; }

		public float Voltage { get; set; }

		public float Pinj { get; set; }

		public float Loading { get; set; }

		public float PEnd1 { get; set; }



        #endregion Properties

    }
}
