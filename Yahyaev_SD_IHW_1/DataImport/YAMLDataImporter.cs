namespace Yahyaev_SD_IHW_1.DataImport;

public class YAMLDataImporter : DataImporter
{
    protected override List<Dictionary<string, string>> ParseData(string content)
    {
        var result = new List<Dictionary<string, string>>();
        // Допустим, что записи разделены строкой "---"
        var records = content.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
            
        foreach (var rec in records)
        {
            var record = new Dictionary<string, string>();
            // Каждая запись содержит строки вида "ключ: значение"
            var lines = rec.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    record[key] = value;
                }
            }
            if (record.Count > 0)
                result.Add(record);
        }
        return result;
    }
}