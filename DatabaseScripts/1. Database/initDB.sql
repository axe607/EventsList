USE [master]
GO
IF EXISTS (SELECT 1 FROM SYS.DATABASES WHERE NAME = 'EventsListDB')
DROP DATABASE EventsListDB
PRINT 'Drop Database [EventsListDB]'
GO
PRINT 'Create Databese [EventsListDB]'
CREATE DATABASE [EventsListDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EventsListDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\EventsListDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EventsListDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\EventsListDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [EventsListDB] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EventsListDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [EventsListDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [EventsListDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [EventsListDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [EventsListDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [EventsListDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [EventsListDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [EventsListDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [EventsListDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [EventsListDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [EventsListDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [EventsListDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [EventsListDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [EventsListDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [EventsListDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [EventsListDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [EventsListDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [EventsListDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [EventsListDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [EventsListDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [EventsListDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [EventsListDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [EventsListDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [EventsListDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [EventsListDB] SET  MULTI_USER 
GO

ALTER DATABASE [EventsListDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [EventsListDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [EventsListDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [EventsListDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [EventsListDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [EventsListDB] SET  READ_WRITE 
GO
:On Error EXIT
PRINT 'Database create:  OK'
