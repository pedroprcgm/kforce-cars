CREATE TABLE Customers (
    FirstName varchar(100),
    LastName varchar(100),
    Age int,
    Occupation varchar(100),
    MaritalStatus varchar(100),
    PersonId bigint
    PRIMARY KEY (PersonId));

INSERT INTO Customers VALUES
    ('WALTER', 'WHITE', 65, 'HIGH SCHOOL TEACHER', 'MARRIED', 1),
    ('JESSE', 'PINKMAN', 39, 'NONE', 'SINGLE', 2),
    ('HANK', 'SCHRADER', 57, 'POLICE OFFICER', 'MARRIED', 3);


CREATE TABLE Orders (
    OrderId bigint,
    PersonId bigint,
    DateCreated DateTime,
    MethodOfPurchase varchar(100),
    PRIMARY KEY (OrderId),
    FOREIGN KEY (PersonId) REFERENCES Customers(PersonId));

INSERT INTO Orders VALUES
    -- WW orders
    (1, 1, '2023-02-23', 'CREDIT CARD'),
    (2, 1, '2023-02-23', 'CREDIT CARD'),
    (3, 1, '2023-02-23', 'DIGITAL WALLET'),

    -- JP orders
    (4, 2, '2023-02-23', 'CREDIT CARD'),
    (5, 2, '2023-02-23', 'CREDIT CARD'),
    (6, 2, '2023-02-23', 'CREDIT CARD'),

    -- HS orders
    (7, 3, '2023-02-23', 'DIGITAL WALLET'),
    (8, 3, '2023-02-23', 'CREDIT CARD'),
    (9, 3, '2023-02-23', 'CREDIT CARD');

CREATE TABLE OrderDetails (
    OrderDetailId bigint,
    OrderId bigint,
    ProductNumber int,
    ProductId bigint,
    ProductOrigin varchar(100),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId));

INSERT INTO OrderDetails VALUES
    -- WW orders
    (1, 1, 1, 1112222333, 'USA'),
    (2, 1, 1, 123, 'USA'),
    (3, 2, 1, 123, 'USA'),
    (4, 2, 1, 1234, 'BRZ'),
    (5, 3, 1, 12345, 'BRZ'),
    (6, 3, 1, 12345, 'BRZ'),

    -- JP orders
    (7, 4, 1, 1112222333, 'USA'),
    (8, 5, 1, 12345, 'BRZ'),
    (9, 6, 1, 12345, 'BRZ'),
    
    -- HS orders
    (10, 7, 1, 123, 'USA'),
    (11, 8, 1, 123456, 'USA'),
    (12, 9, 1, 1234567, 'USA');

SELECT
    CONCAT(C.FirstName, ' ', C.LastName) AS 'Full name',
        C.Age as Age,
    O.OrderId as OrderId,
    O.DateCreated as DateCreated,
    O.MethodOfPurchase as 'Purchase method',
        OD.ProductNumber as ProductNumber,
    OD.ProductId as ProductId
FROM OrderDetails OD
INNER JOIN Orders O ON O.Orderid = OD.OrderId
INNER JOIN Customers C on C.PersonId = O.PersonId
WHERE ProductId = 1112222333;
