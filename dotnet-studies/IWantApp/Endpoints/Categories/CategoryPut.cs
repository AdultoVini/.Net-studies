using Flunt.Validations;
using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDbContext context, HttpContext http)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var categoryEdit = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if(categoryEdit == null)
        {
            return Results.NotFound();
        }
        categoryEdit.EditInfo(categoryRequest.Name, userId, categoryRequest.Active);
        if (!categoryEdit.IsValid)
        {
            return Results.ValidationProblem(categoryEdit.Notifications.ConvertToProblemDetails());
        }
            
        context.SaveChanges();
        return Results.Ok();
    }
}
