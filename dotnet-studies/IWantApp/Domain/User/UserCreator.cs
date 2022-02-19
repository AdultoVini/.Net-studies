
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Domain.User;

public class UserCreator
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserCreator(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<(IdentityResult, string)> Create(string email, string password, List<Claim> claims)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return (result, String.Empty);
        
        return (await _userManager.AddClaimsAsync(user, claims), user.Id);
    }
}
