using IWantApp.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace dotnet_studies.Endpoints.User;

public class UserPost
{
    public static string Template => "/user";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(UserRequest userRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser
        {
            UserName = userRequest.Email,
            Email = userRequest.Email
        };
        var result = await userManager.CreateAsync(user, userRequest.Password);
        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }
        var claimList = new List<Claim>
        {
            new Claim("SectorId", userRequest.SectorId),
            new Claim("Name", userRequest.Name)
        };
        var claimResult = await userManager.AddClaimsAsync(user, claimList);
        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(claimResult.Errors.First());
        }
        return Results.Created($"/user/{user.Id}", user.Id);
    }
}
