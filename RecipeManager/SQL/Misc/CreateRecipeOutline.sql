-- complete steps for creating a recipe

START TRANSACTION;

-- create recipe with given parameters
CALL CreateRecipe(name, instructions, image, servings, minutesToMake);

-- foreach recipe part
	-- uses recipe from CreateRecipe
	CALL CreateRecipePart(ingName, partAmount, measureName, partText);
-- end foreach

COMMIT;