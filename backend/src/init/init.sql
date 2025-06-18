-- ++++++++++++++++++++++ CREATE DB and Tables / Fields ++++++++++++++++++++++

USE master;
GO

-- Drop the database if it already exists (optional and potentially destructive)
IF EXISTS (
      SELECT name
      FROM sys.databases
      WHERE name = N'LoanDB'
      )
BEGIN
    ALTER DATABASE LoanDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LoanDB;
END
GO

-- Create the database
CREATE DATABASE LoanDB;
GO

-- Use the new database
USE LoanDB;
GO

-- Create Address table
CREATE TABLE Address (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Street NVARCHAR(255),
    City NVARCHAR(100),
    PostalCode NVARCHAR(20)
);
GO

-- Create Customer table
CREATE TABLE Customer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Email NVARCHAR(255),
    AddressId INT,
    FOREIGN KEY (AddressId) REFERENCES Address(Id)
);
GO

-- Create Loan table
CREATE TABLE Loan (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    Amount DECIMAL(18,2),
    LoanDate DATETIME,
    FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
);
GO

-- Create Payment table
CREATE TABLE Payment (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LoanId INT, 
    Amount DECIMAL(18,2),
    Status NVARCHAR(50),
    PaymentDate DATETIME,
    FOREIGN KEY (LoanId) REFERENCES Loan(Id)
);
GO

-- ++++++++++++++***+++++++++++++ INSERT SEED Data +++++++++++++++***++++++++++++

-- Use the correct database

-- Insert sample addresses
INSERT INTO Address (Street, City, PostalCode)
VALUES 
    ('1 El Barril ', 'La Vecindad', '45040'),
    ('123 Main St', 'New York', '10001'),
    ('456 Oak Ave', 'Los Angeles', '90001'),
    ('789 Pine Rd', 'Chicago', '60601');
    
-- Insert sample customers
INSERT INTO Customer (Name, Email, AddressId)
VALUES 
    ('El Chavo del Ocho', 'elchavo@example.com', 1),
    ('Don Ramon', 'ramon@example.com', 2),
    ('Profesor Jirafales', 'jirafales@example.com', 3),
    ('El Kiko', 'kiko@example.com', 4),
    ('La Chilindrina', 'Chilindrina@example.com', 4);

-- Insert sample loans
INSERT INTO Loan (CustomerId, Amount, LoanDate)
VALUES 
    (1, 5000.00, '2025-01-15'),
    (2, 10000.00, '2025-03-10'),
    (1, 3000.00, '2025-04-01'),
    (3, 1800.00, '2025-05-01');

-- Insert sample payments
INSERT INTO Payment (LoanId, Amount, Status, PaymentDate)
VALUES 
    (1, 1000.00, 'active', '2025-02-01'),
    (1, 500.00, 'active', '2025-02-15'),
    (2, 2000.00, 'active', '2025-04-01'),
    (3, 1500.00, 'active', '2025-06-01'),
    (4, 1800.00, 'active', '2025-06-03');