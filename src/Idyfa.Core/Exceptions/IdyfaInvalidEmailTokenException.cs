namespace Idyfa.Core.Exceptions;

public class IdyfaInvalidEmailTokenException : Exception
{

    public IdyfaInvalidEmailTokenException()
        : base("The Email confirmation failed because the token is invalid.")
    {
    }
}