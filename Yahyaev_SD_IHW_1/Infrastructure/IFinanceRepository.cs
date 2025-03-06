using Yahyaev_SD_IHW_1.Domain;

namespace Yahyaev_SD_IHW_1.Infrastructure;

public interface IFinanceRepository
{
    void AddBankAccount(BankAccount bankAccount);
    List<BankAccount> GetBankAccounts();
    void UpdateBankAccount(BankAccount bankAccount);
    void RemoveBankAccount(Guid bankId);
    
    void AddCategory(Category category);
    List<Category> GetCategories();
    void UpdateCategory(Category category);
    void RemoveCategory(Guid categoryId);
    
    void AddOperation(Operation operation);
    List<Operation> GetOperations();
    void UpdateOperation(Operation operation);
    void RemoveOperation(Guid operationId);
}