-- gets the ingredient texts for a particular recipe
SELECT PartText
FROM RecipeParts
WHERE RecipeId = ?recipeId
ORDER BY PartNo;

-- gets all recipes created by the current user
SELECT RecipeId, RecipeName
FROM Recipes
WHERE SourceName IN (
	SELECT Username
	FROM CurrentUser);