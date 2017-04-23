DROP PROCEDURE IF EXISTS CreateUserTables;
DROP PROCEDURE IF EXISTS CreateCurrentUserTable;
DROP PROCEDURE IF EXISTS CreateUserRecipeList;
DROP PROCEDURE IF EXISTS CreateUserPantry;
DROP PROCEDURE IF EXISTS CreateUserShoppingList;
DROP PROCEDURE IF EXISTS AddToUserRecipeList;

DELIMITER //

CREATE PROCEDURE CreateUserTables()
BEGIN
	CALL CreateCurrentUserTable();
	CALL CreateUserRecipeList();
	CALL CreateUserPantry();
	CALL CreateUserShoppingList();
END; //

CREATE PROCEDURE CreateCurrentUserTable()
BEGIN
	DROP TEMPORARY TABLE IF EXISTS CurrentUser;

	CREATE TEMPORARY TABLE CurrentUser AS
	SELECT UserId, Username
	FROM Users
	WHERE UserId = @currentUser;
END; //

CREATE PROCEDURE CreateUserRecipeList()
BEGIN
	DROP TEMPORARY TABLE IF EXISTS UserRecipeList;
	
	CREATE TEMPORARY TABLE UserRecipeList AS
	SELECT RecipeId, RecipeName, Image
	FROM RecipeLists NATURAL JOIN Recipes
	WHERE UserId = @currentUser;
END; //

CREATE PROCEDURE CreateUserPantry()
BEGIN
	DROP TEMPORARY TABLE IF EXISTS UserPantry;
	
	CREATE TEMPORARY TABLE UserPantry AS
	SELECT IngName, PantryAmount, MeasureName
	FROM Pantries
	WHERE UserId = @currentUser;
END; //

CREATE PROCEDURE CreateUserShoppingList()
BEGIN
	DROP TEMPORARY TABLE IF EXISTS UserShoppingList;
	
	CREATE TEMPORARY TABLE UserShoppingList AS
	SELECT IngName, PantryAmount, MeasureName
	FROM ShoppingLists
	WHERE UserId = @currentUser;
END; //

CREATE PROCEDURE AddToUserRecipeList(r_id INT)
BEGIN
	DECLARE u_id			INT;
	DECLARE alreadyExists	INT;

	SET u_id = @currentUser;

	SELECT COUNT(*) INTO alreadyExists
	FROM UserRecipeList
	WHERE RecipeId = r_id;

	IF NOT alreadyExists THEN

		INSERT INTO RecipeLists (UserId, RecipeId)
		VALUES (u_id, r_id);

		CALL CreateUserRecipeList();

	END IF;
END; //

CREATE PROCEDURE RemoveFromUserRecipeList(r_id INT)
BEGIN
	DECLARE u_id			INT;
	DECLARE doesExist		INT;

	SET u_id = @currentUser;

	SELECT COUNT(*) INTO doesExist
	FROM UserRecipeList
	WHERE RecipeId = r_id;

	IF doesExist THEN

		DELETE FROM RecipeLists
		WHERE UserId = u_id AND RecipeId = r_id;

		CALL CreateUserRecipeList();

	END IF;
END; //

DELIMITER ;