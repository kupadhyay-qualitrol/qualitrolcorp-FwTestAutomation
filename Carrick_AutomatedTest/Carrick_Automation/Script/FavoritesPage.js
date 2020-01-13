/*This file contains methods and object variables related favorites*/

//USEUNIT PQ_Methods
//USEUNIT Continuous_Recording_Customization_Page

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
  if(TOPOLOGY_DEFAULTFAVORITES.Visible)
  {
  TOPOLOGY_DEFAULTFAVORITES.ClickItem("Default Favorites", "PQ10Min");
  Log.Message("Clicked on PQ10min favorite button under"+ Favorite +"Favorite")
  return true
  }
  else
  {
  Log.Message("Favorite window is not visible")
  }
}

function findGroupName(favoriteName)
{
    var defaultFavoriteItemCount = DEFAULT_FAV.wItemCount("Default Favorites")
    var myFavoriteItemCount = DEFAULT_FAV.wItemCount("My Favorites")
    var defaultFavoriteItems = []
    var myFavoriteItems = []
    var groupName
    for (counter=0;counter<defaultFavoriteItemCount;counter++)
    {
     defaultFavoriteItems[counter]  = DEFAULT_FAV.wItem(0, counter)     
    } 
    if(defaultFavoriteItems.includes(favoriteName))
    {
        groupName = "Default Favorites"
        Log.Message("Favorite found in "+groupName)
        return groupName
    }       
    for (counter=0;counter<myFavoriteItemCount;counter++)
    {
      myFavoriteItems[counter] = DEFAULT_FAV.wItem(1, counter)
     
    }
     if(myFavoriteItems.includes(favoriteName))    
      {
        groupName = "My Favorites"
        Log.Message("Favorite found in "+groupName)
        return groupName       
      } 
    return groupName
}

function checkForPqFavorite(favoriteName)
{ 
  try 
  { 
      GROUPNAME = findGroupName(favoriteName);    
      if(aqString.Compare(GROUPNAME,"",false)==0)
      {
        clickOnAddFavorite()
        PQ_Methods.createPQ10MinFavorite()
        Log.Message("Configured new Favorite for "+ favoriteName)    
        GROUPNAME = findGroupName(favoriteName); 
      }              
  }  
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for" + favoriteName)
  }
}

