namespace RecipeBox.Entities;

public class Recipe
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int CookTimeMinutes { get; set; }
    public decimal Price { get; set; }

    public Category? Category { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();

    public Recipe() { }

    public Recipe(int categoryId, string title, int cookTimeMinutes, decimal price)
    {
        CategoryId = categoryId;
        Title = title;
        CookTimeMinutes = cookTimeMinutes;
        Price = price;
    }
}
