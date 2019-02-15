/*This file contains methods related to Cross Trigger*/
//USEUNIT AssertClass
//USEUNIT ConfigEditor_Comms_NetworkServices
//USEUNIT ConfigEditorPage
//USEUNIT CommonMethod

function Configure_CrossTrigger(DataSheetName,deviceIndex)
{
  //Step1. Configure Cross Trigger  
  AssertClass.IsTrue(ConfigEditorPage.ClickonNetworkServices(),"Clicked on Network Services")
  //Step2. Set UDP Port Number
  AssertClass.IsTrue(ConfigEditor_Comms_NetworkServices.SetUDPPort(CommonMethod.ReadDataFromExcel(DataSheetName,"UDPPortNumber"+(deviceIndex))),"Setting Port Number")
  //Step3. Set MaskID
  AssertClass.IsTrue(ConfigEditor_Comms_NetworkServices.SetGroupMaskID(CommonMethod.ReadDataFromExcel(DataSheetName,"Grp1_GroupMaskID"+(deviceIndex)),CommonMethod.ReadDataFromExcel(DataSheetName,"Grp2_GroupMaskID"+(deviceIndex)),CommonMethod.ReadDataFromExcel(DataSheetName,"Grp3_GroupMaskID"+(deviceIndex)),CommonMethod.ReadDataFromExcel(DataSheetName,"Grp4_GroupMaskID"+(deviceIndex))),"Setting Group Mask ID")
  //Step4. Set Compatibility
  AssertClass.IsTrue(ConfigEditor_Comms_NetworkServices.SetCompatibility(CommonMethod.ReadDataFromExcel(DataSheetName,"Compatibility"+(deviceIndex))),"Setting Compatibility")
}