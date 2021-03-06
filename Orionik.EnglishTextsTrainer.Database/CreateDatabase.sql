﻿CREATE DATABASE [EnglishTextsTrainer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EnglishTextsTrainer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.POPKOVMSSERVER\MSSQL\DATA\EnglishTextsTrainer.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EnglishTextsTrainer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.POPKOVMSSERVER\MSSQL\DATA\EnglishTextsTrainer_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EnglishTextsTrainer] SET COMPATIBILITY_LEVEL = 130
GO
ALTER DATABASE [EnglishTextsTrainer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET ARITHABORT OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF)
GO
ALTER DATABASE [EnglishTextsTrainer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EnglishTextsTrainer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EnglishTextsTrainer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EnglishTextsTrainer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EnglishTextsTrainer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EnglishTextsTrainer] SET  READ_WRITE 
GO
ALTER DATABASE [EnglishTextsTrainer] SET RECOVERY FULL 
GO
ALTER DATABASE [EnglishTextsTrainer] SET  MULTI_USER 
GO
ALTER DATABASE [EnglishTextsTrainer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EnglishTextsTrainer] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EnglishTextsTrainer] SET DELAYED_DURABILITY = DISABLED 
GO
USE [EnglishTextsTrainer]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [EnglishTextsTrainer]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [EnglishTextsTrainer] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
