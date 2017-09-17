IF EXISTS (select 1 from Users u where u.Id = @Id)
BEGIN
    UPDATE
        Users
    SET
        [Password] = @Password,
        [Login] = @Login
    WHERE
        Id = @Id
END
ELSE BEGIN
    INSERT INTO Users 
        ([Login], [Password])
    VALUES
        (@Login, @Password)

    SET @Id = SCOPE_IDENTITY()
END

SELECT @Id