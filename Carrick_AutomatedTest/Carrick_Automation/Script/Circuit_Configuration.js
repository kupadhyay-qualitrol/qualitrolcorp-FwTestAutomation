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
  
  var Busbar_1_Index =0
  var Busbar_2_Index =0
  var BB1FF1_index =0
  var BB1FF2_index =0
  var BB1FF3_index =0
  var BB1FF4_index =0
  var BB1FF5_index =0
  var BB2FF1_index =0
  var BB2FF2_index =0
  var BB2FF3_index =0
  var BB2FF4_index =0
  var BB2FF5_index =0
  
  ChannelPhase =[]
  
  for (let i=0;i<NoOfChannels;i++)
  {
    var BusbarNumber = CommonMethod.ReadDataFromExcel(DataSheetName,"busbar",SheetName,i)
    var FeederNumber = CommonMethod.ReadDataFromExcel(DataSheetName,"feeder_number",SheetName,i)

    if(BusbarNumber=="BUSBAR1" && FeederNumber=="NO_FEEDER")
    {
      Busbar1[Busbar_1_Index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar1Phase[Busbar_1_Index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar1[Busbar_1_Index])
      Busbar_1_Index=Busbar_1_Index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="NO_FEEDER")
    {
      Busbar2[Busbar_2_Index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar2Phase[Busbar_2_Index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar2[Busbar_2_Index])
      Busbar_2_Index=Busbar_2_Index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER1")
    {
      Busbar_1_Feeder_1[BB1FF1_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_1_Feeder_1Phase[BB1FF1_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_1_Feeder_1[BB1FF1_index])
      BB1FF1_index=BB1FF1_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER2")
    {
      Busbar_1_Feeder_2[BB1FF2_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_1_Feeder_2Phase[BB1FF2_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_1_Feeder_2[BB1FF2_index])
      BB1FF2_index=BB1FF2_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER3")
    {
      Busbar_1_Feeder_3[BB1FF3_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_1_Feeder_3Phase[BB1FF3_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_1_Feeder_3[BB1FF3_index])
      BB1FF3_index=BB1FF3_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER4")
    {
      Busbar_1_Feeder_4[BB1FF4_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_1_Feeder_4Phase[BB1FF4_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_1_Feeder_4[BB1FF4_index])
      BB1FF4_index=BB1FF4_index+1
    }
    else if(BusbarNumber=="BUSBAR1" && FeederNumber=="FEEDER5")
    {
      Busbar_1_Feeder_5[BB1FF5_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_1_Feeder_5Phase[BB1FF5_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_1_Feeder_5[BB1FF5_index])
      BB1FF5_index=BB1FF5_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER1")
    {
      Busbar_2_Feeder_1[BB2FF1_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_2_Feeder_1Phase[BB2FF1_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_2_Feeder_1[BB2FF1_index])
      BB2FF1_index=BB2FF1_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER2")
    {
      Busbar_2_Feeder_2[BB2FF2_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_2_Feeder_2Phase[BB2FF2_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_2_Feeder_2[BB2FF2_index])
      BB2FF2_index=BB2FF2_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER3")
    {
      Busbar_2_Feeder_3[BB2FF3_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_2_Feeder_3Phase[BB2FF3_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_2_Feeder_3[BB2FF3_index])
      BB2FF3_index=BB2FF3_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER4")
    {
      Busbar_2_Feeder_4[BB2FF4_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_2_Feeder_4Phase[BB2FF4_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_2_Feeder_4[BB2FF4_index])
      BB2FF4_index=BB2FF4_index+1
    }
    else if(BusbarNumber=="BUSBAR2" && FeederNumber=="FEEDER5")
    {
      Busbar_2_Feeder_5[BB2FF5_index]= CommonMethod.ReadDataFromExcel(DataSheetName,"label",SheetName,i)
      Busbar_2_Feeder_5Phase[BB2FF5_index]=CommonMethod.ReadDataFromExcel(DataSheetName,"phase",SheetName,i)
      Log.Message(Busbar_2_Feeder_5[BB2FF5_index])
      BB2FF5_index=BB2FF5_index+1
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
      }
    }
    return true
  }
//  
//    for (let channelindex=0;channelindex<Busbar2.length;channelindex++)
//    {
//      if(Busbar2Phase[channelindex]=="A/AB")
//      {
//        ConfigEditor_Circuits.SetVL1(Busbar2[channelindex])
//      }
//      else if(Busbar2Phase[channelindex]=="B/BC")
//      {
//        ConfigEditor_Circuits.SetVL2(Busbar2[channelindex])
//      }
//      else if(Busbar2Phase[channelindex]=="C/CA")
//      {
//        ConfigEditor_Circuits.SetVL3(Busbar2[channelindex])
//      }
//      else if(Busbar2Phase[channelindex]=="N")
//      {
//        ConfigEditor_Circuits.SetVN(Busbar2[channelindex])
//      }
//      else
//      {
//        Log.Message("Phase is not defined")
//      }
//    }
//    return true
//  }
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

function SetBB1Feeder1()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_1_Feeder_1.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_1_Feeder_1.length;channelindex++)
    {
      if(Busbar_1_Feeder_1[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_1_Feeder_1[channelindex])
      }
      else if(Busbar_1_Feeder_1Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_1_Feeder_1[channelindex])
      }
      else if(Busbar_1_Feeder_1Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_1_Feeder_1[channelindex])
      }
      else if(Busbar_1_Feeder_1Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_1_Feeder_1[channelindex])
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

function SetBB1Feeder2()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_1_Feeder_2.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_1_Feeder_2.length;channelindex++)
    {
      if(Busbar_1_Feeder_2[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_1_Feeder_2[channelindex])
      }
      else if(Busbar_1_Feeder_2Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_1_Feeder_2[channelindex])
      }
      else if(Busbar_1_Feeder_2Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_1_Feeder_2[channelindex])
      }
      else if(Busbar_1_Feeder_2Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_1_Feeder_2[channelindex])
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

function SetBB1Feeder3()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_1_Feeder_3.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_1_Feeder_3.length;channelindex++)
    {
      if(Busbar_1_Feeder_3[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_1_Feeder_3[channelindex])
      }
      else if(Busbar_1_Feeder_3Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_1_Feeder_3[channelindex])
      }
      else if(Busbar_1_Feeder_3Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_1_Feeder_3[channelindex])
      }
      else if(Busbar_1_Feeder_3Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_1_Feeder_3[channelindex])
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

function SetBB1Feeder4()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_1_Feeder_4.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_1_Feeder_4.length;channelindex++)
    {
      if(Busbar_1_Feeder_4[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_1_Feeder_4[channelindex])
      }
      else if(Busbar_1_Feeder_4Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_1_Feeder_4[channelindex])
      }
      else if(Busbar_1_Feeder_4Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_1_Feeder_4[channelindex])
      }
      else if(Busbar_1_Feeder_4Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_1_Feeder_4[channelindex])
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

function SetBB1Feeder5()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_1_Feeder_5.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_1_Feeder_5.length;channelindex++)
    {
      if(Busbar_1_Feeder_5[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_1_Feeder_5[channelindex])
      }
      else if(Busbar_1_Feeder_5Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_1_Feeder_5[channelindex])
      }
      else if(Busbar_1_Feeder_5Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_1_Feeder_5[channelindex])
      }
      else if(Busbar_1_Feeder_5Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_1_Feeder_5[channelindex])
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

function SetBB2Feeder1()
{
  //Configure Feeder 1
  if(Busbar2.length>0 && Busbar_2_Feeder_1.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_2_Feeder_1.length;channelindex++)
    {
      if(Busbar_2_Feeder_1[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_2_Feeder_1[channelindex])
      }
      else if(Busbar_2_Feeder_1Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_2_Feeder_1[channelindex])
      }
      else if(Busbar_2_Feeder_1Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_2_Feeder_1[channelindex])
      }
      else if(Busbar_2_Feeder_1Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_2_Feeder_1[channelindex])
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

function SetBB2Feeder2()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_2_Feeder_2.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_2_Feeder_2.length;channelindex++)
    {
      if(Busbar_2_Feeder_2[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_2_Feeder_2[channelindex])
      }
      else if(Busbar_2_Feeder_2Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_2_Feeder_2[channelindex])
      }
      else if(Busbar_2_Feeder_2Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_2_Feeder_2[channelindex])
      }
      else if(Busbar_2_Feeder_2Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_2_Feeder_2[channelindex])
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

function SetBB2Feeder3()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_2_Feeder_3.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_2_Feeder_3.length;channelindex++)
    {
      if(Busbar_2_Feeder_3[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_2_Feeder_3[channelindex])
      }
      else if(Busbar_2_Feeder_3Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_2_Feeder_3[channelindex])
      }
      else if(Busbar_2_Feeder_3Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_2_Feeder_3[channelindex])
      }
      else if(Busbar_2_Feeder_3Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_2_Feeder_3[channelindex])
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

function SetBB2Feeder4()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_2_Feeder_4.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_2_Feeder_4.length;channelindex++)
    {
      if(Busbar_2_Feeder_4[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_2_Feeder_4[channelindex])
      }
      else if(Busbar_2_Feeder_4Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_2_Feeder_4[channelindex])
      }
      else if(Busbar_2_Feeder_4Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_2_Feeder_4[channelindex])
      }
      else if(Busbar_2_Feeder_4Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_2_Feeder_4[channelindex])
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

function SetBB2Feeder5()
{
  //Configure Feeder 1
  if(Busbar1.length>0 && Busbar_2_Feeder_5.length>0)
  {
    for (let channelindex=0;channelindex<Busbar_2_Feeder_5.length;channelindex++)
    {
      if(Busbar_2_Feeder_5[channelindex]=="A/AB")
      {
        ConfigEditor_Circuits.SetIL1(Busbar_2_Feeder_5[channelindex])
      }
      else if(Busbar_2_Feeder_5Phase[channelindex]=="B/BC")
      {
        ConfigEditor_Circuits.SetIL2(Busbar_2_Feeder_5[channelindex])
      }
      else if(Busbar_2_Feeder_5Phase[channelindex]=="C/CA")
      {
        ConfigEditor_Circuits.SetIL3(Busbar_2_Feeder_5[channelindex])
      }
      else if(Busbar_2_Feeder_5Phase[channelindex]=="N")
      {
        ConfigEditor_Circuits.SetIN(Busbar_2_Feeder_5[channelindex])
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

function ConfigureBB1Feeder()
{
  var FeederArr =[Busbar_1_Feeder_1,Busbar_1_Feeder_2,Busbar_1_Feeder_3,Busbar_1_Feeder_4,Busbar_1_Feeder_5]
  var PhaseArr = [Busbar_1_Feeder_1Phase,Busbar_1_Feeder_2Phase,Busbar_1_Feeder_3Phase,Busbar_1_Feeder_4Phase,Busbar_1_Feeder_5Phase]
  var ActualFeeder_BB1 =[]
  var ActualPhase_BB1 =[]
  var BB1arrindex=0
  for (let i=0;i<FeederArr.length;i++)
  {
    if(FeederArr[i].length>0)
    {    
      ActualFeeder_BB1[BB1arrindex]=FeederArr[i]
      ActualPhase_BB1[BB1arrindex] = PhaseArr[i]
      BB1arrindex=BB1arrindex+1
    }  
  }
  for(let i=0;i<ActualFeeder_BB1.length;i++)
  {
    if(i!=0)
    {
      ConfigEditor_Circuits.ClickOnAddNewCircuit()
    }  
    SetBBFeeder(ActualFeeder_BB1[i],ActualPhase_BB1[i])
  }
}

function ConfigureBB2Feeder()
{
  var FeederArr =[Busbar_2_Feeder_1,Busbar_2_Feeder_2,Busbar_2_Feeder_3,Busbar_2_Feeder_4,Busbar_2_Feeder_5]
  var PhaseArr = [Busbar_2_Feeder_1Phase,Busbar_2_Feeder_2Phase,Busbar_2_Feeder_3Phase,Busbar_2_Feeder_4Phase,Busbar_2_Feeder_5Phase]
  var ActualFeeder_BB2 =[]
  var ActualPhase_BB2 =[]
  var BB2arrindex=0
  for (let i=0;i<FeederArr.length;i++)
  {
    if(FeederArr[i].length>0)
    {
      ActualFeeder_BB2[BB2arrindex]=FeederArr[i]
      ActualPhase_BB2[BB2arrindex] = PhaseArr[i]
      BB2arrindex=BB2arrindex+1
    }  
  }
  for(let i=0;i<ActualFeeder_BB2.length;i++)
  {
    if(i!=0)
    {
      ConfigEditor_Circuits.ClickOnAddNewCircuit()
    }  
    SetBBFeeder(ActualFeeder_BB2[i],ActualPhase_BB2[i])
  }
}
