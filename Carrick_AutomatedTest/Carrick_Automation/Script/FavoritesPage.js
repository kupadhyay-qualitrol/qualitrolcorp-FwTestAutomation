/*This file contains methods and object variables related favorites*/

//Variables
var Topology_DefaultFavorites =Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.DAFAVuexpbarFavorites
var TOOLS_FAVORITES = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.zUserControlBase_Toolbars_Dock_Area_Top
var PANE_FAVORITE = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4

//


//This function is used to click on All FR Trigger Favorites
function ClickOnAllFRTriggeredRecord()
{
  if(Topology_DefaultFavorites.Visible)
  {
    Topology_DefaultFavorites.ClickItem("Default Favorites", "All FR triggered records (DFR, DDR-C, DDR-T*")
    Log.Message("Clicked on All FR Triggered Record in Favorites")
    return true
    //TODO Need to further fine tune this code to handle condition
  }
  else
  {
    //TODO Write Code to Enable Default Favorites
  }
}

//This Function is used to Click on Add Favorite button on favorite section
function clickOnAddFavorite()
{
  if(PANE_FAVORITE.Visible)
  {
    TOOLS_FAVORITES.ClickItem("utFavoriteToolbar|&New Favorite");
    Log.Message("Clicked on Add Favorite button")
  }
}