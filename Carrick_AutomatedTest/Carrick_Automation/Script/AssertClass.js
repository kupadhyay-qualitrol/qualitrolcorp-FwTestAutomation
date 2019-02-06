/*
 This file contains methods related to Assert Class to verify the execution steps
*/

//This method is used to compare Actual boolean values.
function IsTrue(Actual,LogMessage)
{
  if(Actual !="")
  {
    if(Actual==true)
    {
      Log.Message("Actual Value is :- "+Actual+".Message:- "+LogMessage)    
    }
    else
    {
      Log.Message("Results didn't match.Actual Value is :- "+Actual+".Message:- "+LogMessage)    
      throw ("Results didn't match.Actual Value is :- "+Actual+".Message:- "+LogMessage)
    }
  }
  else
  {
    throw ("Actual value is null")
  }
}

//This method is used to compare Actual boolean values.
function IsFalse(Actual,LogMessage)
{
  if(Actual !="")
  {
    if(Actual==false)
    {
      Log.Message("Actual Value is :- "+Actual+".Message:- "+LogMessage)    
    }
    else
    {
      Log.Message("Results didn't match.Actual Value is :- "+Actual+".Message:- "+LogMessage)    
      throw ("Results didn't match.Actual Value is :- "+Actual+".Message:- "+LogMessage)
    }
  }
  else
  {
    throw ("Actual value is null")
  }
}

//This method is used to compare Decimal values with Delta.
function CompareDecimalValues(Expected,Actual,delta,LogMessage)
{
  if(typeof(Expected)=="number" && typeof(Actual)=="number")
  {
    if(Math.abs(Expected-Actual)<=delta)
    {
      Log.Message("Delta is :- "+Math.abs(Expected-Actual)+".Message:- "+LogMessage)    
    }
    else
    {
      Log.Message("Results didn't match.Delta is :- "+Math.abs(Expected-Actual)+".Message:- "+LogMessage)      
      throw ("Results didn't match.Delta is :- "+Math.abs(Expected-Actual)+".Message:- "+LogMessage)
    }
  }
  else
  {
    throw ("Expected/Actual value is not a decimal:- "+ Expected+"  "+Actual)
  }  
}

//This method is used to compare Expected and Actual string values.
function CompareString(Expected,Actual,LogMessage)
{
  if(Actual !="" && Expected !="")
  {
    if(typeof(Expected)=="string" && typeof(Actual)=="string")
    {  
      if(aqString.Compare(Expected,Actual,false)==0)
      {
        Log.Message("Strings are Same.Message:- "+LogMessage)    
      }
      else
      {
        Log.Message("Results didn't match.Expected:- "+Expected+" .Actual:- "+Actual+" .Message:- "+LogMessage)      
        throw ("Results didn't match.Expected:- "+Expected+" .Actual:- "+Actual+" .Message:- "+LogMessage)
      }
    }
    else
    {
      throw ("Expected/Actual value is not a string:- "+ Expected+"  "+Actual)
    }
  }
  else
  {
    throw ("Actual/Expected value is null")
  }
}