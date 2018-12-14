function Test1()
{
  var iQ_Plus;
  var userLogin;
  var textBox;
  var shellForm;
  var ultraTree;
  var ultraToolbarsDockArea;
  var directoryListView;
  var ultraGrid;
  NameMapping.Sys.Keys("[Hold][Win]d[Release]");
  TestedApps.iQ_Plus.Run(1, true);
  TestedApps.iQ_Plus.Run(1, true);
  iQ_Plus = Aliases.iQ_Plus;
  iQ_Plus.dlgIQ.Close();
  userLogin = iQ_Plus.UserLogin;
  textBox = userLogin.USRLOGINtxtPassword;
  textBox.Click(27, 4);
  textBox.Click(26, 9);
  userLogin.USRLOGINlnkLblLogin.ClickLink(0);
  shellForm = iQ_Plus.ShellForm;
  ultraTree = shellForm.windowDockingArea2.dockableWindow5.DeviceTopology.DeviceTopology.UserControlBase_Fill_Panel.TPGYutscTopologies.ultraTabSharedControlsPage1.panelTree.TPGYutvTopologyTree;
  ultraTree.ClickItemXY("All Devices|IDM+18|IND_DAU_50|Busbar_1_No Name 0 (Bb1F1)", 79, 9);
  ultraTree.ClickItemXY("All Devices|IDM+18|IND_DAU_50", 78, 14);
  ultraToolbarsDockArea = shellForm.zShellForm_Toolbars_Dock_Area_Top;
  ultraToolbarsDockArea.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|FR &Manual Trigger");
  iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManualTriggerView.MNLTRGgrpContainer.MNLTRGbtnOk.ClickButton();
  ultraToolbarsDockArea.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory|&DFR Directory ");
  directoryListView = iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView;
  directoryListView.DIRLSTVWbtnDownloadDataNow.ClickButton();
  directoryListView.DIRLSTVWbtnCancel.ClickButton();
  ultraGrid = shellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid;
  ultraGrid.ClickCellXY(0, "Rec. #(DFR)", 92, 3);
  ultraGrid.DblClickCellXY(0, "Rec. #(DFR)", 92, 6);
  iQ_Plus.MainForm.Close();
  ultraGrid.ClickCellRXY(0, "Rec. #(DFR)", 97, 3);
  ultraGrid.ClickCellXY(0, "Rec. #(DFR)", 96, 9);
  ultraGrid.ClickCellRXY(0, "Rec. #(DFR)", 96, 9);
  ultraGrid.ClickCellXY(0, "Rec. #(DFR)", 96, 9);
  Tables.Table1.Check();
  
  aqObject.CheckProperty(Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid, "wColumn(13)", cmpContains, "Rec. #(DFR)", false);
}

function Test2()
{
  var ultraGrid;
  ultraGrid = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid;
  ultraGrid.Drag(721, 378, 164, -17);
  ultraGrid.Drag(789, 380, 223, -17);
  ultraGrid.ClickCellXY(0, "Rec. #(DFR)", 90, 5);
  ultraGrid.ClickCellRXY(0, "Rec. #(DFR)", 90, 5);
  ultraGrid.ClickCellXY(0, "Rec. #(DFR)", 49, 7);
}