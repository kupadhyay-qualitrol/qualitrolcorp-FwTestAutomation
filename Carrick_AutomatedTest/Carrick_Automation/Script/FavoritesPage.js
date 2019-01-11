/*This file contains methods and object variables related favorites*/

//Variables
var Topology_DefaultFavorites =Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.DAFAVuexpbarFavorites
//
//This function is used to click on All FR Trigger Favorites
function ClickOnAllFRTriggeredRecord()
{
  if(Topology_DefaultFavorites.Visible)
  {
    Topology_DefaultFavorites.ClickItem("Default Favorites", "All FR triggered records (DFR, DDR-C, DDR-T, DDR-L)")
    Log.Message("Clicked on All FR Triggered Record in Favorites")
    return true
    //TODO Need to further fine tune this code to handle condition
  }
  else
  {
    //TODO Write Code to Enable Default Favorites
  }
}
