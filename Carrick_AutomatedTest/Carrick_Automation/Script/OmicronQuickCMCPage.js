/*This file contains methods and objects related to Quick CMC Utility.
*/

//Variables
var Btn_Start =Aliases.QuickCMC.wndAfx.xtpBarTop.toolbarTheRibbon.buttonStart
//

//This function is used to click on Start Button on Quick CMC page
function ClickStart()
{
  if(Btn_Start.Exists)
  {
    Btn_Start.ClickButton()
    Log.Message("Clicked on Start Button on Quick CMC")
    return true
  }
  else
  {
    Log.Message("Button Start doesn't exists")
    return false
  }
}
