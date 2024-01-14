IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'iShopDB')
BEGIN
    DROP DATABASE iShopDB;
END
ELSE
BEGIN
    PRINT 'Database does not exist.';
END


CREATE DATABASE iShopDB
GO
USE iShopDB
GO
CREATE TABLE ProductTbl(
    Id INT IDENTITY(1,1) primary key,
    ProductName VARCHAR(50) NOT NULL,
	Description VARCHAR(500),
	ImageName VARCHAR(100),
	Category VARCHAR(50),
	Price FLOAT,
	Discount FLOAT,
	Stock INT
);

CREATE TABLE UserCredentialsTbl(
	Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(50) UNIQUE NOT NULL,
	UserPassword VARCHAR(100) NOT NULL,
);

CREATE TABLE UserAddressTbl(
	Id INT PRIMARY KEY,
	IsPrimary BIT NOT NULL,
	UserId INT NOT NULL,
    Line1 VARCHAR(100),
	Line2 VARCHAR(100),
	Line3 VARCHAR(100),
	Line4 VARCHAR(100),
	City VARCHAR(20),
	Country VARCHAR(20),
	Postcode VARCHAR(20),
	FOREIGN KEY(UserId) REFERENCES UserCredentialsTbl(Id),

);

CREATE TABLE UserDetailsTbl(
	UserId INT PRIMARY KEY,
    FirstName VARCHAR(50),
	LastName VARCHAR(50),
	MiddleNames VARCHAR(50),
	Email VARCHAR(100),
	PrimaryAddressId INT,
	FOREIGN KEY(UserId) REFERENCES UserCredentialsTbl(Id),
	FOREIGN KEY(PrimaryAddressId) REFERENCES UserAddressTbl(Id)
);

CREATE TABLE CartTbl(
    Id INT IDENTITY(1,1) primary key,
    ProductId INT,
	CustomerId INT,
	Quantity INT,
	FOREIGN KEY(CustomerId) REFERENCES UserCredentialsTbl(Id),
	FOREIGN KEY(ProductId) REFERENCES ProductTbl(Id),
);


INSERT INTO ProductTbl (ProductName, ImageName, Description, Category, Price, Discount, Stock)
VALUES
    ('Rolex-1', 'image1.jpg', 'sexy watch', 'watch', 50.00, 0.00, 50),
    ('Nike-1', 'image2.jpg', 'sexy watch', 'bracelet', 75.00, 0.10, 50),
    ('Rolex-2', 'image3.jpg', 'sexy watch', 'watch', 100.00, 0.20, 50),
    ('HotShot-1', 'image4.jpg', 'sexy watch', 'belt', 120.00, 0.15, 50),
	('Rolex-1', 'image1.jpg', 'sexy watch', 'watch', 50.00, 0.00, 50),
    ('lovely-sunglass', 'image2.jpg', 'sexy watch', 'sunglass', 75.00, 0.10, 50),
    ('Rolex-2', 'image3.jpg', 'sexy watch', 'watch', 100.00, 0.20, 50),
    ('HotShot-1', 'image4.jpg', 'sexy watch', 'belt', 120.00, 0.15, 50);


PRINT 'Database recreated and reseeded.';