
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2010 23:36:00
-- Generated from EDMX file: C:\Users\Honza\documents\visual studio 2010\Projects\Felbook\Felbook\Models\FelbookModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Felbook];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserGroupMembership_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroupMembership] DROP CONSTRAINT [FK_UserGroupMembership_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupMembership_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroupMembership] DROP CONSTRAINT [FK_UserGroupMembership_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupAdministration_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupAdministration] DROP CONSTRAINT [FK_GroupAdministration_User];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupAdministration_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupAdministration] DROP CONSTRAINT [FK_GroupAdministration_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_Followings_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Followings] DROP CONSTRAINT [FK_Followings_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Followings_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Followings] DROP CONSTRAINT [FK_Followings_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageReaders_Message]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageReaders] DROP CONSTRAINT [FK_MessageReaders_Message];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageReaders_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageReaders] DROP CONSTRAINT [FK_MessageReaders_User];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageReplies]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_MessageReplies];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_StatusInformationComment];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationLinks_StatusInformation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusInformationLinks] DROP CONSTRAINT [FK_StatusInformationLinks_StatusInformation];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationLinks_Link]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusInformationLinks] DROP CONSTRAINT [FK_StatusInformationLinks_Link];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationImages_StatusInformation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusInformationImages] DROP CONSTRAINT [FK_StatusInformationImages_StatusInformation];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationImages_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusInformationImages] DROP CONSTRAINT [FK_StatusInformationImages_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationFiles_StatusInformation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusInformationFiles] DROP CONSTRAINT [FK_StatusInformationFiles_StatusInformation];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusInformationFiles_File]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusInformationFiles] DROP CONSTRAINT [FK_StatusInformationFiles_File];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupStatuses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusSet] DROP CONSTRAINT [FK_GroupStatuses];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStatuses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StatusSet] DROP CONSTRAINT [FK_UserStatuses];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupCreator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSet] DROP CONSTRAINT [FK_UserGroupCreator];
GO
IF OBJECT_ID(N'[dbo].[FK_SentMessages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_SentMessages];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupChildren]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSet] DROP CONSTRAINT [FK_GroupChildren];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[GroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupSet];
GO
IF OBJECT_ID(N'[dbo].[MessageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageSet];
GO
IF OBJECT_ID(N'[dbo].[StatusSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatusSet];
GO
IF OBJECT_ID(N'[dbo].[LinkSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinkSet];
GO
IF OBJECT_ID(N'[dbo].[CommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentSet];
GO
IF OBJECT_ID(N'[dbo].[FileSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileSet];
GO
IF OBJECT_ID(N'[dbo].[ImageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageSet];
GO
IF OBJECT_ID(N'[dbo].[UserGroupMembership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroupMembership];
GO
IF OBJECT_ID(N'[dbo].[GroupAdministration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupAdministration];
GO
IF OBJECT_ID(N'[dbo].[Followings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Followings];
GO
IF OBJECT_ID(N'[dbo].[MessageReaders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageReaders];
GO
IF OBJECT_ID(N'[dbo].[StatusInformationLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatusInformationLinks];
GO
IF OBJECT_ID(N'[dbo].[StatusInformationImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatusInformationImages];
GO
IF OBJECT_ID(N'[dbo].[StatusInformationFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatusInformationFiles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [LastLogged] datetime  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GroupSet'
CREATE TABLE [dbo].[GroupSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Creator_Id] int  NULL,
    [Children_Id] int  NULL
);
GO

-- Creating table 'MessageSet'
CREATE TABLE [dbo].[MessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [Replies_Id] int  NULL,
    [Sender_Id] int  NOT NULL
);
GO

-- Creating table 'StatusSet'
CREATE TABLE [dbo].[StatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [Group_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'LinkSet'
CREATE TABLE [dbo].[LinkSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [URL] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [StatusInformationComment_Comment_Id] int  NOT NULL
);
GO

-- Creating table 'FileSet'
CREATE TABLE [dbo].[FileSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ImageSet'
CREATE TABLE [dbo].[ImageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserGroupMembership'
CREATE TABLE [dbo].[UserGroupMembership] (
    [Users_Id] int  NOT NULL,
    [JoinedGroups_Id] int  NOT NULL
);
GO

-- Creating table 'GroupAdministration'
CREATE TABLE [dbo].[GroupAdministration] (
    [Administrators_Id] int  NOT NULL,
    [AdminedGroups_Id] int  NOT NULL
);
GO

-- Creating table 'Followings'
CREATE TABLE [dbo].[Followings] (
    [Followers_Id] int  NOT NULL,
    [Followings_Id] int  NOT NULL
);
GO

-- Creating table 'MessageReaders'
CREATE TABLE [dbo].[MessageReaders] (
    [Messages_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- Creating table 'StatusInformationLinks'
CREATE TABLE [dbo].[StatusInformationLinks] (
    [StatusInformationLinks_Link_Id] int  NOT NULL,
    [Links_Id] int  NOT NULL
);
GO

-- Creating table 'StatusInformationImages'
CREATE TABLE [dbo].[StatusInformationImages] (
    [StatusInformationImages_Image_Id] int  NOT NULL,
    [Images_Id] int  NOT NULL
);
GO

-- Creating table 'StatusInformationFiles'
CREATE TABLE [dbo].[StatusInformationFiles] (
    [StatusInformationFiles_File_Id] int  NOT NULL,
    [Files_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupSet'
ALTER TABLE [dbo].[GroupSet]
ADD CONSTRAINT [PK_GroupSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [PK_MessageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StatusSet'
ALTER TABLE [dbo].[StatusSet]
ADD CONSTRAINT [PK_StatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LinkSet'
ALTER TABLE [dbo].[LinkSet]
ADD CONSTRAINT [PK_LinkSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileSet'
ALTER TABLE [dbo].[FileSet]
ADD CONSTRAINT [PK_FileSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [PK_ImageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Users_Id], [JoinedGroups_Id] in table 'UserGroupMembership'
ALTER TABLE [dbo].[UserGroupMembership]
ADD CONSTRAINT [PK_UserGroupMembership]
    PRIMARY KEY NONCLUSTERED ([Users_Id], [JoinedGroups_Id] ASC);
GO

-- Creating primary key on [Administrators_Id], [AdminedGroups_Id] in table 'GroupAdministration'
ALTER TABLE [dbo].[GroupAdministration]
ADD CONSTRAINT [PK_GroupAdministration]
    PRIMARY KEY NONCLUSTERED ([Administrators_Id], [AdminedGroups_Id] ASC);
GO

-- Creating primary key on [Followers_Id], [Followings_Id] in table 'Followings'
ALTER TABLE [dbo].[Followings]
ADD CONSTRAINT [PK_Followings]
    PRIMARY KEY NONCLUSTERED ([Followers_Id], [Followings_Id] ASC);
GO

-- Creating primary key on [Messages_Id], [Users_Id] in table 'MessageReaders'
ALTER TABLE [dbo].[MessageReaders]
ADD CONSTRAINT [PK_MessageReaders]
    PRIMARY KEY NONCLUSTERED ([Messages_Id], [Users_Id] ASC);
GO

-- Creating primary key on [StatusInformationLinks_Link_Id], [Links_Id] in table 'StatusInformationLinks'
ALTER TABLE [dbo].[StatusInformationLinks]
ADD CONSTRAINT [PK_StatusInformationLinks]
    PRIMARY KEY NONCLUSTERED ([StatusInformationLinks_Link_Id], [Links_Id] ASC);
GO

-- Creating primary key on [StatusInformationImages_Image_Id], [Images_Id] in table 'StatusInformationImages'
ALTER TABLE [dbo].[StatusInformationImages]
ADD CONSTRAINT [PK_StatusInformationImages]
    PRIMARY KEY NONCLUSTERED ([StatusInformationImages_Image_Id], [Images_Id] ASC);
GO

-- Creating primary key on [StatusInformationFiles_File_Id], [Files_Id] in table 'StatusInformationFiles'
ALTER TABLE [dbo].[StatusInformationFiles]
ADD CONSTRAINT [PK_StatusInformationFiles]
    PRIMARY KEY NONCLUSTERED ([StatusInformationFiles_File_Id], [Files_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Users_Id] in table 'UserGroupMembership'
ALTER TABLE [dbo].[UserGroupMembership]
ADD CONSTRAINT [FK_UserGroupMembership_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JoinedGroups_Id] in table 'UserGroupMembership'
ALTER TABLE [dbo].[UserGroupMembership]
ADD CONSTRAINT [FK_UserGroupMembership_Group]
    FOREIGN KEY ([JoinedGroups_Id])
    REFERENCES [dbo].[GroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupMembership_Group'
CREATE INDEX [IX_FK_UserGroupMembership_Group]
ON [dbo].[UserGroupMembership]
    ([JoinedGroups_Id]);
GO

-- Creating foreign key on [Administrators_Id] in table 'GroupAdministration'
ALTER TABLE [dbo].[GroupAdministration]
ADD CONSTRAINT [FK_GroupAdministration_User]
    FOREIGN KEY ([Administrators_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AdminedGroups_Id] in table 'GroupAdministration'
ALTER TABLE [dbo].[GroupAdministration]
ADD CONSTRAINT [FK_GroupAdministration_Group]
    FOREIGN KEY ([AdminedGroups_Id])
    REFERENCES [dbo].[GroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupAdministration_Group'
CREATE INDEX [IX_FK_GroupAdministration_Group]
ON [dbo].[GroupAdministration]
    ([AdminedGroups_Id]);
GO

-- Creating foreign key on [Followers_Id] in table 'Followings'
ALTER TABLE [dbo].[Followings]
ADD CONSTRAINT [FK_Followings_User]
    FOREIGN KEY ([Followers_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Followings_Id] in table 'Followings'
ALTER TABLE [dbo].[Followings]
ADD CONSTRAINT [FK_Followings_User1]
    FOREIGN KEY ([Followings_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Followings_User1'
CREATE INDEX [IX_FK_Followings_User1]
ON [dbo].[Followings]
    ([Followings_Id]);
GO

-- Creating foreign key on [Messages_Id] in table 'MessageReaders'
ALTER TABLE [dbo].[MessageReaders]
ADD CONSTRAINT [FK_MessageReaders_Message]
    FOREIGN KEY ([Messages_Id])
    REFERENCES [dbo].[MessageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'MessageReaders'
ALTER TABLE [dbo].[MessageReaders]
ADD CONSTRAINT [FK_MessageReaders_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageReaders_User'
CREATE INDEX [IX_FK_MessageReaders_User]
ON [dbo].[MessageReaders]
    ([Users_Id]);
GO

-- Creating foreign key on [Replies_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_MessageReplies]
    FOREIGN KEY ([Replies_Id])
    REFERENCES [dbo].[MessageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageReplies'
CREATE INDEX [IX_FK_MessageReplies]
ON [dbo].[MessageSet]
    ([Replies_Id]);
GO

-- Creating foreign key on [StatusInformationComment_Comment_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_StatusInformationComment]
    FOREIGN KEY ([StatusInformationComment_Comment_Id])
    REFERENCES [dbo].[StatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusInformationComment'
CREATE INDEX [IX_FK_StatusInformationComment]
ON [dbo].[CommentSet]
    ([StatusInformationComment_Comment_Id]);
GO

-- Creating foreign key on [StatusInformationLinks_Link_Id] in table 'StatusInformationLinks'
ALTER TABLE [dbo].[StatusInformationLinks]
ADD CONSTRAINT [FK_StatusInformationLinks_StatusInformation]
    FOREIGN KEY ([StatusInformationLinks_Link_Id])
    REFERENCES [dbo].[StatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Links_Id] in table 'StatusInformationLinks'
ALTER TABLE [dbo].[StatusInformationLinks]
ADD CONSTRAINT [FK_StatusInformationLinks_Link]
    FOREIGN KEY ([Links_Id])
    REFERENCES [dbo].[LinkSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusInformationLinks_Link'
CREATE INDEX [IX_FK_StatusInformationLinks_Link]
ON [dbo].[StatusInformationLinks]
    ([Links_Id]);
GO

-- Creating foreign key on [StatusInformationImages_Image_Id] in table 'StatusInformationImages'
ALTER TABLE [dbo].[StatusInformationImages]
ADD CONSTRAINT [FK_StatusInformationImages_StatusInformation]
    FOREIGN KEY ([StatusInformationImages_Image_Id])
    REFERENCES [dbo].[StatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Images_Id] in table 'StatusInformationImages'
ALTER TABLE [dbo].[StatusInformationImages]
ADD CONSTRAINT [FK_StatusInformationImages_Image]
    FOREIGN KEY ([Images_Id])
    REFERENCES [dbo].[ImageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusInformationImages_Image'
CREATE INDEX [IX_FK_StatusInformationImages_Image]
ON [dbo].[StatusInformationImages]
    ([Images_Id]);
GO

-- Creating foreign key on [StatusInformationFiles_File_Id] in table 'StatusInformationFiles'
ALTER TABLE [dbo].[StatusInformationFiles]
ADD CONSTRAINT [FK_StatusInformationFiles_StatusInformation]
    FOREIGN KEY ([StatusInformationFiles_File_Id])
    REFERENCES [dbo].[StatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Files_Id] in table 'StatusInformationFiles'
ALTER TABLE [dbo].[StatusInformationFiles]
ADD CONSTRAINT [FK_StatusInformationFiles_File]
    FOREIGN KEY ([Files_Id])
    REFERENCES [dbo].[FileSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusInformationFiles_File'
CREATE INDEX [IX_FK_StatusInformationFiles_File]
ON [dbo].[StatusInformationFiles]
    ([Files_Id]);
GO

-- Creating foreign key on [Group_Id] in table 'StatusSet'
ALTER TABLE [dbo].[StatusSet]
ADD CONSTRAINT [FK_GroupStatuses]
    FOREIGN KEY ([Group_Id])
    REFERENCES [dbo].[GroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupStatuses'
CREATE INDEX [IX_FK_GroupStatuses]
ON [dbo].[StatusSet]
    ([Group_Id]);
GO

-- Creating foreign key on [User_Id] in table 'StatusSet'
ALTER TABLE [dbo].[StatusSet]
ADD CONSTRAINT [FK_UserStatuses]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatuses'
CREATE INDEX [IX_FK_UserStatuses]
ON [dbo].[StatusSet]
    ([User_Id]);
GO

-- Creating foreign key on [Creator_Id] in table 'GroupSet'
ALTER TABLE [dbo].[GroupSet]
ADD CONSTRAINT [FK_UserGroupCreator]
    FOREIGN KEY ([Creator_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupCreator'
CREATE INDEX [IX_FK_UserGroupCreator]
ON [dbo].[GroupSet]
    ([Creator_Id]);
GO

-- Creating foreign key on [Sender_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_SentMessages]
    FOREIGN KEY ([Sender_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentMessages'
CREATE INDEX [IX_FK_SentMessages]
ON [dbo].[MessageSet]
    ([Sender_Id]);
GO

-- Creating foreign key on [Children_Id] in table 'GroupSet'
ALTER TABLE [dbo].[GroupSet]
ADD CONSTRAINT [FK_GroupChildren]
    FOREIGN KEY ([Children_Id])
    REFERENCES [dbo].[GroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupChildren'
CREATE INDEX [IX_FK_GroupChildren]
ON [dbo].[GroupSet]
    ([Children_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------