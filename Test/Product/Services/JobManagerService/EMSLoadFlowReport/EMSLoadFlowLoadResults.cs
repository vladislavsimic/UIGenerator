
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
	/// <summary>
	/// Class containing the result for <see cref="EMSLoadFlowLoadRecord">.
	/// </summary>
    [DataContract]
    public partial class EMSLoadFlowLoadResults : EMSLoadFlowReportResult
    {
        #region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public EMSLoadFlowLoadResults()
		{
			EMSLoadFlowReportType = EMSLoadFlowReportType.Load;
		}
      

        #endregion Constructors

        #region Properties

		/// <summary>
		/// The collection containing EMSLoadFlowLoad records <see cref="EMSLoadFlowLoadRecord"/>
		/// </summary>
		[DataMember]
		public List<EMSLoadFlowLoadRecord> Records { get; set; }


        #endregion Properties
    }
}