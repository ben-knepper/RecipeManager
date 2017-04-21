-- takes a search term
PREPARE SearchRecipeNames FROM '
	SELECT *
	FROM Recipes
	WHERE RecipeName LIKE CONCAT(\'%\', ?, \'%\')';