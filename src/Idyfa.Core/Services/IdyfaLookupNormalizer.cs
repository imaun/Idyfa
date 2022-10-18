using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Services;

public class IdyfaLookupNormalizer : ILookupNormalizer
{
    
    public string NormalizeName(string name)
    {
        if (name.IsNullOrEmpty())
        {
            return null;
        }
        
        name = name.Trim().CorrectYeKe().ToUpperInvariant();
        return name.RemoveDiacritics()
            .RemovePunctuation()
            .CleanUnderLines();
    }

    public string NormalizeEmail(string email)
    {
        if (email.IsNullOrEmpty())
        {
            return null;
        }

        return email.Trim().CorrectYeKe().ToUpperInvariant()
                    .RemoveDiacritics().RemovePunctuation().CleanUnderLines();
    }
}