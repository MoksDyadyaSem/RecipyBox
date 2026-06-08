namespace RecipeBox.Dto.response;

public sealed record RecipeResponse(
    int Id,
    int CategoryId,
    string CategoryName,
    string Title,
    int CookTimeMinutes,
    decimal Price);
