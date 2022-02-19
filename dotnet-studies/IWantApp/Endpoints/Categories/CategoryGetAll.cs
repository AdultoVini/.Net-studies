using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(ApplicationDbContext context)
    {
        var category = context.Categories.ToList();
        var response = category.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name, CreatedBy = c.CreatedBy, EditedBy =  c.EditedBy, Active = c.Active });
        return Results.Ok(response);
    }
}
