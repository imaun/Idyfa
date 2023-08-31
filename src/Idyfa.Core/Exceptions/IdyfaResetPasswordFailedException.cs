using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Exceptions;

public class IdyfaResetPasswordFailedException : Exception
{
    
    public IdyfaResetPasswordFailedException(IReadOnlyCollection<IdentityError> errors)
        : base(message: "Password Reset failed.")
    {
        ResetPasswordErrors = errors;
    }
    
    public IReadOnlyCollection<IdentityError> ResetPasswordErrors { get; }
}