using Yahyaev_SD_IHW_1.Facade;

namespace Yahyaev_SD_IHW_1.Timing;

public class CreateAccountCommand : ICommand
{
    private readonly FinanceFacade _facade;
    private readonly string _accountName;

    public CreateAccountCommand(FinanceFacade facade, string accountName)
    {
        _facade = facade;
        _accountName = accountName;
    }

    public void Execute()
    {
        var account = _facade.CreateBankAccount(_accountName);
        Console.WriteLine($"Создан аккаунт: {account.Id} - {account.Name}.");
    }
}