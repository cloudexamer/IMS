CREATE TABLE dbo.Rolodex
(
    Oid INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    FirstName NVARCHAR(100) NULL,
    LastName  NVARCHAR(100) NULL,
    Phone     NVARCHAR(50)  NULL,
    Email     NVARCHAR(150) NULL
);

INSERT INTO Rolodex (FirstName, LastName, Phone, Email)
VALUES
('David', 'Jason', '555-100-0001', 'david.jason@example.com'),
('Maria', 'Garcia', '555-100-0002', 'maria.garcia@example.com'),
('John', 'Smith', '555-100-0003', 'john.smith@example.com'),
('Lisa', 'Johnson', '555-100-0004', 'lisa.johnson@example.com'),
('Michael', 'Brown', '555-100-0005', 'michael.brown@example.com'),
('Sarah', 'Davis', '555-100-0006', 'sarah.davis@example.com'),
('Robert', 'Miller', '555-100-0007', 'robert.miller@example.com'),
('Jennifer', 'Wilson', '555-100-0008', 'jennifer.wilson@example.com'),
('William', 'Moore', '555-100-0009', 'william.moore@example.com'),
('Jessica', 'Taylor', '555-100-0010', 'jessica.taylor@example.com'),
('Daniel', 'Anderson', '555-100-0011', 'daniel.anderson@example.com'),
('Ashley', 'Thomas', '555-100-0012', 'ashley.thomas@example.com'),
('Christopher', 'Jackson', '555-100-0013', 'christopher.jackson@example.com'),
('Amanda', 'White', '555-100-0014', 'amanda.white@example.com'),
('Matthew', 'Harris', '555-100-0015', 'matthew.harris@example.com'),
('Stephanie', 'Martin', '555-100-0016', 'stephanie.martin@example.com'),
('Joshua', 'Thompson', '555-100-0017', 'joshua.thompson@example.com'),
('Nicole', 'Martinez', '555-100-0018', 'nicole.martinez@example.com'),
('Andrew', 'Robinson', '555-100-0019', 'andrew.robinson@example.com'),
('Melissa', 'Clark', '555-100-0020', 'melissa.clark@example.com'),
('Anthony', 'Rodriguez', '555-100-0021', 'anthony.rodriguez@example.com'),
('Elizabeth', 'Lewis', '555-100-0022', 'elizabeth.lewis@example.com'),
('Ryan', 'Lee', '555-100-0023', 'ryan.lee@example.com'),
('Heather', 'Walker', '555-100-0024', 'heather.walker@example.com'),
('Kevin', 'Hall', '555-100-0025', 'kevin.hall@example.com'),
('Megan', 'Allen', '555-100-0026', 'megan.allen@example.com'),
('Brian', 'Young', '555-100-0027', 'brian.young@example.com'),
('Laura', 'Hernandez', '555-100-0028', 'laura.hernandez@example.com'),
('Jason', 'King', '555-100-0029', 'jason.king@example.com'),
('Rachel', 'Wright', '555-100-0030', 'rachel.wright@example.com');