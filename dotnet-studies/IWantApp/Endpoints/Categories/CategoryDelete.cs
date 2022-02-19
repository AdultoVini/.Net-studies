using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryDelete
{
    public static string Template => "/categories/{id}";
    public static string[] methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var categoryDelete = context.Categories.Where(c => c.Id == id).FirstOrDefault();
        context.Categories.Remove(categoryDelete);
        context.SaveChanges();
        return Results.Ok();
    }

}
