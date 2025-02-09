using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOfTheDead.Models;

public interface IZombieTable
{
    public Task<List<ZombieTable>> GetTables();
    public Task<HashSet<Guid>> GetTableIds();
    public Task<int> AddTable(ZombieTable table);
    public Task<int> DeleteTable(Guid tableId);
}
