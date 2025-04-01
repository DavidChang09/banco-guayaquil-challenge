-- Crear la base de datos
CREATE DATABASE ms_products;
GO

-- Cambiar al contexto de la base de datos
USE ms_products;
GO

-- Crear un schema llamado 'catalog'
CREATE SCHEMA catalog;
GO

-- Crear tabla Products
CREATE TABLE catalog.Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Price DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- Insertar datos demo
INSERT INTO catalog.Products (Name, Description, Price, Stock)
VALUES 
('Laptop', 'Laptop de 15 pulgadas', 1200.00, 10),
('Mouse', 'Mouse inalámbrico', 25.50, 50),
('Teclado', 'Teclado mecánico', 75.99, 20),
('Monitor', 'Monitor 24 pulgadas', 200.00, 15);
GO
