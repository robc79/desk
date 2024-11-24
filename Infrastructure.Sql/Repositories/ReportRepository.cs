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

    public async Task<List<ItemReport>> GetItemReportsAsync(CancellationToken ct)
    {
        IEnumerable<ItemReport> itemReports;
        
        const string sql = @"
            select
                AspNetUsers.UserName as Username,
                count(Items.Id) as ItemCount,
                di.DeletedCount,
                count(Items.ImageName) as ImageCount
            from Items
            inner join AspNetUsers on Items.OwnerId = AspNetUsers.Id
            inner join (select OwnerId, count(OwnerId) as DeletedCount from Items where IsDeleted = '1' group by OwnerId) di on Items.OwnerId = di.OwnerId
            group by AspNetUsers.UserName, di.DeletedCount
            order by AspNetUsers.UserName";

        using (var connection = new SqlConnection(_connectionString))
        {
            itemReports = await connection.QueryAsync<ItemReport>(sql, ct);
        }

        return itemReports.ToList();
    }
}
