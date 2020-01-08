/*This file contains methods and object variables related Continuous Recording Customization Screen*/

//Variables

var DLG_CONTINUOUS_RECORDING = Aliases.iQ_Plus.ModalDialogContainer
var BTN_PQ_WAVEFORM = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CRCSTgrpCRStyles.CRCSTrboPQWaveform
var RADIOBTN_10MINUTE = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTgrpTimeInterval.CRPQPCSTpnlCRPQDataType.CRPQPCSTrdbtn10Minute
var BTN_MORE = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTbtnMoreHide
var CHECKBOX_VRMS = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamFirst.CRPQPCSTgrpCRPQParametersFirst.CRPQPCSTchkURMS
var CHECKBOX_IRMS = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamFirst.CRPQPCSTgrpCRPQParametersFirst.CRPQPCSTchkIRMS
var CHECKBOX_RMS = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamThird.CRPQPCSTgrpCRPQParametersThird.CRPQPCSTchkStandaloneRMS 
var EDT_BOX_FAVORITE = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.DACSTBgbOptions.DACSTBtxtFavoriteName
var BTN_SAVE = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.DACSTBgbSelection.DACSTBbtnSave

//This Function is used to Click on PQ Standalone waveform
function clickOnPQStandaloneFavorite()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
    BTN_PQ_WAVEFORM.ClickButton()
    Log.Message("PQ Waveform button clicked")
    return true
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}
//This function is used to select 10 min radio button.
function select10MinRadioBtn()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
   RADIOBTN_10MINUTE.ClickButton()
   Log.Message("10 Minute radio button checked") 
   return true
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}
//This function is used to click on more button.
function clickOnMoreButton()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
   BTN_MORE.ClickButton()
   Log.Message("More Button clicked") 
   return true
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}
//This function is used to check VRMS checkbox.
function checkVRMSAndIRMSCheckBox()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
   CHECKBOX_VRMS.Click()
   Log.Message("VRMS checkbox checked") 
   CHECKBOX_IRMS.Click()
   Log.Message("IRMS checkbox checked") 
   return true
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}
//This function is used to check RMS checkbox.
function checkRMSCheckBox()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
   CHECKBOX_RMS.Click()
   Log.Message("RMS checkbox checked") 
   return true
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}
//This function is used to add favorite name
function add10MinFavoriteName()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
    EDT_BOX_FAVORITE.SetText("PQ10Min")
    Log.Message("Added PQ favorite name as PQ10Min")
    return true
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}
//This function is used to click on Save button
function clickOnSavePQButton()
{
  if(DLG_CONTINUOUS_RECORDING.Visible)
  {
   BTN_SAVE.Click()
   Log.Message("Save Button clicked")
   return true 
  }
  else
  {
    return false
    Log.Message("Continuous Recording Customization Screen is not Visibile")
  }
}