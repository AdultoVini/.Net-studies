using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employee;

public class EmployeeGetAll
{
    public static string Template => "employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        /*var users = UserManager.Users.ToList();
        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = UserManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(item.Email, userName));
        }

        return Results.Ok(employees);*/
        //dapper abaixo
        if(page == null || page == 0 || rows == null || rows > 10)
        {
            return Results.BadRequest("Parâmetros errados");
        }
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
