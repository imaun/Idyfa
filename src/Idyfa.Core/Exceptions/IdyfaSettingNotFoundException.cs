namespace Idyfa.Core.Exceptions;

public class IdyfaSettingNotFoundException : Exception
{

    public IdyfaSettingNotFoundException() : base(message: "The setting for Idyfa not found.")
    {
    }
}