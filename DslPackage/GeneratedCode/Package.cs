﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using VSShellInterop = global::Microsoft.VisualStudio.Shell.Interop;
using VSShell = global::Microsoft.VisualStudio.Shell;
using DslShell = global::Microsoft.VisualStudio.Modeling.Shell;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using System;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
	
namespace SchneiderElectricDMS.PowerFunctionsReportDSL
{
	/// <summary>
	/// This class implements the VS package that integrates this DSL into Visual Studio.
	/// </summary>
	[VSShell::DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\14.0")]
	[VSShell::PackageRegistration(RegisterUsing = VSShell::RegistrationMethod.Assembly, UseManagedResourcesOnly = true)]
	[VSShell::ProvideToolWindow(typeof(PowerFunctionsReportDSLExplorerToolWindow), MultiInstances = false, Style = VSShell::VsDockStyle.Tabbed, Orientation = VSShell::ToolWindowOrientation.Right, Window = "{3AE79031-E1BC-11D0-8F78-00A0C9110057}")]
	[VSShell::ProvideToolWindowVisibility(typeof(PowerFunctionsReportDSLExplorerToolWindow), Constants.PowerFunctionsReportDSLEditorFactoryId)]
	[VSShell::ProvideStaticToolboxGroup("@PowerFunctionsReportDSLToolboxTab;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", "SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab")]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@EnumToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.EnumToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"Enum", 
					"@EnumToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 0)]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@CommentToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.CommentToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"Comment", 
					"@CommentToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 1)]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@CommentRelationshipToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.CommentRelationshipToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"CommentRelationship", 
					"@CommentRelationshipToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 2)]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@TabToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.TabToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"Tab", 
					"@TabToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 3)]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@DataGridToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.DataGridToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"DataGrid", 
					"@DataGridToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 4)]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@TabTabReferenceToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.TabTabReferenceToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"TabTabReference", 
					"@TabTabReferenceToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 5)]
	[VSShell::ProvideStaticToolboxItem("SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxTab",
					"@TabDataGridReferenceToolboxItem;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					"SchneiderElectricDMS.PowerFunctionsReportDSL.TabDataGridReferenceToolboxItem", 
					"CF_TOOLBOXITEMCONTAINER,CF_TOOLBOXITEMCONTAINER_HASH,CF_TOOLBOXITEMCONTAINER_CONTENTS", 
					"TabDataGridReference", 
					"@TabDataGridReferenceToolboxBitmap;SchneiderElectricDMS.PowerFunctionsReportDSL.Dsl.dll", 
					0xff00ff,
					Index = 6)]
	[VSShell::ProvideEditorFactory(typeof(PowerFunctionsReportDSLEditorFactory), 103, TrustLevel = VSShellInterop::__VSEDITORTRUSTLEVEL.ETL_AlwaysTrusted)]
	[VSShell::ProvideEditorExtension(typeof(PowerFunctionsReportDSLEditorFactory), "." + Constants.DesignerFileExtension, 50)]
	[VSShell::ProvideEditorLogicalView(typeof(PowerFunctionsReportDSLEditorFactory), "{7651A702-06E5-11D1-8EBD-00A0C90F26EA}")] // Designer logical view GUID i.e. VSConstants.LOGVIEWID_Designer
	[DslShell::ProvideRelatedFile("." + Constants.DesignerFileExtension, Constants.DefaultDiagramExtension,
		ProjectSystem = DslShell::ProvideRelatedFileAttribute.CSharpProjectGuid,
		FileOptions = DslShell::RelatedFileType.FileName)]
	[DslShell::ProvideRelatedFile("." + Constants.DesignerFileExtension, Constants.DefaultDiagramExtension,
		ProjectSystem = DslShell::ProvideRelatedFileAttribute.VisualBasicProjectGuid,
		FileOptions = DslShell::RelatedFileType.FileName)]
	[DslShell::RegisterAsDslToolsEditor]
	[global::System.Runtime.InteropServices.ComVisible(true)]
	[DslShell::ProvideBindingPath]
	[DslShell::ProvideXmlEditorChooserBlockSxSWithXmlEditor(@"PowerFunctionsReportDSL", typeof(PowerFunctionsReportDSLEditorFactory))]

	internal abstract partial class PowerFunctionsReportDSLPackageBase : DslShell::ModelingPackage
	{
		protected global::SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxHelper toolboxHelper;	
		
		/// <summary>
		/// Initialization method called by the package base class when this package is loaded.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

			// Register the editor factory used to create the DSL editor.
			this.RegisterEditorFactory(new PowerFunctionsReportDSLEditorFactory(this));
			
			// Initialize the toolbox helper
			toolboxHelper = new global::SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLToolboxHelper(this);

			// Create the command set that handles menu commands provided by this package.
			PowerFunctionsReportDSLCommandSet commandSet = new PowerFunctionsReportDSLCommandSet(this);
			commandSet.Initialize();
			
			// Create the command set that handles cut/copy/paste commands provided by this package.
			PowerFunctionsReportDSLClipboardCommandSet clipboardCommandSet = new PowerFunctionsReportDSLClipboardCommandSet(this);
			clipboardCommandSet.Initialize();
			
			// Register the model explorer tool window for this DSL.
			this.AddToolWindow(typeof(PowerFunctionsReportDSLExplorerToolWindow));

			// Initialize Extension Registars
			// this is a partial method call
			this.InitializeExtensions();

			// Add dynamic toolbox items
			this.SetupDynamicToolbox();
		}

		/// <summary>
		/// Partial method to initialize ExtensionRegistrars (if any) in the DslPackage
		/// </summary>
		partial void InitializeExtensions();
		
		/// <summary>
		/// Returns any dynamic tool items for the designer
		/// </summary>
		/// <remarks>The default implementation is to return the list of items from the generated toolbox helper.</remarks>
		protected override global::System.Collections.Generic.IList<DslDesign::ModelingToolboxItem> CreateToolboxItems()
		{
			try
			{
				Debug.Assert(toolboxHelper != null, "Toolbox helper is not initialized");
				return toolboxHelper.CreateToolboxItems();
			}
			catch(global::System.Exception e)
			{
				global::System.Diagnostics.Debug.Fail("Exception thrown during toolbox item creation.  This may result in Package Load Failure:\r\n\r\n" + e);
				throw;
			}
		}
		
		
		/// <summary>
		/// Given a toolbox item "unique ID" and a data format identifier, returns the content of
		/// the data format. 
		/// </summary>
		/// <param name="itemId">The unique ToolboxItem to retrieve data for</param>
		/// <param name="format">The desired format of the resulting data</param>
		protected override object GetToolboxItemData(string itemId, DataFormats.Format format)
		{
			Debug.Assert(toolboxHelper != null, "Toolbox helper is not initialized");
		
			// Retrieve the specified ToolboxItem from the DSL
			return toolboxHelper.GetToolboxItemData(itemId, format);
		}
	}

}

//
// Package attributes which may need to change are placed on the partial class below, rather than in the main include file.
//
namespace SchneiderElectricDMS.PowerFunctionsReportDSL
{
	/// <summary>
	/// Double-derived class to allow easier code customization.
	/// </summary>
	[VSShell::ProvideMenuResource("1000.ctmenu", 1)]
	[VSShell::ProvideToolboxItems(1)]
	[global::Microsoft.VisualStudio.TextTemplating.VSHost.ProvideDirectiveProcessor(typeof(global::SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLDirectiveProcessor), global::SchneiderElectricDMS.PowerFunctionsReportDSL.PowerFunctionsReportDSLDirectiveProcessor.PowerFunctionsReportDSLDirectiveProcessorName, "A directive processor that provides access to PowerFunctionsReportDSL files")]
	[global::System.Runtime.InteropServices.Guid(Constants.PowerFunctionsReportDSLPackageId)]
	internal sealed partial class PowerFunctionsReportDSLPackage : PowerFunctionsReportDSLPackageBase
	{
	}
}