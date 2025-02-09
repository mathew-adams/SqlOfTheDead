using SqlOfTheDead.Models;
using System.Net.Http.Json;

namespace SqlOfTheDead.Client.Pages.Table;

public class ServiceTableClient : IZombieTable
{
    private HttpClient _http;
    public ServiceTableClient(HttpClient http) => _http = http;

    #region GET
    public async Task<HashSet<Guid>> GetTableIds()
    {
        var ids = await _http.GetFromJsonAsync<HashSet<Guid>>("api/table/id");
        return ids ?? [];
    }

    public async Task<List<ZombieTable>> GetTables()
    {
        var tables = await _http.GetFromJsonAsync<List<ZombieTable>>("/api/table");
        return tables ?? [];
    }
    #endregion

    #region POST
    public async Task<int> AddTable(ZombieTable table)
    {
        var result = await _http.PostAsJsonAsync<ZombieTable>("api/table", table);
        return 0;
    }
    #endregion

    #region DELETE
    public async Task<int> DeleteTable(Guid tableId)
    {
        var result = await _http.DeleteAsync($"/api/table/{tableId}");
        result.EnsureSuccessStatusCode();
        return (int)result.StatusCode;
    }
    #endregion
}
