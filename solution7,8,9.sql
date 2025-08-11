USE Week4CodingAssessment

--7

CREATE TABLE Worker (
    WORKER_ID INT PRIMARY KEY,
    FIRST_NAME CHAR(25) NOT NULL,
    LAST_NAME CHAR(25),
    SALARY INT CHECK (SALARY BETWEEN 10000 AND 25000),
    JOINING_DATE DATETIME,
    DEPARTMENT CHAR(25) CHECK (DEPARTMENT IN ('HR', 'Sales', 'Accts', 'IT'))
)

INSERT INTO Worker (WORKER_ID, FIRST_NAME, LAST_NAME, SALARY, JOINING_DATE, DEPARTMENT)
VALUES
(1, 'Rohit', 'Sharma', 15000, '2023-01-15', 'HR'),
(2, 'Vijay', 'Kumar', 20000, '2022-11-10', 'Sales'),
(3, 'David', 'Warner', 24000, '2024-03-05', 'Accts'),
(4, 'Virat', 'Kohli', 18000, '2021-08-20', 'IT');


SELECT * FROM Worker;



--8

CREATE TABLE Employee (
    employee_id INT PRIMARY KEY,
    name VARCHAR(50),
    exp INT,
    salary INT,
    departmentName VARCHAR(50)
)

CREATE PROCEDURE InsertOrUpdateEmployee
    @id INT,
    @name VARCHAR(50),
    @exp INT,
    @salary INT,
    @departmentName VARCHAR(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Employee WHERE employee_id = @id)
    BEGIN
        UPDATE Employee
        SET name = @name, exp = @exp, salary = @salary, departmentName = @departmentName
        WHERE employee_id = @id;
        PRINT 'Record updated successfully'
    END
    ELSE
    BEGIN
        INSERT INTO Employee VALUES(@id, @name, @exp, @salary, @departmentName);
        PRINT 'Record inserted successfully'
    END
END


EXEC InsertOrUpdateEmployee 
    @id = 101, 
    @name = 'Sanjay', 
    @exp = 5, 
    @salary = 20000, 
    @departmentName = 'IT'



EXEC InsertOrUpdateEmployee 
    @id = 101, 
    @name = 'Kamal', 
    @exp = 6, 
    @salary = 22000, 
    @departmentName = 'IT'


SELECT * FROM Employee;



--9

CREATE PROCEDURE DeleteEmployee
    @id INT
AS
BEGIN
    DELETE FROM Employee WHERE employee_id = @id;
    PRINT 'Record deleted successfully';
END


EXEC DeleteEmployee @id=101

SELECT * FROM Employee