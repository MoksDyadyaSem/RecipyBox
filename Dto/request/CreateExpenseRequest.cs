namespace RecipeBox.Dto.request;

public sealed record CreateExpenseRequest(string Description, decimal Amount, string Date);
