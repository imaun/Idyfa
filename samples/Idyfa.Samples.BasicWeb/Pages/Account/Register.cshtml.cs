using Idyfa.Core.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Idyfa.Samples.BasicWeb.Pages.Account;

public class Register : PageModel
{
    private readonly IIdyfaUserManager _userManager;

    public Register(IIdyfaUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
        var user = Core.User.RegisterUser(
            "imun", "dsdasd", "imun22@gmail.com", "9120939232", "iman", "nemati");

        var result = await _userManager.CreateAsync(user, "sadasfdasd");
    }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}