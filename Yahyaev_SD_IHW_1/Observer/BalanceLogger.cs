using Yahyaev_SD_IHW_1.Domain;

namespace Yahyaev_SD_IHW_1.Observer;

public class BalanceLogger : IBankAccountObserver
{
    public void HandleBalanceChanged(object sender, EventArgs e)
    {
        if (sender is BankAccount account)
        {
            Console.WriteLine($"[INFO] [Observer] Баланс счета {account.Name} обновлен: {account.Balance}.");
        }
    }
}