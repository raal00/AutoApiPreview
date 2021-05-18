CREATE TABLE [dbo].[Associate] (
	[Id] INT PRIMARY KEY IDENTITY(0, 1),
	[FirstName] NVARCHAR(50) NOT NULL,
	[MiddleName] NVARCHAR(50) NULL,
	[LastName] NVARCHAR(50) NULL
)
GO
CREATE TABLE [dbo].[AssociateLogin] (
	[Id] INT PRIMARY KEY IDENTITY(0, 1),
	[AssociateId] INT NOT NULL,
	[Login] NVARCHAR(100) NOT NULL,
	[PasswordHash] NVARCHAR(100) NOT NULL,
	FOREIGN KEY ([AssociateId]) REFERENCES [dbo].[Associate] ([Id])
)
GO
CREATE TABLE [dbo].[Role] (
	[Id] INT PRIMARY KEY IDENTITY(0, 1),
	[RoleDisplayName] NVARCHAR(40) NOT NULL,
	[RoleSystemName] NVARCHAR(40) NOT NULL
)
GO
INSERT INTO [dbo].[Role] ([RoleDisplayName], [RoleSystemName])
VALUES 
(N'Администратор', 'admin'),
(N'Пользователь', 'user')
GO
CREATE TABLE [dbo].[AssociateRole] (
	[Id] INT PRIMARY KEY IDENTITY(0, 1),
	[AssociateId] INT NOT NULL,
	[RoleId] INT NOT NULL,
	FOREIGN KEY ([AssociateId]) REFERENCES [dbo].[Associate] ([Id]),
	FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
)
GO
CREATE TABLE [dbo].[Car] (
	[Id] INT PRIMARY KEY IDENTITY(0, 1),
	[Code] NVARCHAR(50) NOT NULL,
	[Brand] NVARCHAR(50) NOT NULL,
	[Model] NVARCHAR(50) NOT NULL,
	[Color] NVARCHAR(30) NOT NULL,
	[Type] NVARCHAR(30) NOT NULL,
	[EngineVolume] INT NOT NULL,
	[CreateDate] DATE NOT NULL
)
GO
CREATE TABLE [dbo].[Order] (
	[Id] INT PRIMARY KEY IDENTITY(0, 1),
	[CarId] INT NOT NULL,
	[CarModel] NVARCHAR NULL,
	[AssociateId] INT NOT NULL,
	[SystemNumber] NVARCHAR(50) NOT NULL,
	[OrderDate] DATE NOT NULL,
	[IsCompleted] BIT NOT NULL,
	FOREIGN KEY ([AssociateId]) REFERENCES [dbo].[Associate] ([Id]),
	FOREIGN KEY ([CarId]) REFERENCES [dbo].[Car] ([Id])
)