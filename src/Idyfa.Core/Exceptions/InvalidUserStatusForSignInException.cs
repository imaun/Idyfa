namespace Idyfa.Core.Exceptions;

public class InvalidUserStatusForSignInException : Exception
{
    public InvalidUserStatusForSignInException(UserStatus status)
    {
        UserStatus = status;
    }
    
    public UserStatus UserStatus { get; }
}