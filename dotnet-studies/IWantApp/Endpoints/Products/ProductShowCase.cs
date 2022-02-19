using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Endpoints.Products;

public class ProductShowCase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "name")
    {
        if(row > 10)
        {
            return Results.Problem(title: "Row with max 10", statusCode: 400);
        }
        var product = context.Products.AsNoTracking().Include(p => p.Category).Where(p => p.HasStock && p.Active);

        
        if(orderBy == "name")
        {
            product = product.OrderBy(product => product.Name);
        }
        else if(orderBy == "price")
        {
            product = product.OrderBy(product => product.Price);
        }
        else
        {
            return Results.Problem(title: "Order by price or name   ", statusCode: 400);
        }
        product = product.Skip((page - 1) * row).Take(row);
            var productsFiltereds = product.ToList();

        var results = productsFiltereds.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price ,p.Active));
        return Results.Ok(results);
    }
}
