/* this is a comment */
-- Practice 2, 1a
SELECT CompanyName
FROM Customers
WHERE Country = "Finland"

-- Practice 2, 1b
SELECT *
FROM Orders
WHERE CustomerID = "QUEDE"

-- Practice 2, 1b with subquery
SELECT *
FROM Orders
WHERE CustomerID = (SELECT CustomerID
                    FROM Customers
                    WHERE CompanyName = "Que Delícia")

-- Practice 2, 1c
SELECT *
FROM Employees
WHERE City = "London" AND Country = "UK"

-- Practice 2, 2a
SELECT COUNT(*) AS "Customer Count"
FROM Customers

-- Practice 2, 2b
SELECT ProductName, UnitPrice, UnitsInStock, UnitPrice * UnitsInStock
FROM Products

SELECT SUM(UnitPrice * UnitsInStock)
FROM Products

-- Practice 2, 2c
SELECT *
FROM Products

SELECT SUM(UnitPrice * Quantity)
FROM "Order Details"
WHERE (ProductID = 14) OR (ProductID = 74)

-- taking discounts into account
SELECT SUM(UnitPrice * Quantity * (1-Discount))
FROM "Order Details"
WHERE (ProductID = 14) OR (ProductID = 74)

-- with a subquery
SELECT SUM(UnitPrice * Quantity * (1-Discount))
FROM "Order Details"
WHERE ProductID IN (SELECT ProductID
                    FROM Products
                    WHERE ProductName LIKE "%tofu%")
