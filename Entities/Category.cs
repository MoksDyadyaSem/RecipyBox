namespace RecipeBox.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Category() { }

    public Category(string name)
    {
        Name = name;
    }
}
