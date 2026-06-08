namespace RecipeBox.Dto.response;

public sealed record OrderResponse(
    int Id,
    int CustomerId,
    string CustomerName,
    DateTime CreatedAt,
    string Status,
    decimal TotalAmount,
    IReadOnlyList<OrderItemResponse> Items);
