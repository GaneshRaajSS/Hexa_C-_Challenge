CREATE DATABASE OrderManagementSystem

USE OrderManagementSystem

CREATE TABLE Product(
productId INT PRIMARY KEY IDENTITY(1,1),
productName VARCHAR(255),
description VARCHAR(255),
price DECIMAL(10,2),
quantityInStock INT,
type VARCHAR(50)CHECK (type IN ('Electronics', 'Clothing')) NOT NULL
)

CREATE TABLE Electronics (
    productId INT PRIMARY KEY,
    brand VARCHAR(255) NOT NULL,
    warrantyPeriod INT NOT NULL,
    FOREIGN KEY (productId) REFERENCES Product(productId)
)

CREATE TABLE Clothing (
    productId INT PRIMARY KEY,
    size VARCHAR(50) NOT NULL,
    color VARCHAR(50) NOT NULL,
    FOREIGN KEY (productId) REFERENCES Product(productId)
)

CREATE TABLE [User] (
    userId INT PRIMARY KEY IDENTITY(100,1),
    username VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(50) CHECK (role IN ('Admin', 'User')) NOT NULL
)

INSERT INTO Product VALUES ('Smartphone', 'Latest model smartphone', 8999.99, 50, 'Electronics'),
('T-shirt', 'Cotton T-shirt', 399.49, 150, 'Clothing')
SELECT * FROM Product

INSERT INTO Electronics VALUES (1, 'TechBrand', 6)
SELECT * FROM Electronics

INSERT INTO Clothing VALUES (2, 'M', 'Red')
SELECT * FROM Clothing

INSERT INTO [User] VALUES ('adminUser', 'Admin123', 'Admin'),
('regularUser', 'User123', 'User')
SELECT * FROM [User]
