using dotnet_studies.Domain.Sector;
using IWantApp.Infra;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_studies.Endpoints.SectorServices;
public class SectorPost
{
    public static string Template => "/sector";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    
    public static async Task<IResult> Action(SectorRequest sectorRequest, ApplicationDbContext context)
    {
        var sector = new Sector
        {
            Name = sectorRequest.Name,
        };
        await context.Sector.AddAsync(sector);
        await context.SaveChangesAsync();
        return Results.Created($"/sector;{sector.Id}", sector.Id);
    }
}
