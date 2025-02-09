using Microsoft.EntityFrameworkCore;
using SqlOfTheDead.Models;
using SqlOfTheDead.Routes;

namespace SqlOfTheDead.Services;

public class ServiceTableServer : IZombieTable
{
    DatabaseContext _db;
    public ServiceTableServer(DatabaseContext db) => _db = db;

    #region GET
    public async Task<List<ZombieTable>> GetTables()
    {
        var tables = await _db.ZombieTable.AsNoTracking().ToListAsync();
        foreach(var table in tables)
        {
            table.Fields = await _db.ZombieField.Where(w => w.TableId == table.Id).ToListAsync();
            table.Indexes = await _db.ZombieIndex.Where(w => w.TableId == table.Id).ToListAsync();
            foreach (var index in table.Indexes)
            {
                index.Fields = await _db.ZombieIndexField.Where(w => w.IndexId == index.Id).ToListAsync();
            }
        }

        return tables;
    }

    public async Task<HashSet<Guid>> GetTableIds()
    {
        return await _db.ZombieTable
                        .AsNoTracking()
                        .Select(s => s.Id)
                        .ToHashSetAsync();
    }
    #endregion

    #region POST
    public async Task<int> AddTable(ZombieTable table)
    {
        if (_db.ZombieTable.Any(a => a.Id == table.Id)) await DeleteTable(table.Id);
        _db.ZombieTable.Add(table);
        return await _db.SaveChangesAsync();
    }
    #endregion

    #region DELETE
    public async Task<int> DeleteTable(Guid tableId)
    {
        var table = await _db.ZombieTable.FirstOrDefaultAsync(f => f.Id == tableId);
        if (table is null) return 0;

        var fields = await _db.ZombieField.Where(w => w.TableId == tableId).ToListAsync();
        var indexes = await _db.ZombieIndex.Where(w => w.TableId == tableId).ToListAsync();
        var indexIds = indexes.Select(s => s.Id).ToHashSet();
        var indexFields = await _db.ZombieIndexField.Where(w => indexIds.Contains(w.IndexId)).ToListAsync();

        _db.ZombieIndexField.RemoveRange(indexFields);
        _db.ZombieIndex.RemoveRange(indexes);
        _db.ZombieField.RemoveRange(fields);
        _db.ZombieTable.Remove(table);

        return await _db.SaveChangesAsync();
    }
    #endregion
}
