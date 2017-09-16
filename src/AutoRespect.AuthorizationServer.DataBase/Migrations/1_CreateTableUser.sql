-- <Migration ID="1a6c30bb-3daf-4eeb-a498-4a0ce25ec6e4" />
CREATE TABLE Users (
	Id INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Login varchar (64) NOT NULL,
	Password VARCHAR (32) NOT NULL
)

GO
