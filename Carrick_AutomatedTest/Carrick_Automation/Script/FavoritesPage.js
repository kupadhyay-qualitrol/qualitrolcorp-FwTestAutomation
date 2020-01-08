/*This file contains methods and object variables related favorites*/

//Variables
var TOPOLOGY_DEFAULTFAVORITES =Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.DAFAVuexpbarFavorites
var TOOLS_FAVORITES = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.zUserControlBase_Toolbars_Dock_Area_Top
var PANE_FAVORITE = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4

//


//This function is used to click on All FR Trigger Favorites
function ClickOnAllFRTriggeredRecord()
{
  if(TOPOLOGY_DEFAULTFAVORITES.Visible)
  {
    TOPOLOGY_DEFAULTFAVORITES.ClickItem("Default Favorites", "All FR triggered records (DFR, DDR-C, DDR-T*")
    Log.Message("Clicked on All FR Triggered Record in Favorites")
    return true
    //TODO Need to further fine tune this code to handle condition
  }
  else
  {
    //TODO Write Code to Enable Default Favorites
  }
}

//This Function is used to check and add favorite
function clickOnAddFavorite()
{
  if(TOOLS_FAVORITES.Visible)
    {
    TOOLS_FAVORITES.ClickItem("utFavoriteToolbar|&New Favorite");
    Log.Message("Clicked on Add Favorite button")
    }
  else 
    {
    Log.Message("Favorite pane is not visible")
    }
}

//This function is used to click on PQ10min favorite
function clickOnPQ10Min()
{
  if(TOPOLOGY_DEFAULTFAVORITES.wItem("Default Favorites", "PQ10min") == "PQ10Min")
    {
    TOPOLOGY_DEFAULTFAVORITES.ClickItem("Default Favorites", "PQ10Min");
    Log.Message("Clicked on PQ10min favorite button under Default Favorite")
    }
  else
    {
    TOPOLOGY_DEFAULTFAVORITES.ClickItem("My Favorites", "PQ10Min");
    Log.Message("Clicked on PQ10min favorite button under My Favorite")
    }
}