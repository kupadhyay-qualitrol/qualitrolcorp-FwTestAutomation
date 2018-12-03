/*This file contains method and locators related to
* login page functionality of iq+.
*/

//Variables
var Edtbx_Username=Aliases.iQ_Plus.UserLogin.USRLOGINtxtUserName
var Edtbx_Password=Aliases.iQ_Plus.UserLogin.USRLOGINtxtPassword
var Btn_Login=Aliases.iQ_Plus.UserLogin.USRLOGINlnkLblLogin
//

//This method is used to Login the iQ+ application by providing username and password.
function Login(username,password)
{
  //Check whether user login box is launched or not.
  if(Edtbx_Username.Enabled)
  {
   Log.Message("User login box is available.")
   if(username!="" && password!="")
   {
    Edtbx_Username.SetText(username)
    Log.Message("Entered Login name:- " + username)
    Edtbx_Password.SetText(password)
    Log.Message("Entered Login Password:- " + password)
    Btn_Login.Click()
   }
   else
   {
     Log.Error("Either username or password is null")
   }
  }
  else
  {
    Log.Error("Application is not launched")
  }
}
