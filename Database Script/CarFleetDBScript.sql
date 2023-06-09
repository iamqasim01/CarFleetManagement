USE [master]
GO
/****** Object:  Database [CarFleetManagement]    Script Date: 4/27/2023 12:54:54 PM ******/
CREATE DATABASE [CarFleetManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarFleetManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLEXPRESS\MSSQL\DATA\CarFleetManagement.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CarFleetManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLEXPRESS\MSSQL\DATA\CarFleetManagement_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CarFleetManagement] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarFleetManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarFleetManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarFleetManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarFleetManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarFleetManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarFleetManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarFleetManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarFleetManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarFleetManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarFleetManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarFleetManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarFleetManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarFleetManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarFleetManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarFleetManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarFleetManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarFleetManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarFleetManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarFleetManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarFleetManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarFleetManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarFleetManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarFleetManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarFleetManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CarFleetManagement] SET  MULTI_USER 
GO
ALTER DATABASE [CarFleetManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarFleetManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarFleetManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarFleetManagement] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CarFleetManagement] SET DELAYED_DURABILITY = DISABLED 
GO
USE [CarFleetManagement]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 4/27/2023 12:54:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[VehicleID] [bigint] IDENTITY(1,1) NOT NULL,
	[RegistrationNo] [nvarchar](15) NULL,
	[Brand] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vehicle_TechnicalTest]    Script Date: 4/27/2023 12:54:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle_TechnicalTest](
	[TechnicalTestID] [bigint] IDENTITY(1,1) NOT NULL,
	[VehicleID] [bigint] NULL,
	[InspectionDate] [date] NULL,
	[NextInspectionDate] [date] NULL,
	[IsOperational] [bit] NULL,
 CONSTRAINT [PK_Vehicle_Technical_Tests] PRIMARY KEY CLUSTERED 
(
	[TechnicalTestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Vehicle] ON 

INSERT [dbo].[Vehicle] ([VehicleID], [RegistrationNo], [Brand], [Model], [Photo]) VALUES (1, N'SG50963', N'Maserati', N'Ghibli SQ4', N'https://i.pinimg.com/564x/4c/5b/d3/4c5bd362cf2d3aa935ba5971d1fe3a8b.jpg')
INSERT [dbo].[Vehicle] ([VehicleID], [RegistrationNo], [Brand], [Model], [Photo]) VALUES (2, N'TR789143', N'Ford', N'Shelby GT500', N'https://static1.hotcarsimages.com/wordpress/wp-content/uploads/2023/03/2024-ford-shelby-mustang-gt500-render-front.jpg?q=50&fit=contain&w=1140&h=&dpr=1.5')
INSERT [dbo].[Vehicle] ([VehicleID], [RegistrationNo], [Brand], [Model], [Photo]) VALUES (3, N'KR89R12Y', N'Porshe', N'Porshe 911 GT3', N'https://images.cdn.circlesix.co/image/1/1000/0/uploads/media/2020-11/25/cbf678a9ba240d5b/be8i8272.jpg')
SET IDENTITY_INSERT [dbo].[Vehicle] OFF
SET IDENTITY_INSERT [dbo].[Vehicle_TechnicalTest] ON 

INSERT [dbo].[Vehicle_TechnicalTest] ([TechnicalTestID], [VehicleID], [InspectionDate], [NextInspectionDate], [IsOperational]) VALUES (1, 1, CAST(N'2023-04-20' AS Date), CAST(N'2024-04-20' AS Date), 1)
INSERT [dbo].[Vehicle_TechnicalTest] ([TechnicalTestID], [VehicleID], [InspectionDate], [NextInspectionDate], [IsOperational]) VALUES (2, 2, CAST(N'2023-04-28' AS Date), CAST(N'2024-04-28' AS Date), 1)
INSERT [dbo].[Vehicle_TechnicalTest] ([TechnicalTestID], [VehicleID], [InspectionDate], [NextInspectionDate], [IsOperational]) VALUES (3, 3, CAST(N'2023-04-30' AS Date), CAST(N'2024-04-30' AS Date), 1)
INSERT [dbo].[Vehicle_TechnicalTest] ([TechnicalTestID], [VehicleID], [InspectionDate], [NextInspectionDate], [IsOperational]) VALUES (4, 1, CAST(N'2023-05-30' AS Date), CAST(N'2023-04-27' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Vehicle_TechnicalTest] OFF
ALTER TABLE [dbo].[Vehicle_TechnicalTest]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_TechnicalTest_Vehicle] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicle] ([VehicleID])
GO
ALTER TABLE [dbo].[Vehicle_TechnicalTest] CHECK CONSTRAINT [FK_Vehicle_TechnicalTest_Vehicle]
GO
USE [master]
GO
ALTER DATABASE [CarFleetManagement] SET  READ_WRITE 
GO
