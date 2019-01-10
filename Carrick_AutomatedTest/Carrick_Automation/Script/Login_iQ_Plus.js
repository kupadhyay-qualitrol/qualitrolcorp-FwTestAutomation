/*This file contains methods related to Login/Logout iQ+*/

//USEUNIT CommonMethod
//USEUNIT AssertClass
//USEUNIT LoginPage

function Login_iQ()
{
   var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"
   AssertClass.IsTrue(CommonMethod.Launch_iQ_Plus(),"Launch iQ+ application")   
   LoginPage.Login(CommonMethod.ReadDataFromExcel(DataSheetName,"Username"),CommonMethod.ReadDataFromExcel(DataSheetName,"Password"))  
}
