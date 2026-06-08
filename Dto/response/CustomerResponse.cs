namespace RecipeBox.Dto.response;

public sealed record CustomerResponse(int Id, string FullName, string Phone, DateTime RegisteredAt);
