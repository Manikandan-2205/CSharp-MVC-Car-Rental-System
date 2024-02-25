--drop table customer;
--drop table carreg;
--drop table Rentail;

CREATE TABLE [dbo].[Customer] (
	[Id]       INT          IdENTITY (1, 1) NOT NULL,
	[CustName] VARCHAR (50) NULL,
	[Address]  VARCHAR (50) NULL,
	[Mobile]   VARCHAR (50) NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Carreg] (
	[Id]        INT          IdENTITY (1, 1) NOT NULL,
	[CarNo]     VARCHAR (50) NULL,
	[Make]      VARCHAR (50) NULL,
	[Model]     VARCHAR (50) NULL,
	[Available] VARCHAR (50) NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);


--  drop table Rental;

CREATE TABLE [dbo].[Rental] (
	[Id]        INT          IDENTITY (1, 1) NOT NULL,
	[CarId]     VARCHAR (50) NULL,
	[CustId]    INT          NULL,
	[Fee]       INT          NULL,
	[StartDate] DATETIME         NULL,
	[EndDate]   DATETIME         NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);

--  drop table ReturnCar;

CREATE TABLE [dbo].[ReturnCar] (
	[Id]        INT          IDENTITY (1, 1) NOT NULL,
	[CarNo]     VARCHAR (50) NULL,
	[ReturnDate] DATETIME         NULL,
	[Elsp]       INT          NULL,
	[Fine]       INT          NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);


INSERT INTO [dbo].[Customer] ([CustName], [Address], [Mobile])
VALUES 
	('Krish Kumar', '123 Main Street, Erode, TN', '9876543210'),
	('Sandhya Raj', '456 Oak Avenue, Coimbatore, TN', '8765432109'),
	('Ragul Somu', '789 Pine Lane, Salem, TN', '7654321098'),
	('Kavitha Mohan', '101 Maple Road, Tirupur, TN', '6543210987'),
	('Nithya Subramaniam', '202 Cedar Street, Namakkal, TN', '5432109876'),
	('Aryana Sekar', '303 Elm Court, Karur, TN', '4321098765'),
	('Pavithra Kumar', '404 Birch Avenue, Dindigul, TN', '3210987654'),
	('Vignesh Balaji', '505 Walnut Lane, Madurai, TN', '2109876543'),
	('Shanthi Mohan', '606 Pine Road, Trichy, TN', '1098765432'),
	('Karthik Raja', '707 Oak Street, Chennai, TN', '9876543210');


-- Insert data into Carreg table
INSERT INTO [dbo].[Carreg] ([CarNo], [Make], [Model], [Available])
VALUES 
	('TN123', 'Toyota', 'Corolla', 'Yes'),
	('TN456', 'Honda', 'Civic', 'Yes'),
	('TN789', 'Ford', 'Focus', 'Yes'),
	('TN101', 'Chevrolet', 'Malibu', 'Yes'),
	('TN202', 'Nissan', 'Altima', 'Yes'),
	('TN303', 'Hyundai', 'Elantra', 'Yes'),
	('TN404', 'Kia', 'Optima', 'Yes'),
	('TN505', 'Mazda', 'Mazda3', 'Yes'),
	('TN606', 'Volkswagen', 'Jetta', 'Yes'),
	('TN707', 'Subaru', 'Impreza', 'Yes');

-- Insert data into Rental table
INSERT INTO [dbo].[Rental] ([CarId], [CustId], [Fee], [StartDate], [EndDate])
VALUES 
	('TN123', 1, 500, '2024-02-01', '2024-02-10'),
	('TN456', 2, 600, '2024-02-05', '2024-02-15'),
	('TN789', 3, 550, '2024-02-10', '2024-02-20'),
	('TN101', 4, 650, '2024-02-15', '2024-02-25'),
	('TN202', 5, 700, '2024-02-20', '2024-02-28'),
	('TN303', 6, 450, '2024-02-25', '2024-03-05'),
	('TN404', 7, 500, '2024-03-01', '2024-03-10'),
	('TN505', 8, 600, '2024-03-05', '2024-03-15'),
	('TN606', 9, 550, '2024-03-10', '2024-03-20'),
	('TN707', 10, 650, '2024-03-15', '2024-03-25');

-- Insert data into ReturnCar table
INSERT INTO [dbo].[ReturnCar] ([CarNo], [ReturnDate], [Elsp], [Fine])
VALUES 
	('TN123', '2024-02-10', 5, 50),
	('TN456', '2024-02-15', 7, 70),
	('TN789', '2024-02-20', 6, 60),
	('TN101', '2024-02-25', 8, 80),
	('TN202', '2024-02-28', 5, 50),
	('TN303', '2024-03-05', 7, 70),
	('TN404', '2024-03-10', 6, 60),
	('TN505', '2024-03-15', 8, 80),
	('TN606', '2024-03-20', 7, 70),
	('TN707', '2024-03-25', 9, 90);

	TRUNCATE TABLE Rental;
	TRUNCATE TABLE ReturnCar;

	select * from rental;

	select * from ReturnCar;


	UPDATE Carreg
	SET [Available] = 'Yes'
	WHERE [Available] = 'No';

	--drop table RoleDetails;

	CREATE TABLE [dbo].[RoleDetails] (
	[RoleId]  INT           NOT NULL,
	[RoleName] NVARCHAR (50) NOT NULL,
	PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

-- drop table UserDetails;

CREATE TABLE [dbo].[UserDetails] (
	[Id]              INT      IDENTITY (1, 1)      NOT NULL,
	[Email]           NVARCHAR (50) NULL,
	[PhoneNo]         NVARCHAR (10) NULL,
	[Username]        NVARCHAR (50) NULL,
	[Password]        NVARCHAR (10) NOT NULL,
	[ConfirmPassword] NVARCHAR (10) NOT NULL,
	[RoleId]          INT           NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserDetails_RoleDetails] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[RoleDetails] ([RoleId])
);

-- Insert records into RoleDetails table
INSERT INTO [dbo].[RoleDetails] ([RoleId], [RoleName]) VALUES (1, 'Admin');
INSERT INTO [dbo].[RoleDetails] ([RoleId], [RoleName]) VALUES (2, 'User');

-- Insert multiple records into UserDetails table with valid RoleId
-- Insert records into UserDetails table without specifying Id (assuming Id is an identity column)
INSERT INTO [dbo].[UserDetails] ([Email], [PhoneNo], [Username], [Password], [ConfirmPassword], [RoleId]) 
VALUES 
('Manikandanpalanisamy123@gmail.com', '9360982027', 'Manikandan', '123', '123', 1),
('Manikandanpalanisamy123@gmail.com', '9360982027', 'Mani', '123', '123', 2);

select * from RoleDetails;
select * from UserDetails;
select * from Rental;
select * from Rental;

truncate table Rental;
truncate table ReturnCar;

update Carreg
set Available = 'Yes'
where Available = 'No';

update userDetails 
set Password = '12345678'
where password = '123';