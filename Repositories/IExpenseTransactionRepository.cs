using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public interface IExpenseTransactionRepository
{
    IReadOnlyList<ExpenseTransaction> GetAll();
    ExpenseTransaction? GetById(int id);
    void Add(ExpenseTransaction transaction);
    void Delete(int id);
}
