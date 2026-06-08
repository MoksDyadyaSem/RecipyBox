using RecipeBox.Entities;

namespace RecipeBox.Services;

public interface IExpenseService
{
    IReadOnlyList<ExpenseTransaction> GetAllTransactions();
    ExpenseTransaction? GetTransactionById(int id);
    IReadOnlyList<ExpenseTransaction> GetRecentTransactions(int count);
    ExpenseTransaction AddTransaction(string description, decimal amount, string occurredOnText);
    bool DeleteTransaction(int id);
}
