namespace RecipeBox.Dto.request;

public sealed record CreateRecipeRequest(int CategoryId, string Title, int CookTimeMinutes, decimal Price);
