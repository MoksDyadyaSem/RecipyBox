using RecipeBox.Dto.request;
using RecipeBox.Dto.response;
using RecipeBox.Services;

namespace RecipeBox.Api;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategoriesEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/categories").WithTags("Categories");

        group.MapGet("/", (ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(catalog.GetCategories().Select(mapper.Map)));

        group.MapGet("/stats", (ICatalogService catalog) =>
            Results.Ok(catalog.GetCategoryStatistics()));

        group.MapGet("/{id:int}", (int id, ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
        {
            var category = catalog.GetCategoryById(id);
            return category is null
                ? Results.NotFound(new ErrorResponse { Message = "категория не найдена" })
                : Results.Ok(mapper.Map(category));
        });

        group.MapPost("/", (CreateCategoryRequest req, ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
        {
            if (string.IsNullOrWhiteSpace(req.Name))
            {
                return Results.BadRequest(new ErrorResponse { Message = "название не может быть пустым" });
            }

            var created = catalog.AddCategory(req.Name);
            return Results.Created($"/api/categories/{created.Id}", mapper.Map(created));
        });

        group.MapDelete("/{id:int}", (int id, ICatalogService catalog) =>
        {
            var deleted = catalog.DeleteCategory(id);
            return deleted
                ? Results.Ok()
                : Results.NotFound(new ErrorResponse { Message = "категория не найдена" });
        });

        return api;
    }
}
