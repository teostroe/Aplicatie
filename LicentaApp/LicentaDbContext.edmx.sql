
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/20/2018 23:03:44
-- Generated from EDMX file: D:\CSIE\Licenta\Licenta2\LicentaApp\LicentaApp\LicentaDbContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LicentaTeoStroe];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Comenzi_IdClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comenzi] DROP CONSTRAINT [FK_Comenzi_IdClient];
GO
IF OBJECT_ID(N'[dbo].[FK_Comenzi_IdUtilizator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comenzi] DROP CONSTRAINT [FK_Comenzi_IdUtilizator];
GO
IF OBJECT_ID(N'[dbo].[FK_RandComenziProdus_780915676]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RandComenziProduse] DROP CONSTRAINT [FK_RandComenziProdus_780915676];
GO
IF OBJECT_ID(N'[dbo].[FK_ViziteMedicale_I_1739780243]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ViziteMedicale] DROP CONSTRAINT [FK_ViziteMedicale_I_1739780243];
GO
IF OBJECT_ID(N'[dbo].[FK_DetaliiProdus_IdProdus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetaliiProdus] DROP CONSTRAINT [FK_DetaliiProdus_IdProdus];
GO
IF OBJECT_ID(N'[dbo].[FK_Produse_IdFurnizor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Produse] DROP CONSTRAINT [FK_Produse_IdFurnizor];
GO
IF OBJECT_ID(N'[dbo].[FK_Inventar_IdMagazin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventar] DROP CONSTRAINT [FK_Inventar_IdMagazin];
GO
IF OBJECT_ID(N'[dbo].[FK_Inventar_IdProdus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventar] DROP CONSTRAINT [FK_Inventar_IdProdus];
GO
IF OBJECT_ID(N'[dbo].[FK_Utilizatori_IdMagazin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilizatori] DROP CONSTRAINT [FK_Utilizatori_IdMagazin];
GO
IF OBJECT_ID(N'[dbo].[FK_Preturi_IdProdus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Preturi] DROP CONSTRAINT [FK_Preturi_IdProdus];
GO
IF OBJECT_ID(N'[dbo].[FK_RandComenziProduse_IdProdus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RandComenziProduse] DROP CONSTRAINT [FK_RandComenziProduse_IdProdus];
GO
IF OBJECT_ID(N'[dbo].[FK_Utilizatori_IdRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilizatori] DROP CONSTRAINT [FK_Utilizatori_IdRol];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clienti]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clienti];
GO
IF OBJECT_ID(N'[dbo].[Comenzi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comenzi];
GO
IF OBJECT_ID(N'[dbo].[DetaliiProdus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetaliiProdus];
GO
IF OBJECT_ID(N'[dbo].[Furnizori]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Furnizori];
GO
IF OBJECT_ID(N'[dbo].[Inventar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inventar];
GO
IF OBJECT_ID(N'[dbo].[Magazine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Magazine];
GO
IF OBJECT_ID(N'[dbo].[Preturi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Preturi];
GO
IF OBJECT_ID(N'[dbo].[Produse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Produse];
GO
IF OBJECT_ID(N'[dbo].[RandComenziProduse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RandComenziProduse];
GO
IF OBJECT_ID(N'[dbo].[Roluri]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roluri];
GO
IF OBJECT_ID(N'[dbo].[Utilizatori]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilizatori];
GO
IF OBJECT_ID(N'[dbo].[ViziteMedicale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViziteMedicale];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clienti'
CREATE TABLE [dbo].[Clienti] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nume] nvarchar(50)  NOT NULL,
    [Prenume] nvarchar(50)  NOT NULL,
    [NumarTelefon] nvarchar(10)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [DataNastere] datetime  NOT NULL,
    [DataInregistrare] datetime  NOT NULL,
    [Profesie] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Comenzi'
CREATE TABLE [dbo].[Comenzi] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Data] datetime  NOT NULL,
    [Discount] decimal(18,2)  NULL,
    [IdUtilizator] int  NOT NULL,
    [IdClient] int  NOT NULL
);
GO

-- Creating table 'DetaliiProdus'
CREATE TABLE [dbo].[DetaliiProdus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Denumire] nvarchar(255)  NOT NULL,
    [Valoare] nvarchar(255)  NOT NULL,
    [IdProdus] int  NOT NULL
);
GO

-- Creating table 'Furnizori'
CREATE TABLE [dbo].[Furnizori] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Denumire] nvarchar(50)  NOT NULL,
    [CIF] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [NumarTelefon] nvarchar(10)  NOT NULL,
    [Adresa] nvarchar(150)  NOT NULL,
    [NRREGCOMERTULUI] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'Inventar'
CREATE TABLE [dbo].[Inventar] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CantitateDisponibila] int  NOT NULL,
    [IdMagazin] int  NOT NULL,
    [IdProdus] int  NOT NULL
);
GO

-- Creating table 'Magazine'
CREATE TABLE [dbo].[Magazine] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Denumire] nvarchar(50)  NOT NULL,
    [Oras] nvarchar(50)  NOT NULL,
    [Adresa] nvarchar(max)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Preturi'
CREATE TABLE [dbo].[Preturi] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valoare] decimal(18,2)  NOT NULL,
    [DataActualizare] datetime  NOT NULL,
    [EsteUtilizatAcum] int  NOT NULL,
    [IdProdus] int  NOT NULL
);
GO

-- Creating table 'Produse'
CREATE TABLE [dbo].[Produse] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cod] nvarchar(100)  NOT NULL,
    [Denumire] nvarchar(150)  NOT NULL,
    [Discount] decimal(18,2)  NULL,
    [TipProdus] int  NOT NULL,
    [IdFurnizor] int  NOT NULL
);
GO

-- Creating table 'RandComenziProduse'
CREATE TABLE [dbo].[RandComenziProduse] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantitate] int  NOT NULL,
    [IdComanda] int  NOT NULL,
    [IdProdus] int  NOT NULL,
    [TipTratement] int  NULL,
    [TipCuloare] int  NULL,
    [Discount] decimal(10,2)  NULL
);
GO

-- Creating table 'Roluri'
CREATE TABLE [dbo].[Roluri] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Denumire] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'Utilizatori'
CREATE TABLE [dbo].[Utilizatori] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Nume] nvarchar(50)  NOT NULL,
    [Prenume] nvarchar(50)  NOT NULL,
    [Parola] nvarchar(25)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [IdRol] int  NOT NULL,
    [IdMagazin] int  NOT NULL
);
GO

-- Creating table 'ViziteMedicale'
CREATE TABLE [dbo].[ViziteMedicale] (
    [IdComandaVizitaMedicala] int  NOT NULL,
    [DistantaPupilara] decimal(18,0)  NOT NULL,
    [SferaDistantaStang] decimal(18,0)  NULL,
    [SferaAproapeStang] decimal(18,0)  NULL,
    [CilindruStang] decimal(18,0)  NULL,
    [AxStang] decimal(18,0)  NULL,
    [PrismaStang] decimal(18,0)  NULL,
    [SferaDistantaDrept] decimal(18,0)  NULL,
    [SferaAproapeDrept] decimal(18,0)  NULL,
    [CilindruDrept] decimal(18,0)  NULL,
    [AxDrept] decimal(18,0)  NULL,
    [PrismaDrept] decimal(18,0)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Clienti'
ALTER TABLE [dbo].[Clienti]
ADD CONSTRAINT [PK_Clienti]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comenzi'
ALTER TABLE [dbo].[Comenzi]
ADD CONSTRAINT [PK_Comenzi]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetaliiProdus'
ALTER TABLE [dbo].[DetaliiProdus]
ADD CONSTRAINT [PK_DetaliiProdus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Furnizori'
ALTER TABLE [dbo].[Furnizori]
ADD CONSTRAINT [PK_Furnizori]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Inventar'
ALTER TABLE [dbo].[Inventar]
ADD CONSTRAINT [PK_Inventar]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Magazine'
ALTER TABLE [dbo].[Magazine]
ADD CONSTRAINT [PK_Magazine]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Preturi'
ALTER TABLE [dbo].[Preturi]
ADD CONSTRAINT [PK_Preturi]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Produse'
ALTER TABLE [dbo].[Produse]
ADD CONSTRAINT [PK_Produse]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RandComenziProduse'
ALTER TABLE [dbo].[RandComenziProduse]
ADD CONSTRAINT [PK_RandComenziProduse]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roluri'
ALTER TABLE [dbo].[Roluri]
ADD CONSTRAINT [PK_Roluri]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Utilizatori'
ALTER TABLE [dbo].[Utilizatori]
ADD CONSTRAINT [PK_Utilizatori]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdComandaVizitaMedicala] in table 'ViziteMedicale'
ALTER TABLE [dbo].[ViziteMedicale]
ADD CONSTRAINT [PK_ViziteMedicale]
    PRIMARY KEY CLUSTERED ([IdComandaVizitaMedicala] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdClient] in table 'Comenzi'
ALTER TABLE [dbo].[Comenzi]
ADD CONSTRAINT [FK_Comenzi_IdClient]
    FOREIGN KEY ([IdClient])
    REFERENCES [dbo].[Clienti]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comenzi_IdClient'
CREATE INDEX [IX_FK_Comenzi_IdClient]
ON [dbo].[Comenzi]
    ([IdClient]);
GO

-- Creating foreign key on [IdUtilizator] in table 'Comenzi'
ALTER TABLE [dbo].[Comenzi]
ADD CONSTRAINT [FK_Comenzi_IdUtilizator]
    FOREIGN KEY ([IdUtilizator])
    REFERENCES [dbo].[Utilizatori]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comenzi_IdUtilizator'
CREATE INDEX [IX_FK_Comenzi_IdUtilizator]
ON [dbo].[Comenzi]
    ([IdUtilizator]);
GO

-- Creating foreign key on [IdComanda] in table 'RandComenziProduse'
ALTER TABLE [dbo].[RandComenziProduse]
ADD CONSTRAINT [FK_RandComenziProdus_780915676]
    FOREIGN KEY ([IdComanda])
    REFERENCES [dbo].[Comenzi]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RandComenziProdus_780915676'
CREATE INDEX [IX_FK_RandComenziProdus_780915676]
ON [dbo].[RandComenziProduse]
    ([IdComanda]);
GO

-- Creating foreign key on [IdComandaVizitaMedicala] in table 'ViziteMedicale'
ALTER TABLE [dbo].[ViziteMedicale]
ADD CONSTRAINT [FK_ViziteMedicale_I_1739780243]
    FOREIGN KEY ([IdComandaVizitaMedicala])
    REFERENCES [dbo].[Comenzi]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdProdus] in table 'DetaliiProdus'
ALTER TABLE [dbo].[DetaliiProdus]
ADD CONSTRAINT [FK_DetaliiProdus_IdProdus]
    FOREIGN KEY ([IdProdus])
    REFERENCES [dbo].[Produse]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetaliiProdus_IdProdus'
CREATE INDEX [IX_FK_DetaliiProdus_IdProdus]
ON [dbo].[DetaliiProdus]
    ([IdProdus]);
GO

-- Creating foreign key on [IdFurnizor] in table 'Produse'
ALTER TABLE [dbo].[Produse]
ADD CONSTRAINT [FK_Produse_IdFurnizor]
    FOREIGN KEY ([IdFurnizor])
    REFERENCES [dbo].[Furnizori]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Produse_IdFurnizor'
CREATE INDEX [IX_FK_Produse_IdFurnizor]
ON [dbo].[Produse]
    ([IdFurnizor]);
GO

-- Creating foreign key on [IdMagazin] in table 'Inventar'
ALTER TABLE [dbo].[Inventar]
ADD CONSTRAINT [FK_Inventar_IdMagazin]
    FOREIGN KEY ([IdMagazin])
    REFERENCES [dbo].[Magazine]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Inventar_IdMagazin'
CREATE INDEX [IX_FK_Inventar_IdMagazin]
ON [dbo].[Inventar]
    ([IdMagazin]);
GO

-- Creating foreign key on [IdProdus] in table 'Inventar'
ALTER TABLE [dbo].[Inventar]
ADD CONSTRAINT [FK_Inventar_IdProdus]
    FOREIGN KEY ([IdProdus])
    REFERENCES [dbo].[Produse]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Inventar_IdProdus'
CREATE INDEX [IX_FK_Inventar_IdProdus]
ON [dbo].[Inventar]
    ([IdProdus]);
GO

-- Creating foreign key on [IdMagazin] in table 'Utilizatori'
ALTER TABLE [dbo].[Utilizatori]
ADD CONSTRAINT [FK_Utilizatori_IdMagazin]
    FOREIGN KEY ([IdMagazin])
    REFERENCES [dbo].[Magazine]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Utilizatori_IdMagazin'
CREATE INDEX [IX_FK_Utilizatori_IdMagazin]
ON [dbo].[Utilizatori]
    ([IdMagazin]);
GO

-- Creating foreign key on [IdProdus] in table 'Preturi'
ALTER TABLE [dbo].[Preturi]
ADD CONSTRAINT [FK_Preturi_IdProdus]
    FOREIGN KEY ([IdProdus])
    REFERENCES [dbo].[Produse]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Preturi_IdProdus'
CREATE INDEX [IX_FK_Preturi_IdProdus]
ON [dbo].[Preturi]
    ([IdProdus]);
GO

-- Creating foreign key on [IdProdus] in table 'RandComenziProduse'
ALTER TABLE [dbo].[RandComenziProduse]
ADD CONSTRAINT [FK_RandComenziProduse_IdProdus]
    FOREIGN KEY ([IdProdus])
    REFERENCES [dbo].[Produse]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RandComenziProduse_IdProdus'
CREATE INDEX [IX_FK_RandComenziProduse_IdProdus]
ON [dbo].[RandComenziProduse]
    ([IdProdus]);
GO

-- Creating foreign key on [IdRol] in table 'Utilizatori'
ALTER TABLE [dbo].[Utilizatori]
ADD CONSTRAINT [FK_Utilizatori_IdRol]
    FOREIGN KEY ([IdRol])
    REFERENCES [dbo].[Roluri]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Utilizatori_IdRol'
CREATE INDEX [IX_FK_Utilizatori_IdRol]
ON [dbo].[Utilizatori]
    ([IdRol]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------