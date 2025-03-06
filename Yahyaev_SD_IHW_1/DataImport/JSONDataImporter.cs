using System.Text.Json;

namespace Yahyaev_SD_IHW_1.DataImport;

public class JSONDataImporter : DataImporter
{
    protected override List<Dictionary<string, string>> ParseData(string content)
    {
        var records = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(content);
        return records ?? new List<Dictionary<string, string>>();
    }
}