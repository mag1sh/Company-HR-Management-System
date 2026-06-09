SELECT TOP (1000) [Id]
      ,[Title]
      ,[Description]
      ,[BaseSalary]
  FROM [CompanyHRDb].[dbo].[Positions]


  SELECT * FROM Departments
  SELECT * FROM Employees
  SELECT * FROM Salaries
  SELECT * FROM Departments
  SELECT * FROM Leaves
  SELECT * FROM Positions
  SELECT * FROM SalaryHistories
  SELECT * FROM EmploymentHistories

  ALTER TABLE Positions
ADD DepartmentId INT NOT NULL DEFAULT 1

INSERT INTO Departments (Name, Description) VALUES
('Programmisti', 'programirane'),
('Chistachi', 'chistqt'),
('Nachalnici', 'upravlqvat'),
('Dizaineri', 'pravqt dizaini sigumo'),
('HR', 'upravlenie na choveshki resursi'),
('Marketing', 'marketing i reklama');

INSERT INTO Positions (Title, Description, BaseSalary, DepartmentId) VALUES
('Developer', 'developvat', 1200.00, 1),
('Glaven chistach', 'glavniq na chistachite', 400.00, 2),
('Glaven developer', 'po vishiq developer', 300.00, 1),
('Shef', 'nai ludiq', 12000.00, 3),
('Glaven dizainer', 'visshiq dizainer', 1430.00, 4),
('Pomoshtnik dizainer', 'pomoshtnik na glavniq', 900.00, 4),
('HR Manager', 'upravlява HR otdela', 2500.00, 5),
('HR Specialist', 'raboti s dokumenti', 1500.00, 5),
('Marketing Manager', 'upravlява marketing otdela', 2800.00, 6),
('Marketing Specialist', 'raboti s kampanii', 1600.00, 6),
('Junior Developer', 'nachinaeshtq razrabotchik', 900.00, 1),
('Senior Developer', 'opitten razrabotchik', 2000.00, 1);

INSERT INTO Employees (Name_FirstName, Name_LastName, Email_Value, Address_Country, Address_City, Address_PostalCode, Address_Street, Address_StreetNumber, HireDate, TerminationDate, Status, DepartmentId, PositionId) VALUES
('Svetoslav', 'Svetlios', 'svetlios@gmail.com',    'Bulgaria', 'Razgrad', '7280', 'Orel',        '23', '2023-01-04 18:11:54', NULL, 0, 2, 1),
('Magdalena', 'Hristova', 'magdalenah@gmail.com',  'Bulgaria', 'Razgrad', '7280', 'Drujba kozata','49', '2026-06-07 18:02:10', NULL, 0, 2, 4),
('Sisko',     'Sisko',    'siskosiskov@abv.bg',    'Bulgaria', 'Razgrad', '7280', 'Orel',        '23', '2026-06-07 18:03:21', NULL, 0, 4, 1),
('Tamer',     'Naimov',   'tamem@abv.bg',          'Bulgaria', 'Razgrad', '7280', 'Mortagonovo', '9',  '2026-06-08 20:12:10', NULL, 0, 3, 1),
('Ivan',      'Petrov',   'ivan.petrov@gmail.com', 'Bulgaria', 'Sofia',   '1000', 'Vitosha',     '5',  '2024-03-15 09:00:00', NULL, 0, 5, 7),
('Maria',     'Georgieva','maria.g@gmail.com',     'Bulgaria', 'Varna',   '9000', 'Primorska',   '12', '2023-06-01 09:00:00', NULL, 0, 5, 8),
('Georgi',    'Ivanov',   'georgi.i@abv.bg',       'Bulgaria', 'Plovdiv', '4000', 'Maritsa',     '3',  '2022-01-10 09:00:00', NULL, 0, 1, 11),
('Elena',     'Dimitrova','elena.d@gmail.com',     'Bulgaria', 'Razgrad', '7280', 'Svoboda',     '7',  '2021-05-20 09:00:00', NULL, 0, 1, 12),
('Petar',     'Stoyanov', 'petar.s@abv.bg',        'Bulgaria', 'Ruse',    '7000', 'Dunav',       '15', '2023-11-01 09:00:00', NULL, 0, 6, 9),
('Nikoleta',  'Vasileva', 'nikoleta.v@gmail.com',  'Bulgaria', 'Sofia',   '1000', 'Rakovski',    '22', '2020-08-15 09:00:00', NULL, 0, 6, 10),
('Dimitar',   'Kolev',    'dimitar.k@abv.bg',      'Bulgaria', 'Razgrad', '7280', 'Beli Lom',    '8',  '2024-01-05 09:00:00', NULL, 0, 2, 2),
('Silviq',    'Hristova', 'silviq.h@gmail.com',    'Bulgaria', 'Razgrad', '7280', 'Bulgaria',    '33', '2019-03-01 09:00:00', NULL, 0, 4, 5);

INSERT INTO Salaries (EmployeeId, Amount) VALUES
(1,  1200.00),
(2,  12000.00),
(3,  1200.00),
(4,  12000.00),
(5,  2500.00),
(6,  1500.00),
(7,  900.00),
(8,  2000.00),
(9,  2800.00),
(10, 1600.00),
(11, 400.00),
(12, 1430.00);

SELECT * FROM Leaves WHERE Status = 1
SELECT * FROM Employees