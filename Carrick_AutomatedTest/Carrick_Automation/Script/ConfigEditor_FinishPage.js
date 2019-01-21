/*This page contains methods and objects related to Config Editor Finish Page
*/

var Tree_ErrorList = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucValidation.lstErrors

function GetErrorText(MainTreeItem)
{
  var cnt=-1
  if(Tree_ErrorList.Exists)
  {
    if(Tree_ErrorList.wItems.Count>0)
    {
      for (let treeitem=0;treeitem<Tree_ErrorList.wItems.Count;treeitem++)
      {
        if(Tree_ErrorList.wItems.Item(treeitem).Text==MainTreeItem)
        {
          cnt=treeitem
          break
        }      
      }
      if(cnt!=-1)
      {
        var ErrorMsg =Tree_ErrorList.wItems.Item(cnt).Items.Item(cnt).Text      
        Log.Message(ErrorMsg)      
        return ErrorMsg
      }
      else
      {
        Log.Message("No matching item found")
        Log.Picture(Tree_ErrorList,"Finish Pane Snapshot")      
        return null
      }
    }
    else
    {
      Log.Message("No Error exists on the Finish Pane")
      return null
    }
  }
  else
  {
    Log.Message("Not on Error list Page.")
  }
}