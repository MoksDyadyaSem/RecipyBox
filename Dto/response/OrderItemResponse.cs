namespace RecipeBox.Dto.response;

public sealed record OrderItemResponse(
    int RecipeId,
    string RecipeTitle,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal);
