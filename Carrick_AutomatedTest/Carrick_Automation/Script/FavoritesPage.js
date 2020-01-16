//USEUNIT AssertClass
//USEUNIT CommonMethod
//USEUNIT ConfigEditor_PQ_FreeInterval
/*This file contains methods and object variables related favorites*/
//Variables
var Topology_DefaultFavorites =Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.DAFAVuexpbarFavorites
var TOOLS_FAVORITES = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.zUserControlBase_Toolbars_Dock_Area_Top
var PANE_FAVORITE = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4
var GROUPNAME
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

function findGroupName(favoriteName)
{
    var defaultFavoriteItemCount = DEFAULT_FAV.wItemCount("Default Favorites")
    var myFavoriteItemCount = DEFAULT_FAV.wItemCount("My Favorites")
    var defaultFavoriteItems = []
    var myFavoriteItems = []
    var groupName
    for (counter=0;counter<defaultFavoriteItemCount;counter++)
    {
     defaultFavoriteItems [counter]  = DEFAULT_FAV.wItem(0, counter)
    } 
    if(defaultFavoriteItems.includes(favoriteName))
    {
        groupName = "Default Favorites"
        Log.Message("Favorite found in "+groupName)
        return groupName
    }       
    for (counter=0;counter<myFavoriteItemCount;counter++)
    {
      myFavoriteItems [counter] = DEFAULT_FAV.wItem(1, counter)
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
  //Navigate to Favorites under Conitnouous Recording for PQ Free Interval
  CommonMethod.RibbonToolbar.wItems.Item("&Data Analysis").Click()
  CommonMethod.RibbonToolbar.ClickItem("&Data Analysis|Data Analysis Views|&Continuous Recording")
  
  try 
  { 
      GROUPNAME = findGroupName(favoriteName);    
      if(aqString.Compare(GROUPNAME,"",false)==0)
      {
        ConfigEditor_PQ.createNewFavoriteForPqFreeInterval()
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