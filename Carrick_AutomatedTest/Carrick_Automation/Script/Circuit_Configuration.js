/*This file contains methods related to circuit configuration in iQ+*/
//USEUNIT CommonMethod
//USEUNIT ConfigEditor_Circuits

//Variables
var Busbar1
var Busbar2
var Busbar_1_Feeder_1
var Busbar_1_Feeder_2
var Busbar_1_Feeder_3
var Busbar_1_Feeder_4
var Busbar_1_Feeder_5
var Busbar_2_Feeder_1
var Busbar_2_Feeder_2
var Busbar_2_Feeder_3
var Busbar_2_Feeder_4
var Busbar_2_Feeder_5

var Busbar1Phase
var Busbar2Phase
var Busbar_1_Feeder_1Phase
var Busbar_1_Feeder_2Phase
var Busbar_1_Feeder_3Phase
var Busbar_1_Feeder_4Phase
var Busbar_1_Feeder_5Phase
var Busbar_2_Feeder_1Phase
var Busbar_2_Feeder_2Phase
var Busbar_2_Feeder_3Phase
var Busbar_2_Feeder_4Phase
var Busbar_2_Feeder_5Phase
//

function GetCircuitConfiguration(DataSheetName,SheetName,NoOfChannels)
{
  Busbar1 =[]
  Busbar2 =[]      
  Busbar_1_Feeder_1 =[]
  Busbar_1_Feeder_2 =[]
  Busbar_1_Feeder_3 =[]
  Busbar_1_Feeder_4 =[]
  Busbar_1_Feeder_5 =[]
  Busbar_2_Feeder_1 =[]
  Busbar_2_Feeder_2 =[]
  Busbar_2_Feeder_3 =[]
  Busbar_2_Feeder_4 =[]
  Busbar_2_Feeder_5 =[]
  
  Busbar1Phase =[]
  Busbar2Phase =[]      
  Busbar_1_Feeder_1Phase =[]
  Busbar_1_Feeder_2Phase =[]
  Busbar_1_Feeder_3Phase =[]
  Busbar_1_Feeder_4Phase =[]
  Busbar_1_Feeder_5Phase =[]
  Busbar_2_Feeder_1Phase =[]
  Busbar_2_Feeder_2Phase =[]
  Busbar_2_Feeder_3Phase =[]
  Busbar_2_Feeder_4Phase =[]
  Busbar_2_Feeder_5Phase =[]
  
  var busbar_1_Index =0
  var busbar_1_Index =0
  var bB1FF1_index =0
  var bB1FF2_index =0
  var bB1FF3_index =0
  var bB1FF4_index =0
  var bB1FF5_index =0
  var bB2FF1_index =0
  var bB2FF2_index =0
  var bB2FF3_index =0
  var bB2FF4_index =0
  var bB2FF5_index =0
  
  ChannelPhase =[]
  
  for (let channeliterator=0;channeliterator<NoOfChannels;channeliterator++)
  {
    var BusbarNumber = CommonMethod.ReadDataFromExcel(DataSheetName,"busbar",SheetName,channeliterator)
    var FeederNumber = CommonMethod.ReadDataFromExcel(DataSheetName,"feeder_number",SheetName,channeliterator)

    if(BusbarNumber=="BUSBAR1" && FeederNumber=="NO_FEEDER")
    {
      Busbar1[busbar_1_Index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar1Phase[busbar_1_Index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar1[busbar_1_Index])
      busbar_1_Index=busbar_1_Index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="NO_FEEDER")
    {
      Busbar2[busbar_2_Index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar2Phase[busbar_2_Index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar2[busbar_2_Index])
      busbar_2_Index=busbar_2_Index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER1")
    {
      Busbar_1_Feeder_1[bB1FF1_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_1_Feeder_1Phase[bB1FF1_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_1_Feeder_1[bB1FF1_index])
      bB1FF1_index=bB1FF1_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER2")
    {
      Busbar_1_Feeder_2[bB1FF2_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_1_Feeder_2Phase[bB1FF2_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_1_Feeder_2[bB1FF2_index])
      bB1FF2_index=bB1FF2_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER3")
    {
      Busbar_1_Feeder_3[bB1FF3_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_1_Feeder_3Phase[bB1FF3_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_1_Feeder_3[bB1FF3_index])
      bB1FF3_index=bB1FF3_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER4")
    {
      Busbar_1_Feeder_4[bB1FF4_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_1_Feeder_4Phase[bB1FF4_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_1_Feeder_4[bB1FF4_index])
      bB1FF4_index=bB1FF4_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER5")
    {
      Busbar_1_Feeder_5[bB1FF5_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_1_Feeder_5Phase[bB1FF5_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_1_Feeder_5[bB1FF5_index])
      bB1FF5_index=bB1FF5_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER1")
    {
      Busbar_2_Feeder_1[bB2FF1_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_2_Feeder_1Phase[bB2FF1_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_2_Feeder_1[bB2FF1_index])
      bB2FF1_index=bB2FF1_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER2")
    {
      Busbar_2_Feeder_2[bB2FF2_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_2_Feeder_2Phase[bB2FF2_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_2_Feeder_2[bB2FF2_index])
      bB2FF2_index=bB2FF2_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER3")
    {
      Busbar_2_Feeder_3[bB2FF3_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_2_Feeder_3Phase[bB2FF3_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_2_Feeder_3[bB2FF3_index])
      bB2FF3_index=bB2FF3_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER4")
    {
      Busbar_2_Feeder_4[bB2FF4_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_2_Feeder_4Phase[bB2FF4_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_2_Feeder_4[bB2FF4_index])
      bB2FF4_index=bB2FF4_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER5")
    {
      Busbar_2_Feeder_5[bB2FF5_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,channeliterator)
      Busbar_2_Feeder_5Phase[bB2FF5_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,channeliterator)
      Log.Message(Busbar_2_Feeder_5[bB2FF5_index])
      bB2FF5_index=bB2FF5_index+1
    }
  }
}

function SetBusbar1()
{
  //Configure Busbar 1
  if(Busbar1.length>0)
  {
    for (let channelindex=0;channelindex<Busbar1.length;channelindex++)
    {  
      if(Busbar1.length==1)
      {
        if(Busbar1Phase[channelindex]=="A/AB")
        {
          ConfigEditor_Circuits.SetVL1(Busbar1[channelindex])
          ConfigEditor_Circuits.SetVL2("Not Selected")
          ConfigEditor_Circuits.SetVL3("Not Selected")
          ConfigEditor_Circuits.SetVN("Not Selected")
        }
        else if(Busbar1Phase[channelindex]=="B/BC")
        {
          ConfigEditor_Circuits.SetVL1("Not Selected")      
          ConfigEditor_Circuits.SetVL2(Busbar1[channelindex])                
          ConfigEditor_Circuits.SetVL3("Not Selected")
          ConfigEditor_Circuits.SetVN("Not Selected")
        }
        else if(Busbar1Phase[channelindex]=="C/CA")
        {
          ConfigEditor_Circuits.SetVL1("Not Selected") 
          ConfigEditor_Circuits.SetVL2("Not Selected") 
          ConfigEditor_Circuits.SetVL3(Busbar1[channelindex])
          ConfigEditor_Circuits.SetVN("Not Selected")        
        }
        else if(Busbar1Phase[channelindex]=="N")
        {
          ConfigEditor_Circuits.SetVL1("Not Selected") 
          ConfigEditor_Circuits.SetVL2("Not Selected")         
          ConfigEditor_Circuits.SetVL3("Not Selected")
          ConfigEditor_Circuits.SetVN(Busbar1[channelindex])
        }
        else
        {
          Log.Message("Phase is not defined")
        }
      }
      else
      { 
        if(Busbar1Phase[channelindex]=="A/AB")
        {
          ConfigEditor_Circuits.SetVL1(Busbar1[channelindex])
        }
        else if(Busbar1Phase[channelindex]=="B/BC")
        {
          ConfigEditor_Circuits.SetVL2(Busbar1[channelindex])
        }
        else if(Busbar1Phase[channelindex]=="C/CA")
        {
          ConfigEditor_Circuits.SetVL3(Busbar1[channelindex])
        }
        else if(Busbar1Phase[channelindex]=="N")
        {
          ConfigEditor_Circuits.SetVN(Busbar1[channelindex])
        }
        else
        {
          Log.Message("Phase is not defined")
        }
        if(Busbar1.length==3)
        {
          ConfigEditor_Circuits.SetVN("Not Selected")
        }
      }
    }
    return true
  }
  else
  {
    Log.Message("No Circuit exists")
    return false
  }
}

function SetBusbar2()
{
  //Configure Busbar 2
  if(Busbar2.length>0 && Busbar1.length>0)
  {
    for (let channelindex=0;channelindex<Busbar2.length;channelindex++)
    {  
      if(Busbar2.length==1)
      {
        if(Busbar2Phase[channelindex]=="A/AB")
        {
          ConfigEditor_Circuits.SetVL1(Busbar2[channelindex])
          ConfigEditor_Circuits.SetVL2("Not Selected")
          ConfigEditor_Circuits.SetVL3("Not Selected")
          ConfigEditor_Circuits.SetVN("Not Selected")
        }
        else if(Busbar2Phase[channelindex]=="B/BC")
        {
          ConfigEditor_Circuits.SetVL1("Not Selected")      
          ConfigEditor_Circuits.SetVL2(Busbar2[channelindex])                
          ConfigEditor_Circuits.SetVL3("Not Selected")
          ConfigEditor_Circuits.SetVN("Not Selected")
        }
        else if(Busbar2Phase[channelindex]=="C/CA")
        {
          ConfigEditor_Circuits.SetVL1("Not Selected") 
          ConfigEditor_Circuits.SetVL2("Not Selected") 
          ConfigEditor_Circuits.SetVL3(Busbar2[channelindex])
          ConfigEditor_Circuits.SetVN("Not Selected")        
        }
        else if(Busbar2Phase[channelindex]=="N")
        {
          ConfigEditor_Circuits.SetVL1("Not Selected") 
          ConfigEditor_Circuits.SetVL2("Not Selected")         
          ConfigEditor_Circuits.SetVL3("Not Selected")
          ConfigEditor_Circuits.SetVN(Busbar2[channelindex])
        }
        else
        {
          Log.Message("Phase is not defined")
        }
      }
      else
      { 
        if(Busbar2Phase[channelindex]=="A/AB")
        {
          ConfigEditor_Circuits.SetVL1(Busbar2[channelindex])
        }
        else if(Busbar2Phase[channelindex]=="B/BC")
        {
          ConfigEditor_Circuits.SetVL2(Busbar2[channelindex])
        }
        else if(Busbar2Phase[channelindex]=="C/CA")
        {
          ConfigEditor_Circuits.SetVL3(Busbar2[channelindex])
        }
        else if(Busbar2Phase[channelindex]=="N")
        {
          ConfigEditor_Circuits.SetVN(Busbar2[channelindex])
        }
        else
        {
          Log.Message("Phase is not defined")
        }
        if(Busbar2.length==3)
        {
          ConfigEditor_Circuits.SetVN("Not Selected")
        }
      }
    }
    return true
  }
  else
  {
    Log.Message("Busbar 2 is not defined")
    return false
  }
}

function SetBBFeeder(BusbarFeederData,BusbarFeederPhase)
{
  //Configure Feeder
  if(Busbar1.length>0)
  {
    for (let channelindex=0;channelindex<BusbarFeederData.length;channelindex++)
    {
      if(BusbarFeederPhase[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(BusbarFeederData[channelindex])
      }
      else if(BusbarFeederPhase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(BusbarFeederData[channelindex])
      }
      else if(BusbarFeederPhase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(BusbarFeederData[channelindex])
      }
      else if(BusbarFeederPhase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetILN(BusbarFeederData[channelindex])
      }
      else
      {
        Log.Message("Phase is not defined")
      }
    }
    return true
  }
  else
  {
    Log.Message("Feeder Channels not defined")
    return false
  }
}

function ConfigureBB1Feeder(Busbar1_Name)
{
  var feederArr =[Busbar_1_Feeder_1,Busbar_1_Feeder_2,Busbar_1_Feeder_3,Busbar_1_Feeder_4,Busbar_1_Feeder_5]
  var phaseArr = [Busbar_1_Feeder_1Phase,Busbar_1_Feeder_2Phase,Busbar_1_Feeder_3Phase,Busbar_1_Feeder_4Phase,Busbar_1_Feeder_5Phase]
  var actualFeeder_BB1 =[]
  var actualPhase_BB1 =[]
  var bB1arrindex=0
  for (let feederarrindex=0;feederarrindex<feederArr.length;feederarrindex++)
  {
    if(feederArr[feederarrindex].length>0)
    {    
      actualFeeder_BB1[bB1arrindex]=feederArr[feederarrindex]
      actualPhase_BB1[bB1arrindex] = phaseArr[feederarrindex]
      bB1arrindex=bB1arrindex+1
    }  
  }
  for(let actualfeederindex=0;actualfeederindex<actualFeeder_BB1.length;actualfeederindex++)
  {
    if(actualfeederindex!=0)
    {
      ConfigEditor_Circuits.ClickOnAddNewCircuit()
      ConfigEditor_Circuits.SwitchBusbar(Busbar1_Name)
    }  
    SetBBFeeder(actualFeeder_BB1[actualfeederindex],actualPhase_BB1[actualfeederindex])
  }
}

function ConfigureBB2Feeder(Busbar2_Name)
{
  var feederArr =[Busbar_2_Feeder_1,Busbar_2_Feeder_2,Busbar_2_Feeder_3,Busbar_2_Feeder_4,Busbar_2_Feeder_5]
  var phaseArr = [Busbar_2_Feeder_1Phase,Busbar_2_Feeder_2Phase,Busbar_2_Feeder_3Phase,Busbar_2_Feeder_4Phase,Busbar_2_Feeder_5Phase]
  var actualFeeder_BB2 =[]
  var actualPhase_BB2 =[]
  var bB2arrindex=0
  for (let feederarrindex=0;feederarrindex<feederArr.length;feederarrindex++)
  {
    if(feederArr[feederarrindex].length>0)
    {
      actualFeeder_BB2[bB2arrindex]=feederArr[feederarrindex]
      actualPhase_BB2[bB2arrindex] = phaseArr[feederarrindex]
      bB2arrindex=bB2arrindex+1
    }  
  }
  for(let actualfeederindex=0;actualfeederindex<actualFeeder_BB2.length;actualfeederindex++)
  {
    if(actualfeederindex!=0)
    {
      ConfigEditor_Circuits.ClickOnAddNewCircuit()
      ConfigEditor_Circuits.SwitchBusbar(Busbar2_Name)
    }  
    SetBBFeeder(actualFeeder_BB2[actualfeederindex],actualPhase_BB2[actualfeederindex])
  }
}
