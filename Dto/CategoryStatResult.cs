namespace RecipeBox.Dto;

public class CategoryStatResult
{
    public string CategoryName { get; set; } = "";

    public int RecipeCount { get; set; }

    public int AvgCookMinutes { get; set; }

    public decimal TotalPrice { get; set; }
}
