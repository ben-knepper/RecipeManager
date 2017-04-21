-- takes a RecipeId
PREPARE GetRecipeIngredients FROM '
	SELECT PartText
	FROM RecipeParts
	WHERE RecipeId = ?
	ORDER BY PartNo';