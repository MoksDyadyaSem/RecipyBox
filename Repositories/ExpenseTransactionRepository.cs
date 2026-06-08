using Microsoft.EntityFrameworkCore;
using RecipeBox.database;
using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public class ExpenseTransactionRepository : IExpenseTransactionRepository
{
    private readonly RecipeBoxDbContext _db;

    public ExpenseTransactionRepository(RecipeBoxDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<ExpenseTransaction> GetAll()
    {
        return _db.ExpenseTransactions.AsNoTracking().ToList();
    }

    public ExpenseTransaction? GetById(int id)
    {
        return _db.ExpenseTransactions.FirstOrDefault(transaction => transaction.Id == id);
    }

    public void Add(ExpenseTransaction transaction)
    {
        _db.ExpenseTransactions.Add(transaction);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var transaction = _db.ExpenseTransactions.Find(id);
        if (transaction != null)
        {
            _db.ExpenseTransactions.Remove(transaction);
            _db.SaveChanges();
        }
    }
}
