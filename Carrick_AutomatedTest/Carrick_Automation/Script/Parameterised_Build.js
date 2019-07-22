function Test()
{
  Log.Message(BuiltIn.ParamCount())
  var temp =aqString.Trim(BuiltIn.ParamStr(11))
  Log.Message(temp)
  aqString.ListSeparator=";"
  Log.Message(aqString.GetListLength(temp))
  for(i=0;i<aqString.GetListLength(temp);i++)
  {
    Log.Message(aqString.GetListItem(temp,i))
  }
  
}

