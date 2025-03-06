using Yahyaev_SD_IHW_1.Domain;

namespace Yahyaev_SD_IHW_1.Interfaces;

public interface IVisitor
{
    void Visit(BankAccount account);
    void Visit(Operation operation);
    void Visit(Category category);
}