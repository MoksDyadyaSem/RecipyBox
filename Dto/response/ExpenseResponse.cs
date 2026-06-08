namespace RecipeBox.Dto.response;

public sealed record ExpenseResponse(int Id, string Description, decimal Amount, string OccurredOn);
