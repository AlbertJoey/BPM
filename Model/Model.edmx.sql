
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/06/2016 17:06:23
-- Generated from EDMX file: E:\svn\Eage.BPM.Site\Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BPM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Sys_Buttons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Buttons];
GO
IF OBJECT_ID(N'[dbo].[Sys_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Departments];
GO
IF OBJECT_ID(N'[dbo].[Sys_LogDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_LogDetails];
GO
IF OBJECT_ID(N'[dbo].[Sys_NavButtons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_NavButtons];
GO
IF OBJECT_ID(N'[dbo].[Sys_Navigations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Navigations];
GO
IF OBJECT_ID(N'[dbo].[Sys_RoleNavBtns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_RoleNavBtns];
GO
IF OBJECT_ID(N'[dbo].[Sys_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Roles];
GO
IF OBJECT_ID(N'[dbo].[Sys_Roles_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Roles_Departments];
GO
IF OBJECT_ID(N'[dbo].[Sys_UserNavBtns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_UserNavBtns];
GO
IF OBJECT_ID(N'[dbo].[Sys_UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users_Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users_Departments];
GO
IF OBJECT_ID(N'[BPMModelStoreContainer].[Log]', 'U') IS NOT NULL
    DROP TABLE [BPMModelStoreContainer].[Log];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Sys_Buttons'
CREATE TABLE [dbo].[Sys_Buttons] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [ButtonText] nvarchar(50)  NULL,
    [Sortnum] int  NULL,
    [iconCls] nvarchar(50)  NULL,
    [IconUrl] nvarchar(50)  NULL,
    [ButtonTag] nvarchar(50)  NULL,
    [Remark] nvarchar(50)  NULL,
    [ButtonHtml] nvarchar(2000)  NULL
);
GO

-- Creating table 'Sys_Departments'
CREATE TABLE [dbo].[Sys_Departments] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(50)  NULL,
    [ParentId] int  NULL,
    [Sortnum] int  NULL,
    [Remark] nvarchar(500)  NULL
);
GO

-- Creating table 'Sys_LogDetails'
CREATE TABLE [dbo].[Sys_LogDetails] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [LogId] int  NULL,
    [FieldName] nvarchar(50)  NULL,
    [FieldText] nvarchar(50)  NULL,
    [OldValue] nvarchar(max)  NULL,
    [NewValue] nvarchar(max)  NULL,
    [Remark] nvarchar(500)  NULL,
    [AddTime] datetime  NULL
);
GO

-- Creating table 'Sys_NavButtons'
CREATE TABLE [dbo].[Sys_NavButtons] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [NavId] int  NULL,
    [ButtonId] int  NULL,
    [Sortnum] int  NULL
);
GO

-- Creating table 'Sys_RoleNavBtns'
CREATE TABLE [dbo].[Sys_RoleNavBtns] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NULL,
    [NavId] int  NULL,
    [BtnId] int  NULL
);
GO

-- Creating table 'Sys_Roles'
CREATE TABLE [dbo].[Sys_Roles] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(50)  NULL,
    [Sortnum] int  NULL,
    [Remark] nvarchar(500)  NULL,
    [isDefault] int  NULL,
    [DepId] int  NULL
);
GO

-- Creating table 'Sys_Roles_Departments'
CREATE TABLE [dbo].[Sys_Roles_Departments] (
    [KeyId] bigint IDENTITY(1,1) NOT NULL,
    [roleId] int  NULL,
    [depId] int  NULL
);
GO

-- Creating table 'Sys_UserNavBtns'
CREATE TABLE [dbo].[Sys_UserNavBtns] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [NavId] int  NULL,
    [BtnId] int  NULL
);
GO

-- Creating table 'Sys_UserRoles'
CREATE TABLE [dbo].[Sys_UserRoles] (
    [Keyid] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [RoleID] int  NULL
);
GO

-- Creating table 'Sys_Users'
CREATE TABLE [dbo].[Sys_Users] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [PassSalt] nvarchar(50)  NULL,
    [Email] nvarchar(500)  NULL,
    [IsAdmin] bit  NULL,
    [IsDisabled] bit  NULL,
    [TrueName] nvarchar(50)  NULL,
    [DepartmentId] int  NULL,
    [Mobile] nvarchar(50)  NULL,
    [QQ] nvarchar(50)  NULL,
    [Remark] nvarchar(500)  NULL,
    [AddTime] datetime  NULL,
    [MenusJson] nvarchar(max)  NULL,
    [ConfigJson] nvarchar(max)  NULL
);
GO

-- Creating table 'Sys_Users_Departments'
CREATE TABLE [dbo].[Sys_Users_Departments] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [DepID] int  NULL
);
GO

-- Creating table 'Log'
CREATE TABLE [dbo].[Log] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Thread] varchar(255)  NOT NULL,
    [Level] varchar(50)  NOT NULL,
    [Logger] varchar(255)  NOT NULL,
    [Message] varchar(4000)  NOT NULL,
    [Exception] varchar(2000)  NULL
);
GO

-- Creating table 'Sys_Navigations'
CREATE TABLE [dbo].[Sys_Navigations] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [NavTitle] nvarchar(50)  NULL,
    [Linkurl] nvarchar(50)  NULL,
    [Sortnum] int  NULL,
    [iconCls] nvarchar(50)  NULL,
    [iconUrl] nvarchar(50)  NULL,
    [IsVisible] bit  NULL,
    [ParentID] int  NULL,
    [NavTag] nvarchar(50)  NULL,
    [BigImageUrl] nvarchar(500)  NULL,
    [IsNewWindow] bit  NULL,
    [WinWidth] int  NULL,
    [WinHeight] int  NULL,
    [NavArea] nvarchar(500)  NULL,
    [NavController] nvarchar(500)  NULL,
    [NavAction] nvarchar(500)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [KeyId] in table 'Sys_Buttons'
ALTER TABLE [dbo].[Sys_Buttons]
ADD CONSTRAINT [PK_Sys_Buttons]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_Departments'
ALTER TABLE [dbo].[Sys_Departments]
ADD CONSTRAINT [PK_Sys_Departments]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_LogDetails'
ALTER TABLE [dbo].[Sys_LogDetails]
ADD CONSTRAINT [PK_Sys_LogDetails]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_NavButtons'
ALTER TABLE [dbo].[Sys_NavButtons]
ADD CONSTRAINT [PK_Sys_NavButtons]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_RoleNavBtns'
ALTER TABLE [dbo].[Sys_RoleNavBtns]
ADD CONSTRAINT [PK_Sys_RoleNavBtns]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_Roles'
ALTER TABLE [dbo].[Sys_Roles]
ADD CONSTRAINT [PK_Sys_Roles]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_Roles_Departments'
ALTER TABLE [dbo].[Sys_Roles_Departments]
ADD CONSTRAINT [PK_Sys_Roles_Departments]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_UserNavBtns'
ALTER TABLE [dbo].[Sys_UserNavBtns]
ADD CONSTRAINT [PK_Sys_UserNavBtns]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [Keyid] in table 'Sys_UserRoles'
ALTER TABLE [dbo].[Sys_UserRoles]
ADD CONSTRAINT [PK_Sys_UserRoles]
    PRIMARY KEY CLUSTERED ([Keyid] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_Users'
ALTER TABLE [dbo].[Sys_Users]
ADD CONSTRAINT [PK_Sys_Users]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_Users_Departments'
ALTER TABLE [dbo].[Sys_Users_Departments]
ADD CONSTRAINT [PK_Sys_Users_Departments]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [Id], [Date], [Thread], [Level], [Logger], [Message] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [PK_Log]
    PRIMARY KEY CLUSTERED ([Id], [Date], [Thread], [Level], [Logger], [Message] ASC);
GO

-- Creating primary key on [KeyId] in table 'Sys_Navigations'
ALTER TABLE [dbo].[Sys_Navigations]
ADD CONSTRAINT [PK_Sys_Navigations]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------