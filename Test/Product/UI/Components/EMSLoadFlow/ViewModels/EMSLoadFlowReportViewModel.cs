
//###############################################################
//														        #
//	This code was generated by a PowerFunctionsReportDSL tool.	#
//	Changes to this file may cause incorrect behavior	        #
//	and will be lost if the code is regenerated.		        #
//														        #
//###############################################################

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TelventDMS.Common.Components.Utils;
using TelventDMS.Common.DMS.Common;
using TelventDMS.UI.Components.CompositeCommon.Commands;
using TelventDMS.UI.Components.CompositeCommon.Converters;
using TelventDMS.UI.Components.CompositeCommon.Licence;
using TelventDMS.UI.Components.CompositeCommon.ViewModels;
using TelventDMS.UI.Components.CustomControls.HierarchyTreeViewControl.DataProvider;
using TelventDMS.UI.Components.CustomControls.ReportGeneralInfoControl;
using TelventDMS.UI.Components.CustomControls.ReportGeneralInfoControl.Models;
using TelventDMS.UI.Components.EMSLoadFlow.Models;
using TelventDMS.UI.Components.EMSLoadFlow.View;
using TelventDMS.UI.Components.FunctionCommon;
using TelventDMS.UI.Model.Electrical;
using TelventDMS.UI.ServiceProxies;


namespace TelventDMS.UI.Components.EMSLoadFlow.ViewModels
{
    public partial class EMSLoadFlowReportViewModel : ReportDocumentViewModel
    {
        #region Fields
        
		private EMSLoadFlowReportView reportView;
		private HierarchyType hierarchyType;
		private RelayCommand refreshCommand;

    
        #endregion Fields        

        #region Constructors

		public EMSLoadFlowReportViewModel(EMSLoadFlowReportView reportView) : base(reportView)
		{
			this.reportView = reportView;
			this.reportView.CommonHtv.HierarchyTreeViewRefreshed += HierarchyTreeViewRefreshed;
			TreeDataProvider = new HierarchyTreeDataProvider(new List<DMSType>() { DMSType.SOURCE }, HierarchyNetworkType.EMS);
			TreeDataProvider.ExpandTreeOnOpenEventHandler += TreeDataProvider_ExpandTreeOnReportEventHandler;
			hierarchyType = HierarchyType.Container;
			this.reportView.Loaded += ReportView_Loaded;
			SummaryUniqueName = EMSLoadFlowModuleCommands.ShowReport;
		}
       

        #endregion Constructors

        #region Properties
        


        #endregion Properties

        #region Methods

		protected internal void HierarchyTreeViewRefreshed()
		{
			DataProvider.Refresh();
		}


        #endregion Methods
    }
}
