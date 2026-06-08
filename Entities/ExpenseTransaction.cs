namespace RecipeBox.Entities;

public class ExpenseTransaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string OccurredOn { get; set; } = string.Empty;

    public ExpenseTransaction() { }

    public ExpenseTransaction(string description, decimal amount, string occurredOn)
    {
        Description = description;
        Amount = amount;
        OccurredOn = occurredOn;
    }
}
