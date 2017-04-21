-- takes a search term
PREPARE SearchRecipeNames FROM '
	SELECT *
	FROM Recipes
	WHERE RecipeName LIKE CONCAT(\'%\', ?, \'%\')';

-- search by ingredient
SELECT *
FROM Recipes
WHERE ?ingName? IN (
	SELECT IngName
	FROM RecipeParts
	WHERE RecipeParts.RecipeId = Recipes.RecipeId);