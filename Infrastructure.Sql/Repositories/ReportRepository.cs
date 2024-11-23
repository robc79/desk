using Dapper;
using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Desk.Infrastructure.Sql.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly string _connectionString;

    public ReportRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<UserReport>> GetUserReportsAsync(CancellationToken ct)
    {
        IEnumerable<UserReport> userReports;
        
        const string sql = @"
            select AspNetUsers.UserName as Username, count(Items.Id) as ItemCount, count(Items.ImageName) as ImageCount
            from Items
            inner join AspNetUsers on Items.OwnerId = AspNetUsers.Id
            where Items.IsDeleted = '0'
            group by AspNetUsers.UserName";

        using (var connection = new SqlConnection(_connectionString))
        {
            userReports = await connection.QueryAsync<UserReport>(sql, ct);
        }

        return userReports.ToList();
    }
}
