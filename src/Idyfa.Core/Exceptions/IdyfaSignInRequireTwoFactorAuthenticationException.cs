namespace Idyfa.Core.Exceptions;

public class IdyfaSignInRequireTwoFactorAuthenticationException : Exception
{

    public IdyfaSignInRequireTwoFactorAuthenticationException() 
        : base("TwoFactor Authentication Required for Sign-in.")
    {
    }
}