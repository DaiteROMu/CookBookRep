USE [master]
GO
/****** Object:  Database [CookBookDB]    Script Date: 14.06.2018 16:59:57 ******/
CREATE DATABASE [CookBookDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CookBookDB', FILENAME = N'D:\CookBookDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CookBookDB_log', FILENAME = N'D:\CookBookDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CookBookDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CookBookDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CookBookDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CookBookDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CookBookDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CookBookDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CookBookDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CookBookDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CookBookDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CookBookDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CookBookDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CookBookDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CookBookDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CookBookDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CookBookDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CookBookDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CookBookDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CookBookDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CookBookDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CookBookDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CookBookDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CookBookDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CookBookDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CookBookDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CookBookDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CookBookDB] SET  MULTI_USER 
GO
ALTER DATABASE [CookBookDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CookBookDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CookBookDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CookBookDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CookBookDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CookBookDB] SET QUERY_STORE = OFF
GO
USE [CookBookDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CookBookDB]
GO
/****** Object:  User [CookBookAdm]    Script Date: 14.06.2018 16:59:57 ******/
CREATE USER [CookBookAdm] FOR LOGIN [CookBookAdm] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [CookBookAdm]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingridient](
	[IngridientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IngridientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeDetails]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeDetails](
	[RecipeId] [int] NOT NULL,
	[CookingTime] [nvarchar](50) NULL,
	[CookingTemperature] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[Sequencing] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeIngridient](
	[RecipeId] [int] NOT NULL,
	[IngridientId] [int] NOT NULL,
	[Weight] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC,
	[IngridientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserUserRole]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserUserRole](
	[UserId] [int] NOT NULL,
	[UserRoleId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecipeDetails]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecipeIngridient]  WITH CHECK ADD FOREIGN KEY([IngridientId])
REFERENCES [dbo].[Ingridient] ([IngridientId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecipeIngridient]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserUserRole]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserUserRole]  WITH CHECK ADD FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([UserRoleId])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteCategory]
@CategoryId int
AS
DELETE FROM Category
WHERE [Category].CategoryId=@CategoryId
GO
/****** Object:  StoredProcedure [dbo].[DeleteIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteIngridient]
@IngridientId int
AS
DELETE FROM Ingridient
WHERE [Ingridient].IngridientId=@IngridientId
GO
/****** Object:  StoredProcedure [dbo].[DeleteRecipe]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteRecipe]
@RecipeId int
AS
DELETE FROM Recipe
WHERE [Recipe].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[DeleteRecipeIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteRecipeIngridient]
@RecipeId int,
@IngridientId int
AS
DELETE FROM RecipeIngridient
WHERE [RecipeIngridient].RecipeId=@RecipeId AND [RecipeIngridient].IngridientId=@IngridientId
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteUser]
@UserId int
AS
DELETE FROM Users
WHERE [Users].UserId=@UserId
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserUserRole]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteUserUserRole]
@UserId int,
@UserRoleId int
AS
DELETE FROM UserUserRole
WHERE [UserUserRole].UserId=@UserId and [UserUserRole].UserRoleId=@UserRoleId
GO
/****** Object:  StoredProcedure [dbo].[GetCategories]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCategories]
AS
SELECT * FROM Category
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryById]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCategoryById]
@CategoryId int
AS
SELECT * FROM Category
WHERE [Category].CategoryId=@CategoryId
GO
/****** Object:  StoredProcedure [dbo].[GetChildCategoriesById]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetChildCategoriesById]
@CategoryId int
AS
SELECT * FROM Category
WHERE [Category].ParentId=@CategoryId
GO
/****** Object:  StoredProcedure [dbo].[GetIngridientById]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetIngridientById]
@IngridientId int
AS
SELECT * FROM Ingridient
WHERE [Ingridient].IngridientId=@IngridientId
GO
/****** Object:  StoredProcedure [dbo].[GetIngridients]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetIngridients]
AS
SELECT * FROM Ingridient
GO
/****** Object:  StoredProcedure [dbo].[GetLastInsertedRecipeId]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetLastInsertedRecipeId]
AS
SELECT ident_current('Recipe')
GO
/****** Object:  StoredProcedure [dbo].[GetLastInsertedUserId]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetLastInsertedUserId]
AS
SELECT ident_current('Users')
GO
/****** Object:  StoredProcedure [dbo].[GetRecipeById]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipeById]
@RecipeId int
AS
SELECT * FROM Recipe
WHERE [Recipe].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipeDetailsById]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipeDetailsById]
@RecipeId int
AS
SELECT * FROM RecipeDetails
WHERE [RecipeDetails].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipeIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipeIngridient]
@RecipeId int,
@IngridientId int
AS
SELECT [RecipeIngridient].RecipeId, [RecipeIngridient].IngridientId, [Ingridient].Name, [RecipeIngridient].Weight FROM RecipeIngridient inner join Ingridient on [RecipeIngridient].IngridientId=[Ingridient].IngridientId
WHERE [RecipeIngridient].RecipeId=@RecipeId AND [RecipeIngridient].IngridientId=@IngridientId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipeIngridientsByRecipeId]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipeIngridientsByRecipeId]
@RecipeId int
AS
SELECT [RecipeIngridient].RecipeId, [RecipeIngridient].IngridientId, [Ingridient].Name, [RecipeIngridient].Weight FROM RecipeIngridient inner join Ingridient on [RecipeIngridient].IngridientId=[Ingridient].IngridientId
WHERE [RecipeIngridient].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipes]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipes]
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipesByCategoryId]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipesByCategoryId]
@CategoryId int
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId
WHERE [Recipe].CategoryId=@CategoryId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipesByCategoryName]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipesByCategoryName]
@categoryName nvarchar(50)
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId
WHERE CHARINDEX(@categoryName, [Category].Name)>0
GO
/****** Object:  StoredProcedure [dbo].[GetRecipesByIngridientName]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipesByIngridientName]
@ingridientName nvarchar(50)
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM (((Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId) inner join RecipeIngridient on [Recipe].RecipeId=[RecipeIngridient].RecipeId) inner join Ingridient on [RecipeIngridient].IngridientId=[Ingridient].IngridientId)
WHERE CHARINDEX(@ingridientName, [Ingridient].Name)>0
GO
/****** Object:  StoredProcedure [dbo].[GetRecipesByRecipeName]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipesByRecipeName]
@recipeName nvarchar(50)
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId
WHERE CHARINDEX(@recipeName, [Recipe].Name)>0
GO
/****** Object:  StoredProcedure [dbo].[GetTopCategories]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTopCategories]
AS
SELECT * FROM Category
WHERE [Category].ParentId IS NULL
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserById]
@UserId int
AS
SELECT * FROM Users
WHERE [Users].UserId=@UserId
GO
/****** Object:  StoredProcedure [dbo].[GetUserByLogin]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserByLogin]
@Login nvarchar(50)
AS
SELECT * FROM Users
WHERE [Users].Login=@Login
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoles]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserRoles]
AS
SELECT * FROM UserRole
GO
/****** Object:  StoredProcedure [dbo].[GetUserRolesByUserId]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserRolesByUserId]
@UserId int
AS
SELECT [UserUserRole].UserRoleId, [UserRole].Name FROM UserUserRole inner join UserRole on [UserUserRole].UserRoleId=[UserRole].UserRoleId
WHERE [UserUserRole].UserId=@UserId
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUsers]
AS
SELECT [Users].UserId, [Users].Login, [Users].Password, [Users].Email FROM Users
GO
/****** Object:  StoredProcedure [dbo].[InsertCategory]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertCategory]
@Name nvarchar(50),
@ParentId int
AS
INSERT INTO Category(Name, ParentId)
VALUES (@Name, @ParentId)
GO
/****** Object:  StoredProcedure [dbo].[InsertIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertIngridient]
@Name nvarchar(50)
AS
INSERT INTO Ingridient(Name)
VALUES (@Name)
GO
/****** Object:  StoredProcedure [dbo].[InsertRecipe]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertRecipe]
@Name nvarchar(100),
@ImageUrl nvarchar(max),
@CategoryId int
AS
INSERT INTO Recipe(Name,ImageUrl,CategoryId)
VALUES (@Name,@ImageUrl,@CategoryId)
GO
/****** Object:  StoredProcedure [dbo].[InsertRecipeDetails]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertRecipeDetails]
@CookingTime nvarchar(50),
@CookingTemperature nvarchar(50),
@Description nvarchar(200),
@Sequencing nvarchar(max)
AS
INSERT INTO RecipeDetails(RecipeId,CookingTime, CookingTemperature, Description, Sequencing)
VALUES ((SELECT ident_current('Recipe')),@CookingTime,@CookingTemperature,@Description,@Sequencing)
GO
/****** Object:  StoredProcedure [dbo].[InsertRecipeIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertRecipeIngridient]
@RecipeId int,
@IngridientId int,
@Weight nvarchar(100)
AS
INSERT INTO RecipeIngridient(RecipeId,IngridientId,Weight)
VALUES (@RecipeId,@IngridientId,@Weight)
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertUser]
@Login nvarchar(50),
@Password nvarchar(20),
@Email nvarchar(100)
AS
INSERT INTO Users(Login, Password, Email)
VALUES (@Login, @Password, @Email)
GO
/****** Object:  StoredProcedure [dbo].[InsertUserUserRole]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertUserUserRole]
@UserId int,
@UserRoleId int
AS
INSERT INTO UserUserRole(UserId, UserRoleId)
VALUES (@UserId, @UserRoleId)
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateCategory]
@CategoryId int,
@Name nvarchar(50),
@ParentId int
AS
UPDATE Category
SET Name=@Name, ParentId=@ParentId
WHERE [Category].CategoryId=@CategoryId
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategoryParentId]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateCategoryParentId]
@CategoryId int,
@ParentId int
AS
UPDATE Category
SET ParentId=@ParentId
WHERE [Category].CategoryId=@CategoryId
GO
/****** Object:  StoredProcedure [dbo].[UpdateIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateIngridient]
@IngridientId int,
@Name nvarchar(50)
AS
UPDATE Ingridient
SET Name=@Name
WHERE [Ingridient].IngridientId=@IngridientId
GO
/****** Object:  StoredProcedure [dbo].[UpdateRecipe]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateRecipe]
@RecipeId int,
@Name nvarchar(100),
@ImageUrl nvarchar(max),
@CategoryId int
AS
UPDATE Recipe
SET Name=@Name, ImageUrl=@ImageUrl, CategoryId=@CategoryId
WHERE [Recipe].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[UpdateRecipeDetails]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateRecipeDetails]
@RecipeId int,
@CookingTime nvarchar(50),
@CookingTemperature nvarchar(50),
@Description nvarchar(200),
@Sequencing nvarchar(max)
AS
UPDATE RecipeDetails
SET CookingTime=@CookingTime, CookingTemperature=@CookingTemperature, Description=@Description, Sequencing=@Sequencing
WHERE [RecipeDetails].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[UpdateRecipeDetailsSequencing]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateRecipeDetailsSequencing]
@RecipeId int,
@Sequencing nvarchar(max)
AS
UPDATE RecipeDetails
SET Sequencing=@Sequencing
WHERE [RecipeDetails].RecipeId=@RecipeId
GO
/****** Object:  StoredProcedure [dbo].[UpdateRecipeIngridient]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateRecipeIngridient]
@RecipeId int,
@IngridientId int,
@Weight nvarchar(100)
AS
UPDATE RecipeIngridient
SET Weight=@Weight
WHERE [RecipeIngridient].RecipeId=@RecipeId AND [RecipeIngridient].IngridientId=@IngridientId
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 14.06.2018 16:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateUser]
@UserId int,
@Login nvarchar(50),
@Password nvarchar(20),
@Email nvarchar(100)
AS
UPDATE Users
SET Login=@Login, Password=@Password, Email=@Email
WHERE [Users].UserId=@UserId
GO
USE [master]
GO
ALTER DATABASE [CookBookDB] SET  READ_WRITE 
GO
