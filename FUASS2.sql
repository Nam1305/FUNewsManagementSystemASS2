USE [master];
GO

IF DB_ID('FUNewsManagement') IS NOT NULL
    DROP DATABASE [FUNewsManagement];
GO

CREATE DATABASE [FUNewsManagement];
GO

USE [FUNewsManagement];
GO

-- ======================
-- CATEGORY TABLE
-- ======================
CREATE TABLE [dbo].[Category](
    [CategoryID] INT IDENTITY(1,1) NOT NULL,
    [CategoryName] NVARCHAR(100) NOT NULL,
    [CategoryDescription] NVARCHAR(250) NOT NULL,
    [ParentCategoryID] INT NULL,
    [IsActive] BIT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);
GO

-- ======================
-- SYSTEM ACCOUNT TABLE
-- ======================
CREATE TABLE [dbo].[SystemAccount](
    [AccountID] INT IDENTITY(1,1) NOT NULL,
    [AccountName] NVARCHAR(100) NOT NULL,
    [AccountEmail] NVARCHAR(70) NOT NULL,
    [AccountRole] INT NOT NULL,
    [AccountPassword] NVARCHAR(70) NOT NULL,
 CONSTRAINT [PK_SystemAccount] PRIMARY KEY CLUSTERED ([AccountID] ASC)
);
GO

-- ======================
-- NEWS ARTICLE TABLE
-- ======================
CREATE TABLE [dbo].[NewsArticle](
    [NewsArticleID] INT IDENTITY(1,1) NOT NULL,
    [NewsTitle] NVARCHAR(400) NOT NULL,
    [Headline] NVARCHAR(150) NOT NULL,
    [CreatedDate] DATETIME DEFAULT GETDATE(),
    [NewsContent] NVARCHAR(MAX) NULL,
    [NewsSource] NVARCHAR(400) NULL,
    [CategoryID] INT NULL,
    [NewsStatus] BIT NULL,
    [CreatedByID] INT NULL,
    [UpdatedByID] INT NULL,
    [ModifiedDate] DATETIME NULL,
 CONSTRAINT [PK_NewsArticle] PRIMARY KEY CLUSTERED ([NewsArticleID] ASC)
);
GO

-- ======================
-- TAG TABLE
-- ======================
CREATE TABLE [dbo].[Tag](
    [TagID] INT IDENTITY(1,1) NOT NULL,
    [TagName] NVARCHAR(50) NOT NULL,
    [Note] NVARCHAR(400) NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([TagID] ASC)
);
GO

-- ======================
-- NEWS TAG (MANY-TO-MANY)
-- ======================
CREATE TABLE [dbo].[NewsTag](
    [NewsArticleID] INT NOT NULL,
    [TagID] INT NOT NULL,
 CONSTRAINT [PK_NewsTag] PRIMARY KEY CLUSTERED ([NewsArticleID], [TagID])
);
GO

-- ======================
-- COMMENT TABLE
-- ======================
CREATE TABLE [dbo].[Comment](
    [CommentID] INT IDENTITY(1,1) NOT NULL,
    [NewsArticleID] INT NOT NULL,
    [AccountID] INT NOT NULL,
    [Content] NVARCHAR(1000) NOT NULL,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([CommentID] ASC)
);
GO

-- ======================
-- FOREIGN KEYS
-- ======================

-- Category self-reference
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [FK_Category_Parent]
FOREIGN KEY ([ParentCategoryID]) REFERENCES [dbo].[Category]([CategoryID]);
GO

-- NewsArticle relationships
ALTER TABLE [dbo].[NewsArticle]
ADD CONSTRAINT [FK_NewsArticle_Category]
FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Category]([CategoryID])
ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[NewsArticle]
ADD CONSTRAINT [FK_NewsArticle_CreatedBy]
FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[SystemAccount]([AccountID]);
GO
ALTER TABLE [dbo].[NewsArticle]
ADD CONSTRAINT [FK_NewsArticle_UpdatedBy]
FOREIGN KEY ([UpdatedByID]) REFERENCES [dbo].[SystemAccount]([AccountID]);
GO

-- NewsTag relationships (with delete cascade)
ALTER TABLE [dbo].[NewsTag]
ADD CONSTRAINT [FK_NewsTag_NewsArticle]
FOREIGN KEY ([NewsArticleID]) REFERENCES [dbo].[NewsArticle]([NewsArticleID])
ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[NewsTag]
ADD CONSTRAINT [FK_NewsTag_Tag]
FOREIGN KEY ([TagID]) REFERENCES [dbo].[Tag]([TagID])
ON DELETE CASCADE;
GO

-- Comment relationships
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_Comment_NewsArticle]
FOREIGN KEY ([NewsArticleID]) REFERENCES [dbo].[NewsArticle]([NewsArticleID])
ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_Comment_Account]
FOREIGN KEY ([AccountID]) REFERENCES [dbo].[SystemAccount]([AccountID]);
GO

-- ======================
-- SAMPLE DATA
-- ======================
SET IDENTITY_INSERT [dbo].[Category] ON;
INSERT INTO [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDescription], [ParentCategoryID], [IsActive])
VALUES
(1, N'Academic news', N'Articles about research findings and academic announcements.', NULL, 1),
(2, N'Student Affairs', N'Articles about student activities, events, and initiatives.', NULL, 1),
(3, N'Campus Safety', N'Articles about campus incidents and safety measures.', NULL, 1),
(4, N'Alumni News', N'Articles about alumni achievements and career successes.', NULL, 1),
(5, N'Capstone Project News', N'Comprehensive reports on academic capstone projects.', NULL, 0);
SET IDENTITY_INSERT [dbo].[Category] OFF;
GO

INSERT INTO [dbo].[SystemAccount] ([AccountName], [AccountEmail], [AccountRole], [AccountPassword])
VALUES
(N'Emma William', N'EmmaWilliam@FUNewsManagement.org', 2, N'@1'),
(N'Olivia James', N'OliviaJames@FUNewsManagement.org', 2, N'@1'),
(N'Isabella David', N'IsabellaDavid@FUNewsManagement.org', 1, N'@1'),
(N'Michael Charlotte', N'MichaelCharlotte@FUNewsManagement.org', 1, N'@1'),
(N'Steve Paris', N'SteveParis@FUNewsManagement.org', 1, N'@1');
GO

INSERT INTO [dbo].[Tag] ([TagName], [Note])
VALUES
(N'Education', N'Education Note'),
(N'Technology', N'Technology Note'),
(N'Research', N'Research Note'),
(N'Innovation', N'Innovation Note'),
(N'Campus Life', N'Campus Life Note'),
(N'Faculty', N'Faculty Achievements'),
(N'Alumni', N'Alumni News'),
(N'Events', N'University Events'),
(N'Resources', N'Campus Resources');
GO

INSERT INTO [dbo].[NewsArticle] ([NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate])
VALUES
(N'University FU Celebrates Alumni Success', N'Alumni Success Celebration', GETDATE(), N'Sample content', N'Internet', 4, 1, 1, 1, GETDATE());
GO

INSERT INTO [dbo].[Comment] ([NewsArticleID], [AccountID], [Content])
VALUES
(1, 2, N'Great article! Really inspiring.'),
(1, 3, N'Proud to be an FU alumni!');
GO