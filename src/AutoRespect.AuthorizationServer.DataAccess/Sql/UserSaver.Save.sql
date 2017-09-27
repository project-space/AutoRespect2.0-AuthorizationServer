IF EXISTS (select 1 from Account where Id = @Id)
BEGIN
    UPDATE
        Account
    SET
        Password = @Password,
        Login = @Login
    WHERE
        Id = @Id
END
ELSE BEGIN
    INSERT INTO Account
        (Login, Password)
    VALUES
        (@Login, @Password)

    SET @Id = SCOPE_IDENTITY()
END

SELECT @Id