USE [master]
GO
/****** Object:  Database [Exam]    Script Date: 12/12/2015 13:25:33 ******/
CREATE DATABASE [Exam] ON  PRIMARY 
( NAME = N'Exam', FILENAME = N'E:\workspace\Database\MSSQL10_50.MYSQLSERVER\MSSQL\DATA\Exam.mdf' , SIZE = 13312KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Exam_log', FILENAME = N'E:\workspace\Database\MSSQL10_50.MYSQLSERVER\MSSQL\DATA\Exam_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Exam] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Exam].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Exam] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Exam] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Exam] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Exam] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Exam] SET ARITHABORT OFF
GO
ALTER DATABASE [Exam] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Exam] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Exam] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Exam] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Exam] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Exam] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Exam] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Exam] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Exam] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Exam] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Exam] SET  DISABLE_BROKER
GO
ALTER DATABASE [Exam] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Exam] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Exam] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Exam] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Exam] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Exam] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Exam] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Exam] SET  READ_WRITE
GO
ALTER DATABASE [Exam] SET RECOVERY FULL
GO
ALTER DATABASE [Exam] SET  MULTI_USER
GO
ALTER DATABASE [Exam] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Exam] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'Exam', N'ON'
GO
USE [Exam]
GO
/****** Object:  Table [dbo].[TestPaper]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestPaper](
	[ID] [bigint] NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Info] [nvarchar](max) NOT NULL,
	[SingleChoiceQuantity] [int] NOT NULL,
	[SingleChoiceGrade] [int] NOT NULL,
	[MultipleChoiceQuantity] [int] NOT NULL,
	[MultipleChoiceGrade] [int] NOT NULL,
	[CompletionQuantity] [int] NOT NULL,
	[CompletionGrade] [int] NOT NULL,
	[ShortAnswerQuantity] [int] NOT NULL,
	[ShortAnswerGrade] [int] NOT NULL,
	[DiscussionQuantity] [int] NOT NULL,
	[IsReady] [bit] NOT NULL,
	[DiscussionGrade] [int] NOT NULL,
	[ExamType] [nvarchar](50) NULL,
	[SubjectID] [int] NOT NULL,
	[ExamTime] [datetime] NOT NULL,
	[TimeDuration] [int] NULL,
	[Active] [int] NOT NULL,
	[Audit] [int] NOT NULL,
	[ModificationTeacher] [nvarchar](100) NOT NULL,
	[ModificationTeacherID] [nvarchar](128) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TestSubjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试持续时间（单位：分钟)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestPaper', @level2type=N'COLUMN',@level2name=N'TimeDuration'
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Paper]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paper](
	[PaperID] [bigint] NOT NULL,
	[SubjectID] [int] NOT NULL,
	[SingleChoiceQuantity] [int] NULL,
	[MultipleChoiceQuantity] [int] NULL,
	[CompletionQuantity] [int] NULL,
	[ShortAnswerQuantity] [int] NULL,
	[DiscussionQuantity] [int] NULL,
	[SingleChoiceScore] [int] NULL,
	[MultipleChoiceScore] [int] NULL,
	[CompletionScore] [int] NULL,
	[ShortAnswer] [int] NULL,
	[DiscussionScore] [int] NULL,
	[Active] [int] NOT NULL,
	[Audit] [int] NOT NULL,
	[ModificationTeacher] [nvarchar](50) NULL,
	[DateAdded] [datetime] NULL,
	[ModificationDate] [nchar](10) NULL,
 CONSTRAINT [PK_Paper] PRIMARY KEY CLUSTERED 
(
	[PaperID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[MajorID] [int] NOT NULL,
	[MajorName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED 
(
	[MajorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grade](
	[GradeID] [int] NOT NULL,
	[GradeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED 
(
	[GradeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher_Subject]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher_Subject](
	[TeacherID] [nvarchar](256) NOT NULL,
	[SubjectID] [int] NOT NULL,
 CONSTRAINT [PK_Teacher_Subject] PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC,
	[SubjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject_Essay]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject_Essay](
	[SubjectID] [int] NOT NULL,
	[QuestionID] [bigint] NOT NULL,
 CONSTRAINT [PK_Subject_ShortAnswer] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject_Choice]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject_Choice](
	[SubjectID] [int] NOT NULL,
	[QuestionID] [bigint] NOT NULL,
 CONSTRAINT [PK_Subject_MultipleChoice] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectCode] [nvarchar](50) NULL,
	[SubjectName] [nvarchar](100) NOT NULL,
	[Term] [nvarchar](50) NULL,
	[Grade] [nvarchar](50) NULL,
	[Version] [nvarchar](50) NULL,
	[ParentId] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[PreSubject] [nvarchar](50) NULL,
	[Book] [nvarchar](50) NULL,
	[ExamType] [int] NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属的大科目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Subject', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修该课程之前必修的课程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Subject', @level2type=N'COLUMN',@level2name=N'PreSubject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联教材' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Subject', @level2type=N'COLUMN',@level2name=N'Book'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考试类型：实践考核、笔试、机试' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Subject', @level2type=N'COLUMN',@level2name=N'ExamType'
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceID] [bigint] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](500) NULL,
	[Location] [nvarchar](500) NULL,
	[Remark] [nvarchar](1000) NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionEssay ]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionEssay ](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](1000) NOT NULL,
	[Answer] [nvarchar](1000) NOT NULL,
	[QuestionType] [nvarchar](50) NULL,
	[Info] [nvarchar](500) NULL,
	[ModificationTeacher] [nchar](100) NULL,
	[ModificationDate] [datetime] NULL,
	[DateAdded] [date] NOT NULL,
	[Difficulty] [smallint] NOT NULL,
	[Frequency] [smallint] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Assessment] [nvarchar](1000) NULL,
	[Audit] [int] NOT NULL,
	[ModificationTeacherID] [nvarchar](128) NOT NULL,
	[IsImport] [bit] NOT NULL,
	[Auditor] [nvarchar](100) NULL,
	[AuditorID] [nvarchar](128) NULL,
 CONSTRAINT [PK_QuestionShortAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionCollection]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionCollection](
	[UserID] [nvarchar](128) NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[Remark] [nvarchar](1000) NULL,
 CONSTRAINT [PK_QuestionCollection] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionChoice]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionChoice](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](1000) NOT NULL,
	[OptionA] [nvarchar](500) NOT NULL,
	[OptionB] [nvarchar](500) NOT NULL,
	[OptionC] [nvarchar](500) NULL,
	[OptionD] [nvarchar](500) NULL,
	[OptionE] [nvarchar](500) NULL,
	[OptionF] [nvarchar](500) NULL,
	[IsMultiple] [bit] NOT NULL,
	[AisTrue] [bit] NOT NULL,
	[BisTrue] [bit] NOT NULL,
	[CisTrue] [bit] NOT NULL,
	[DisTrue] [bit] NOT NULL,
	[EisTrue] [bit] NOT NULL,
	[FisTrue] [bit] NOT NULL,
	[ModificationTeacher] [nchar](100) NULL,
	[ModificationDate] [datetime] NULL,
	[DateAdded] [date] NOT NULL,
	[Difficulty] [smallint] NOT NULL,
	[Frequency] [smallint] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Assessment] [nchar](1000) NULL,
	[Info] [nvarchar](500) NULL,
	[Audit] [int] NOT NULL,
	[ModificationTeacherID] [nvarchar](128) NOT NULL,
	[IsImport] [bit] NOT NULL,
	[Auditor] [nvarchar](100) NULL,
	[AuditorID] [nvarchar](128) NULL,
 CONSTRAINT [PK_QuestionMultipleChoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nchar](10) NOT NULL,
	[Remark] [nvarchar](100) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaperCollection]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaperCollection](
	[UserID] [nvarchar](128) NOT NULL,
	[PaperID] [bigint] NOT NULL,
	[Remark] [nvarchar](1000) NULL,
 CONSTRAINT [PK_PaperCollection] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[PaperID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [nvarchar](256) NOT NULL,
	[NickName] [nvarchar](50) NULL,
	[RealName] [nvarchar](250) NULL,
	[Address] [nvarchar](250) NULL,
	[School] [nvarchar](250) NULL,
	[Birthday] [datetime] NULL,
	[Company] [nvarchar](250) NULL,
	[Photo] [nvarchar](1000) NULL,
	[TelPhone] [nvarchar](50) NULL,
	[QQ] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Subject]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Subject](
	[UserID] [nvarchar](128) NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_User_Subject] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SubjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'我的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Subject', @level2type=N'COLUMN',@level2name=N'UserID'
GO
/****** Object:  Table [dbo].[TestAnswerList]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAnswerList](
	[AnserListID] [bigint] NOT NULL,
	[PaperID] [bigint] NOT NULL,
	[StudentID] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_TestAnswerList] PRIMARY KEY CLUSTERED 
(
	[AnserListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paper_Essay]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paper_Essay](
	[PaperID] [bigint] NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[DefaultScore] [float] NULL,
	[BigQuestionNumber] [int] NULL,
	[SmallQuestionNumber] [int] NULL,
 CONSTRAINT [PK_Paper_Essay] PRIMARY KEY CLUSTERED 
(
	[PaperID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paper_Choice]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paper_Choice](
	[PaperID] [bigint] NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[DefaultScore] [float] NOT NULL,
	[BigQuestionNumber] [int] NULL,
	[SmallQuestionNumber] [int] NULL,
 CONSTRAINT [PK_Paper_Question] PRIMARY KEY CLUSTERED 
(
	[PaperID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major_Subject]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major_Subject](
	[MajorID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Socre] [int] NULL,
 CONSTRAINT [PK_Major_Subject] PRIMARY KEY CLUSTERED 
(
	[MajorID] ASC,
	[SubjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestAnswerSubItem]    Script Date: 12/12/2015 13:25:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAnswerSubItem](
	[AnswerListID] [bigint] NOT NULL,
	[BigQuestionNumber] [int] NOT NULL,
	[SmallQuestionNumber] [int] NOT NULL,
	[Answer] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnswerSubItem] PRIMARY KEY CLUSTERED 
(
	[AnswerListID] ASC,
	[BigQuestionNumber] ASC,
	[SmallQuestionNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_TestPaper_ExamType]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_ExamType]  DEFAULT (N'练习题') FOR [ExamType]
GO
/****** Object:  Default [DF_TestPaper_SubjectID]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_SubjectID]  DEFAULT ((-1)) FOR [SubjectID]
GO
/****** Object:  Default [DF_TestPaper_TimeDuration]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_TimeDuration]  DEFAULT ((-1)) FOR [TimeDuration]
GO
/****** Object:  Default [DF_TestPaper_Active]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_Active]  DEFAULT ((1)) FOR [Active]
GO
/****** Object:  Default [DF_TestPaper_Audit]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_Audit]  DEFAULT ((0)) FOR [Audit]
GO
/****** Object:  Default [DF_TestPaper_ModificationTeacher]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_ModificationTeacher]  DEFAULT (N'自动生成') FOR [ModificationTeacher]
GO
/****** Object:  Default [DF_TestPaper_ModificationTeacherID]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_ModificationTeacherID]  DEFAULT (N'自动生成') FOR [ModificationTeacherID]
GO
/****** Object:  Default [DF_TestPaper_ModificationDate]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestPaper] ADD  CONSTRAINT [DF_TestPaper_ModificationDate]  DEFAULT (getdate()) FOR [ModificationDate]
GO
/****** Object:  Default [DF_Paper_Active]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper] ADD  CONSTRAINT [DF_Paper_Active]  DEFAULT ((0)) FOR [Active]
GO
/****** Object:  Default [DF_Paper_Audit]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper] ADD  CONSTRAINT [DF_Paper_Audit]  DEFAULT ((0)) FOR [Audit]
GO
/****** Object:  Default [DF_Subject_ParentId]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Subject] ADD  CONSTRAINT [DF_Subject_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
/****** Object:  Default [DF_Resource_DateAdded]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Resource] ADD  CONSTRAINT [DF_Resource_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
/****** Object:  Default [DF_QuestionEssay _QuestionType]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionEssay ] ADD  CONSTRAINT [DF_QuestionEssay _QuestionType]  DEFAULT (N'简答题') FOR [QuestionType]
GO
/****** Object:  Default [DF__QuestionS__Diffi__0AD2A005]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionEssay ] ADD  CONSTRAINT [DF__QuestionS__Diffi__0AD2A005]  DEFAULT ((1)) FOR [Difficulty]
GO
/****** Object:  Default [DF__QuestionS__Frequ__0BC6C43E]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionEssay ] ADD  CONSTRAINT [DF__QuestionS__Frequ__0BC6C43E]  DEFAULT ((1)) FOR [Frequency]
GO
/****** Object:  Default [DF__QuestionS__Audit__52593CB8]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionEssay ] ADD  CONSTRAINT [DF__QuestionS__Audit__52593CB8]  DEFAULT ((0)) FOR [Audit]
GO
/****** Object:  Default [DF_QuestionEssay _UserId]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionEssay ] ADD  CONSTRAINT [DF_QuestionEssay _UserId]  DEFAULT ('System') FOR [ModificationTeacherID]
GO
/****** Object:  Default [DF_QuestionEssay _IsImport]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionEssay ] ADD  CONSTRAINT [DF_QuestionEssay _IsImport]  DEFAULT ((0)) FOR [IsImport]
GO
/****** Object:  Default [DF_QuestionMultipleChoice_OptionD]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionMultipleChoice_OptionD]  DEFAULT ('') FOR [OptionD]
GO
/****** Object:  Default [DF_QuestionMultipleChoice_OptionE]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionMultipleChoice_OptionE]  DEFAULT ('') FOR [OptionE]
GO
/****** Object:  Default [DF_QuestionMultipleChoice_OptionF]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionMultipleChoice_OptionF]  DEFAULT ('') FOR [OptionF]
GO
/****** Object:  Default [DF_QuestionChoice_IsMultiple]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionChoice_IsMultiple]  DEFAULT ((0)) FOR [IsMultiple]
GO
/****** Object:  Default [DF_QuestionMultipleChoice_FisTrue]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionMultipleChoice_FisTrue]  DEFAULT ((0)) FOR [FisTrue]
GO
/****** Object:  Default [DF__QuestionM__Diffi__0CBAE877]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF__QuestionM__Diffi__0CBAE877]  DEFAULT ((1)) FOR [Difficulty]
GO
/****** Object:  Default [DF__QuestionM__Frequ__0DAF0CB0]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF__QuestionM__Frequ__0DAF0CB0]  DEFAULT ((1)) FOR [Frequency]
GO
/****** Object:  Default [DF__QuestionM__Audit__5165187F]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF__QuestionM__Audit__5165187F]  DEFAULT ((0)) FOR [Audit]
GO
/****** Object:  Default [DF_QuestionChoice_UserID]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionChoice_UserID]  DEFAULT ('System') FOR [ModificationTeacherID]
GO
/****** Object:  Default [DF_QuestionChoice_IsImport]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[QuestionChoice] ADD  CONSTRAINT [DF_QuestionChoice_IsImport]  DEFAULT ((0)) FOR [IsImport]
GO
/****** Object:  Default [DF_Paper_Essay_DefaultScore]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper_Essay] ADD  CONSTRAINT [DF_Paper_Essay_DefaultScore]  DEFAULT ((-1)) FOR [DefaultScore]
GO
/****** Object:  Default [DF_Paper_Question_DefaultScore]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper_Choice] ADD  CONSTRAINT [DF_Paper_Question_DefaultScore]  DEFAULT ((-1)) FOR [DefaultScore]
GO
/****** Object:  ForeignKey [FK_User_Subject_Subject]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[User_Subject]  WITH NOCHECK ADD  CONSTRAINT [FK_User_Subject_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[User_Subject] NOCHECK CONSTRAINT [FK_User_Subject_Subject]
GO
/****** Object:  ForeignKey [FK_TestAnswerList_TestPaper]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestAnswerList]  WITH NOCHECK ADD  CONSTRAINT [FK_TestAnswerList_TestPaper] FOREIGN KEY([PaperID])
REFERENCES [dbo].[TestPaper] ([ID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[TestAnswerList] NOCHECK CONSTRAINT [FK_TestAnswerList_TestPaper]
GO
/****** Object:  ForeignKey [FK_Paper_Essay_QuestionEssay ]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper_Essay]  WITH NOCHECK ADD  CONSTRAINT [FK_Paper_Essay_QuestionEssay ] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[QuestionEssay ] ([ID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Paper_Essay] NOCHECK CONSTRAINT [FK_Paper_Essay_QuestionEssay ]
GO
/****** Object:  ForeignKey [FK_Paper_Essay_TestPaper]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper_Essay]  WITH NOCHECK ADD  CONSTRAINT [FK_Paper_Essay_TestPaper] FOREIGN KEY([PaperID])
REFERENCES [dbo].[TestPaper] ([ID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Paper_Essay] NOCHECK CONSTRAINT [FK_Paper_Essay_TestPaper]
GO
/****** Object:  ForeignKey [FK_Paper_Choice_QuestionChoice]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper_Choice]  WITH NOCHECK ADD  CONSTRAINT [FK_Paper_Choice_QuestionChoice] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[QuestionChoice] ([ID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Paper_Choice] NOCHECK CONSTRAINT [FK_Paper_Choice_QuestionChoice]
GO
/****** Object:  ForeignKey [FK_Paper_Choice_TestPaper]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Paper_Choice]  WITH NOCHECK ADD  CONSTRAINT [FK_Paper_Choice_TestPaper] FOREIGN KEY([PaperID])
REFERENCES [dbo].[TestPaper] ([ID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Paper_Choice] NOCHECK CONSTRAINT [FK_Paper_Choice_TestPaper]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_Major_Subject_Major]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Major_Subject]  WITH NOCHECK ADD  CONSTRAINT [FK_Major_Subject_Major] FOREIGN KEY([MajorID])
REFERENCES [dbo].[Major] ([MajorID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Major_Subject] NOCHECK CONSTRAINT [FK_Major_Subject_Major]
GO
/****** Object:  ForeignKey [FK_Major_Subject_Subject]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[Major_Subject]  WITH NOCHECK ADD  CONSTRAINT [FK_Major_Subject_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Major_Subject] NOCHECK CONSTRAINT [FK_Major_Subject_Subject]
GO
/****** Object:  ForeignKey [FK_TestAnswerSubItem_TestAnswerList]    Script Date: 12/12/2015 13:25:35 ******/
ALTER TABLE [dbo].[TestAnswerSubItem]  WITH NOCHECK ADD  CONSTRAINT [FK_TestAnswerSubItem_TestAnswerList] FOREIGN KEY([AnswerListID])
REFERENCES [dbo].[TestAnswerList] ([AnserListID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[TestAnswerSubItem] NOCHECK CONSTRAINT [FK_TestAnswerSubItem_TestAnswerList]
GO
