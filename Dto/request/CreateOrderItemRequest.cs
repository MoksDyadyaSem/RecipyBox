namespace RecipeBox.Dto.request;

public sealed record CreateOrderItemRequest(int RecipeId, int Quantity);
