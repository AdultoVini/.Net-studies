
using dotnet_studies.Services;
using IWantApp.Infra;

namespace dotnet_studies.Endpoints.SectorServices;

public class SectorGetAll
{
    public static string Template => "sector";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(int? page, int? rows, SectorResponse sectorResponse, ApplicationDbContext context, QueryGetAllSector query)
    {
        var sectors = context.Sector.ToList();
        var sectorAndUsers = new List<SectorResponse>();
        foreach (var sector in sectors)
        {
            
            sectorsAndUsers.Add(new SectorResponse(await query.Execute(sector.Id, page.Value, rows.Value)));
            
        }
    }
}
