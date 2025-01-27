USE [master]
GO
/****** Object:  Database [Enrollment]    Script Date: 24.01.2018 18:52:41 ******/
CREATE DATABASE [Enrollment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Enrollment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Enrollment.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Enrollment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Enrollment_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Enrollment] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Enrollment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Enrollment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Enrollment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Enrollment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Enrollment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Enrollment] SET ARITHABORT OFF 
GO
ALTER DATABASE [Enrollment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Enrollment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Enrollment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Enrollment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Enrollment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Enrollment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Enrollment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Enrollment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Enrollment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Enrollment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Enrollment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Enrollment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Enrollment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Enrollment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Enrollment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Enrollment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Enrollment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Enrollment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Enrollment] SET  MULTI_USER 
GO
ALTER DATABASE [Enrollment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Enrollment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Enrollment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Enrollment] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Enrollment]
GO
/****** Object:  Table [dbo].[EducationForms]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationForms](
	[EducationFormId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_EducationForms] PRIMARY KEY CLUSTERED 
(
	[EducationFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EducationPeriods]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationPeriods](
	[EducationPeriodId] [int] NOT NULL,
	[Period] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EducationPeriods] PRIMARY KEY CLUSTERED 
(
	[EducationPeriodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EducationPlaces]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationPlaces](
	[EducationPlaceId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EducationPlace] PRIMARY KEY CLUSTERED 
(
	[EducationPlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinancingTypes]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancingTypes](
	[FinancingTypeId] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FinancingTypes] PRIMARY KEY CLUSTERED 
(
	[FinancingTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NCSQSpecialities]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NCSQSpecialities](
	[NCSQSpecialityId] [int] NOT NULL,
	[Cipher] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Qualification] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_NCSQSpecialities] PRIMARY KEY CLUSTERED 
(
	[NCSQSpecialityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speciality](
	[SpecialityId] [int] NOT NULL,
	[EducationFormId] [int] NOT NULL,
	[EducationPeriodId] [int] NOT NULL,
	[EducationPlaceId] [int] NOT NULL,
	[FinancingTypeId] [int] NOT NULL,
	[NCSQSpecialityId] [int] NOT NULL,
	[SpecialityAvailableDateId] [int] NOT NULL,
	[SpecialityGroupId] [int] NOT NULL,
	[SpecialityPositionsNumberId] [int] NOT NULL,
	[SpecialityThresholdId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpecialityAvailableDates]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialityAvailableDates](
	[SpecialityAvailableDateId] [int] NOT NULL,
	[AvailableFrom] [date] NOT NULL,
	[AvailableUntil] [date] NOT NULL,
 CONSTRAINT [PK_SpecialityAvailableDates] PRIMARY KEY CLUSTERED 
(
	[SpecialityAvailableDateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpecialityGroups]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialityGroups](
	[SpecialityGroupId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SpecialityGroups] PRIMARY KEY CLUSTERED 
(
	[SpecialityGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpecialityPositionsNumbers]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialityPositionsNumbers](
	[SpecialityPositionsNumberId] [int] NOT NULL,
	[TotalNumber] [int] NOT NULL,
	[HonorGuardNumber] [int] NOT NULL,
	[OrphanNumber] [int] NOT NULL,
	[AdditionalNumber] [int] NOT NULL,
 CONSTRAINT [PK_SpecialityPositionsNumbers] PRIMARY KEY CLUSTERED 
(
	[SpecialityPositionsNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpecialityThresholds]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialityThresholds](
	[SpecialityThresholdId] [int] NOT NULL,
	[FirstSubjectId] [int] NOT NULL,
	[FirstMinScore] [int] NOT NULL,
	[FirstMinAdditionalScore] [int] NOT NULL,
	[SecondSubjectId] [int] NOT NULL,
	[SecondMinScore] [int] NOT NULL,
	[SecondMinAdditionalScore] [int] NOT NULL,
	[LanguageSubjectId] [int] NOT NULL,
	[LanguageMinScore] [int] NOT NULL,
	[LanguageMinAdditionalScore] [int] NOT NULL,
 CONSTRAINT [PK_SpecialityThreshols] PRIMARY KEY CLUSTERED 
(
	[SpecialityThresholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 24.01.2018 18:52:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[SubjectId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EducationForms] ([EducationFormId], [Name], [ParentId]) VALUES (1, N'Full-time', 0)
INSERT [dbo].[EducationForms] ([EducationFormId], [Name], [ParentId]) VALUES (2, N'Extramural', 0)
INSERT [dbo].[EducationForms] ([EducationFormId], [Name], [ParentId]) VALUES (3, N'Daytime', 1)
INSERT [dbo].[EducationForms] ([EducationFormId], [Name], [ParentId]) VALUES (4, N'Evening', 1)
INSERT [dbo].[EducationForms] ([EducationFormId], [Name], [ParentId]) VALUES (5, N'Remote', 2)
INSERT [dbo].[EducationForms] ([EducationFormId], [Name], [ParentId]) VALUES (6, N'Extramural', 2)
INSERT [dbo].[EducationPeriods] ([EducationPeriodId], [Period]) VALUES (1, N'Full (4-year)')
INSERT [dbo].[EducationPeriods] ([EducationPeriodId], [Period]) VALUES (2, N'Shortened (3-year)')
INSERT [dbo].[EducationPlaces] ([EducationPlaceId], [Name]) VALUES (1, N'BSUIR')
INSERT [dbo].[EducationPlaces] ([EducationPlaceId], [Name]) VALUES (2, N'BSU')
INSERT [dbo].[EducationPlaces] ([EducationPlaceId], [Name]) VALUES (3, N'BNTU')
INSERT [dbo].[EducationPlaces] ([EducationPlaceId], [Name]) VALUES (4, N'BSTU')
INSERT [dbo].[EducationPlaces] ([EducationPlaceId], [Name]) VALUES (5, N'BSPU')
INSERT [dbo].[FinancingTypes] ([FinancingTypeId], [Type]) VALUES (1, N'Budget')
INSERT [dbo].[FinancingTypes] ([FinancingTypeId], [Type]) VALUES (2, N'OwnFunds')
INSERT [dbo].[FinancingTypes] ([FinancingTypeId], [Type]) VALUES (3, N'75percSale')
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_EducationForms] FOREIGN KEY([EducationFormId])
REFERENCES [dbo].[EducationForms] ([EducationFormId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_EducationForms]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_EducationPeriods] FOREIGN KEY([EducationPeriodId])
REFERENCES [dbo].[EducationPeriods] ([EducationPeriodId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_EducationPeriods]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_EducationPlaces] FOREIGN KEY([EducationPlaceId])
REFERENCES [dbo].[EducationPlaces] ([EducationPlaceId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_EducationPlaces]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_FinancingTypes] FOREIGN KEY([FinancingTypeId])
REFERENCES [dbo].[FinancingTypes] ([FinancingTypeId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_FinancingTypes]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_NCSQSpecialities] FOREIGN KEY([NCSQSpecialityId])
REFERENCES [dbo].[NCSQSpecialities] ([NCSQSpecialityId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_NCSQSpecialities]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_SpecialityAvailableDates] FOREIGN KEY([SpecialityAvailableDateId])
REFERENCES [dbo].[SpecialityAvailableDates] ([SpecialityAvailableDateId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_SpecialityAvailableDates]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_SpecialityGroups] FOREIGN KEY([SpecialityGroupId])
REFERENCES [dbo].[SpecialityGroups] ([SpecialityGroupId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_SpecialityGroups]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_SpecialityPositionsNumbers] FOREIGN KEY([SpecialityPositionsNumberId])
REFERENCES [dbo].[SpecialityPositionsNumbers] ([SpecialityPositionsNumberId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_SpecialityPositionsNumbers]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_SpecialityThresholds] FOREIGN KEY([SpecialityThresholdId])
REFERENCES [dbo].[SpecialityThresholds] ([SpecialityThresholdId])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_SpecialityThresholds]
GO
ALTER TABLE [dbo].[SpecialityThresholds]  WITH CHECK ADD  CONSTRAINT [FK_SpecialityThresholds_Subjects] FOREIGN KEY([LanguageSubjectId])
REFERENCES [dbo].[Subjects] ([SubjectId])
GO
ALTER TABLE [dbo].[SpecialityThresholds] CHECK CONSTRAINT [FK_SpecialityThresholds_Subjects]
GO
ALTER TABLE [dbo].[SpecialityThresholds]  WITH CHECK ADD  CONSTRAINT [FK_SpecialityThresholds_Subjects1] FOREIGN KEY([FirstSubjectId])
REFERENCES [dbo].[Subjects] ([SubjectId])
GO
ALTER TABLE [dbo].[SpecialityThresholds] CHECK CONSTRAINT [FK_SpecialityThresholds_Subjects1]
GO
ALTER TABLE [dbo].[SpecialityThresholds]  WITH CHECK ADD  CONSTRAINT [FK_SpecialityThresholds_Subjects2] FOREIGN KEY([SecondSubjectId])
REFERENCES [dbo].[Subjects] ([SubjectId])
GO
ALTER TABLE [dbo].[SpecialityThresholds] CHECK CONSTRAINT [FK_SpecialityThresholds_Subjects2]
GO
USE [master]
GO
ALTER DATABASE [Enrollment] SET  READ_WRITE 
GO
