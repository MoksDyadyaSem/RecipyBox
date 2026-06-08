using System.Globalization;
using RecipeBox.Entities;
using RecipeBox.Repositories;

namespace RecipeBox.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseTransactionRepository _transactionRepository;

    public ExpenseService(IExpenseTransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public ExpenseTransaction AddTransaction(string description, decimal amount, string occurredOnText)
    {
        string normalizedDate = NormalizeDate(occurredOnText);
        ExpenseTransaction transaction = new ExpenseTransaction(description, amount, normalizedDate);
        _transactionRepository.Add(transaction);
        return transaction;
    }

    public ExpenseTransaction? GetTransactionById(int id)
    {
        return _transactionRepository.GetById(id);
    }

    public bool DeleteTransaction(int id)
    {
        var existing = _transactionRepository.GetById(id);
        if (existing is null)
        {
            return false;
        }

        _transactionRepository.Delete(id);
        return true;
    }

    public IReadOnlyList<ExpenseTransaction> GetAllTransactions()
    {
        return _transactionRepository.GetAll()
            .OrderByDescending(t => t.OccurredOn)
            .ToList();
    }

    public IReadOnlyList<ExpenseTransaction> GetRecentTransactions(int count)
    {
        return _transactionRepository.GetAll()
            .OrderByDescending(t => t.OccurredOn)
            .Take(count)
            .ToList();
    }

    // чтобы дату можно было кидать как угодно, а в базе всегда yyyy-MM-dd
    private static string NormalizeDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd");
        }

        if (DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsed))
        {
            return parsed.ToString("yyyy-MM-dd");
        }

        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var invariant))
        {
            return invariant.ToString("yyyy-MM-dd");
        }

        return DateTime.UtcNow.ToString("yyyy-MM-dd");
    }
}
