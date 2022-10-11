namespace Idyfa.Core.Exceptions;

public class IdyfaOptionsNotFoundException : Exception
{

    public IdyfaOptionsNotFoundException() : base(message: "The Options for Idyfa not found.")
    {
    }
}