using Dapper;
using dotnet_studies.Endpoints.SectorServices;
using Microsoft.Data.SqlClient;

namespace dotnet_studies.Services;

public class QueryGetAllSector
{
    private readonly IConfiguration configuration;

    public QueryGetAllSector(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
   public async Task<IEnumerable<UserResponseforSector>> Execute(string id, int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionString:Trainee"]);
        var query = @"select Email
            from AspNetUsers u inner join AspNetUserClaims c  
            on u.id = c.UserId and claimtype = 'SectorId' and claimvalue = @id
            OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<UserResponseforSector>(
            query,
            new { page, rows }
            );
    }
}
