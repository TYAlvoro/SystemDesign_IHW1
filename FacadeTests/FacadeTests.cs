using Yahyaev_SD_IHW_1.Domain;
using Yahyaev_SD_IHW_1.Facade;
using Yahyaev_SD_IHW_1.Infrastructure;
using Yahyaev_SD_IHW_1.Observer;

namespace FacadeTests;

public class FinanceFacadeTests
{
    private readonly FinanceFacade _facade;
    private readonly IFinanceRepository _repository;
    private readonly TestObserver _observer;

    public FinanceFacadeTests()
    {
        _repository = new FinanceRepository();
        // Создаем тестового наблюдателя
        _observer = new TestObserver();
        var observers = new List<IBankAccountObserver> { _observer };
        // Инициализируем фасад с репозиторием и наблюдателями
        _facade = new FinanceFacade(_repository, observers);
    }

    [Fact]
    public void CreateBankAccount_ShouldCreateAccountAndTriggerObserver()
    {
        // Act
        var account = _facade.CreateBankAccount("Test Account");

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account", account.Name);

        // Изменяем баланс, чтобы проверить, что наблюдатель сработал
        int initialInvocations = _observer.InvocationCount;
        account.Balance += 100;
        Assert.True(_observer.InvocationCount > initialInvocations, "Наблюдатель не вызвался при изменении баланса.");
    }

    [Fact]
    public void CreateOperation_ShouldUpdateBalanceImmediately_ForIncome()
    {
        // Arrange
        var account = _facade.CreateBankAccount("Test Account");
        var category = _facade.CreateCategory(Category.CategoryType.Income, "Salary");
        decimal initialBalance = account.Balance;

        // Act
        _facade.CreateOperation(Operation.OperationType.Income, account, 500m, DateTime.Now, "Salary Payment", category);

        // Assert
        Assert.Equal(initialBalance + 500m, account.Balance);
    }

    [Fact]
    public void CreateOperation_ShouldUpdateBalanceImmediately_ForExpense()
    {
        // Arrange
        var account = _facade.CreateBankAccount("Test Account");
        var category = _facade.CreateCategory(Category.CategoryType.Expense, "Food");
        decimal initialBalance = account.Balance;

        // Act
        _facade.CreateOperation(Operation.OperationType.Expense, account, 300m, DateTime.Now, "Dinner", category);

        // Assert
        Assert.Equal(initialBalance - 300m, account.Balance);
    }

    [Fact]
    public void UpdateOperation_ShouldRecalculateBalance()
    {
        // Arrange
        var account = _facade.CreateBankAccount("Test Account");
        var category = _facade.CreateCategory(Category.CategoryType.Expense, "Food");
        // Создаем операцию-расход, которая уменьшает баланс на 200
        var op = _facade.CreateOperation(Operation.OperationType.Expense, account, 200m, DateTime.Now, "Dinner", category);
        Assert.Equal(-200m, account.Balance);

        // Act: обновляем операцию – новый расход 300 вместо 200
        var updatedOperation = new Operation(op.Type, op.BankAccount, 300m, op.Date, op.Description, op.Category)
        {
            Id = op.Id // сохраняем идентификатор, чтобы обновить ту же операцию
        };
        _facade.UpdateOperation(updatedOperation);

        // Assert: баланс должен быть пересчитан (ожидаем -300)
        Assert.Equal(-300m, account.Balance);
    }

    [Fact]
    public void RemoveOperation_ShouldReverseEffectOnBalance()
    {
        // Arrange
        var account = _facade.CreateBankAccount("Test Account");
        var category = _facade.CreateCategory(Category.CategoryType.Expense, "Food");
        var op = _facade.CreateOperation(Operation.OperationType.Expense, account, 150m, DateTime.Now, "Snack", category);
        Assert.Equal(-150m, account.Balance);

        // Act: удаляем операцию
        _facade.RemoveOperation(op.Id);

        // Assert: баланс должен быть восстановлен (в данном случае до 0)
        Assert.Equal(0m, account.Balance);
    }

    // Вспомогательный класс-тестовый наблюдатель
    private class TestObserver : IBankAccountObserver
    {
        public int InvocationCount { get; private set; } = 0;

        public void HandleBalanceChanged(object sender, EventArgs e)
        {
            InvocationCount++;
        }
    }
}