function Message()
{
  Log.Message(BuiltIn.ParamCount())
  
  for (let i=0;i<=BuiltIn.ParamCount();i++ )
  {
    Log.Message(BuiltIn.ParamStr(i))
  }
  
}