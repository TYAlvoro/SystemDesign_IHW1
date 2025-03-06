using System.Text.Json;
using Yahyaev_SD_IHW_1.Interfaces;
using Yahyaev_SD_IHW_1.Visitor;

namespace Yahyaev_SD_IHW_1.DataExport;

public class JSONDataExporter : DataExporter
{
    protected override IVisitor CreateVisitor()
    {
        return new UnifiedExportVisitor();
    }

    protected override string FormatData(IVisitor visitor)
    {
        var jsonVisitor = visitor as UnifiedExportVisitor;
        if (jsonVisitor == null)
            throw new InvalidOperationException("Неверный тип посетителя");

        var exportData = new
        {
            BankAccounts = jsonVisitor.Accounts,
            Categories = jsonVisitor.Categories,
            Operations = jsonVisitor.Operations
        };
        
        return JsonSerializer.Serialize(exportData, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
        });
    }
}