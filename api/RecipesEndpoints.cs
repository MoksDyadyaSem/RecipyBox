using System;
using System.Linq;
using RecipeBox.Dto.request;
using RecipeBox.Dto.response;
using RecipeBox.Repositories;
using RecipeBox.Services;

namespace RecipeBox.Api;

public static class RecipesEndpoints
{
    public static RouteGroupBuilder MapRecipesEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/recipes").WithTags("Recipes");

        group.MapGet("/", (ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(catalog.GetAllRecipes().Select(mapper.Map)));

        group.MapGet("/search", (string q, ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(catalog.SearchRecipes(q).Select(mapper.Map)));

        group.MapGet("/quick/{maxMinutes:int}", (int maxMinutes, ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(catalog.GetQuickRecipes(maxMinutes).Select(mapper.Map)));

        group.MapGet("/{id:int}", (int id, ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
        {
            var recipe = catalog.GetAllRecipes().FirstOrDefault(r => r.Id == id);
            return recipe is null
                ? Results.NotFound(new ErrorResponse { Message = "рецепт не найден" })
                : Results.Ok(mapper.Map(recipe));
        });

        group.MapPost("/", (CreateRecipeRequest req, ICatalogService catalog, RecipeBox.Dto.IMapper mapper) =>
        {
            try
            {
                var created = catalog.AddRecipe(req.CategoryId, req.Title, req.CookTimeMinutes, req.Price);
                return Results.Created($"/api/recipes/{created.Id}", mapper.Map(created));
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        group.MapDelete("/{id:int}", (int id, IRecipeRepository recipes) =>
        {
            var recipe = recipes.GetById(id);
            if (recipe is null)
            {
                return Results.NotFound(new ErrorResponse { Message = "рецепт не найден" });
            }

            recipes.Delete(id);
            return Results.Ok();
        });

        return api;
    }
}
