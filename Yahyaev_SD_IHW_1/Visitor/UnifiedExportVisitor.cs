using Yahyaev_SD_IHW_1.Domain;
using Yahyaev_SD_IHW_1.Interfaces;

namespace Yahyaev_SD_IHW_1.Visitor;

public class UnifiedExportVisitor : IVisitor
{
    public List<BankAccount> Accounts { get; } = new List<BankAccount>();
    public List<Category> Categories { get; } = new List<Category>();
    public List<Operation> Operations { get; } = new List<Operation>();

    public void Visit(BankAccount account)
    {
        Accounts.Add(account);
    }

    public void Visit(Category category)
    {
        Categories.Add(category);
    }

    public void Visit(Operation operation)
    {
        Operations.Add(operation);
    }
}