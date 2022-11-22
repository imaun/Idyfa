using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Exceptions;

public class InvalidUserNameException : Exception
{

    public InvalidUserNameException() 
        : base(message: "The UserName is not valid.")
    {
        Errors = new List<IdentityError>();
    }

    public InvalidUserNameException(IdentityError[] errors)
    {
        Errors = errors.ToList();
    }
    
    public IEnumerable<IdentityError> Errors { get; }
}