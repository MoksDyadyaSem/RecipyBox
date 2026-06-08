namespace RecipeBox.Dto.request;

public sealed record CreateOrderRequest(int CustomerId, IReadOnlyList<CreateOrderItemRequest> Items);
