USE AsistenciaFacultad;
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Escuela' AND Object_ID = Object_ID(N'Cursos'))
BEGIN
    ALTER TABLE [dbo].[Cursos] ADD [Escuela] VARCHAR(100) NULL;
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'MallaCurricular' AND Object_ID = Object_ID(N'Cursos'))
BEGIN
    ALTER TABLE [dbo].[Cursos] ADD [MallaCurricular] VARCHAR(20) NULL;
END
GO
