using SqlOfTheDead.Models;

namespace SqlOfTheDead.Routes;

public class RouteTable
{
    public static async Task<IResult> GetTables(IZombieTable data)
    {
        var tables = await data.GetTables();
        return Results.Ok(tables);
    }

    public static async Task<IResult> GetTableIds(IZombieTable data)
    {
        return Results.Ok(await data.GetTableIds());
    }

    public static async Task<IResult> AddTable(IZombieTable data, ZombieTable table)
    {
        var http = await data.AddTable(table);
        return Results.Ok(http);
    }

    public static async Task<IResult> DeleteTable(IZombieTable data, Guid tableId)
    {
        var http = await data.DeleteTable(tableId);
        return Results.Ok(http);
    }
}
