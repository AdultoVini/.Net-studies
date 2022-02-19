using IWantApp.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Clients;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator)
    {
        var claimList = new List<Claim>
        {
            new Claim("EmployeeCode", clientRequest.Cpf),
            new Claim("Name", clientRequest.Name )
        };
        (IdentityResult identity, string userId) result = await userCreator.Create(clientRequest.Email, clientRequest.Password, claimList);

        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        //var user = new IdentityUser
        //{
        //    UserName = clientRequest.Email,
        //    Email = clientRequest.Email
        //};
        //var result = await userManager.CreateAsync(user, clientRequest.Password);
        //if (!result.Succeeded)
        //    return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        //var claimList = new List<Claim>
        //{
        //    new Claim("EmployeeCode", clientRequest.Cpf),
        //    new Claim("Name", clientRequest.Name )
        //};
        //var claimResult = await userManager.AddClaimsAsync(user, claimList);

        //if (!claimResult.Succeeded)
        //    return Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/clients/{result.userId}", result.userId);
    }
}
