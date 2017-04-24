DROP PROCEDURE IF EXISTS CreateRecipe;
DROP PROCEDURE IF EXISTS CreateRecipePart;
DROP PROCEDURE IF EXISTS CreateRecipePartWithRecipeId;

DELIMITER //

CREATE PROCEDURE CreateRecipe(
	IN r_name				VARCHAR(100),
	IN r_instructions		TEXT,
	IN r_image				VARCHAR(200),
	IN r_servings			INT,
	IN r_minutesToMake		INT)
BEGIN
	DECLARE r_id			INT;
	DECLARE r_sourceName	VARCHAR(50);

	-- use the lowest avialable id
	SELECT MIN(RecipeId) + 1 INTO r_id
	FROM (
		SELECT RecipeId
		FROM Recipes
			UNION
		SELECT 0 AS RecipeId) AS UsedIds
	WHERE UsedIds.RecipeId + 1 NOT IN (
		SELECT RecipeId
		FROM Recipes);

	SELECT Username INTO r_sourceName
	FROM CurrentUser;

	INSERT INTO Recipes VALUES (
		r_id,
		r_name,
		r_instructions,
		r_image,
		r_servings,
		r_sourceName,
		r_minutesToMake);

	-- save the recipe id for creating recipe parts
	SET @currentRecipe = r_id;
END; //

CREATE PROCEDURE CreateRecipePartWithRecipeId(
	IN r_id				INT,
	IN r_ingName		CHAR(50),
	IN r_partAmount		FLOAT,
	IN r_measureName	CHAR(20),
	IN r_partText		VARCHAR(50))
BEGIN
	DECLARE r_partNo	INT;

	-- use the next partNo
	SELECT MAX(PartNo) + 1 INTO r_partNo
	FROM RecipeParts
	WHERE RecipeId = r_id;

	INSERT INTO Recipes VALUES (
		r_id,
		r_partNo,
		r_ingName,
		r_partAmount,
		r_measureName,
		r_partText);
END; //

CREATE PROCEDURE CreateRecipePart(
	IN r_ingName		CHAR(50),
	IN r_partAmount		FLOAT,
	IN r_measureName	CHAR(20),
	IN r_partText		VARCHAR(50))
BEGIN
	DECLARE r_id		INT;
	
	CALL CreateRecipePartWithRecipeId(
		r_id,
		r_ingName,
		r_partAmount,
		r_measureName,
		r_partText);
END; //

DELIMITER ;

DROP PROCEDURE IF EXISTS AddToMyShoppingList;
DELIMITER //
CREATE PROCEDURE AddToMyShoppingList(IN i_name CHAR)
BEGIN
	DECLARE u_id		INT;
	DECLARE alreadyExists	INT;

	SET u_id = @currentUser;

	SELECT COUNT(*) INTO alreadyExists
	FROM ShoppingLists
	WHERE IngName = i_name;

	IF NOT alreadyExists THEN

		INSERT INTO ShoppingList (UserId, IngName)
		VALUES (u_id,i_name);
	END IF;
END; //
DELIMITER ;