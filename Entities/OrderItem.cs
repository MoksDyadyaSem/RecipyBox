namespace RecipeBox.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int RecipeId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Order? Order { get; set; }
    public Recipe? Recipe { get; set; }

    public OrderItem() { }

    public OrderItem(int recipeId, int quantity, decimal unitPrice)
    {
        RecipeId = recipeId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal LineTotal => UnitPrice * Quantity;
}
