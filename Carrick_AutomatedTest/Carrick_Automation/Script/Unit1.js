function Message()
{
  Log.Message(BuiltIn.ParamCount())
  Log.Message(DeviceIP_18Channel)
  for (let i=0;i<=BuiltIn.ParamCount();i++ )
  {
    Log.Message(BuiltIn.ParamStr(i))
  }
  
}