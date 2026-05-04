BEGIN TRANSACTION;
CREATE TABLE [TB_ESTADIO] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(200) NULL,
    [Cidade] varchar(200) NULL,
    [Capacidade] int NOT NULL,
    CONSTRAINT [PK_TB_ESTADIO] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Capacidade', N'Cidade', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_ESTADIO]'))
    SET IDENTITY_INSERT [TB_ESTADIO] ON;
INSERT INTO [TB_ESTADIO] ([Id], [Capacidade], [Cidade], [Nome])
VALUES (1, 75000, 'Madrid', 'Santiago Bernabeu'),
(2, 80000, 'Barcelona', 'Camp Nou'),
(3, 72000, 'Rio de Janeiro', 'Maracanã '),
(4, 70000, 'São Paulo', 'Morumbi'),
(5, 60000, 'Belo Horizonte', 'Mineirão'),
(6, 63000, 'Fortaleza', 'Castelão'),
(7, 80000, 'Cidade do México', 'Estadio Asteca');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Capacidade', N'Cidade', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_ESTADIO]'))
    SET IDENTITY_INSERT [TB_ESTADIO] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260406113435_MigracaoEstadios', N'10.0.5');

COMMIT;
GO

