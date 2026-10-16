USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'AsistenciaFacultad')
BEGIN
    ALTER DATABASE AsistenciaFacultad SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE AsistenciaFacultad;
END
GO

CREATE DATABASE AsistenciaFacultad;
GO
USE AsistenciaFacultad;
GO

-- Tabla: Docentes
CREATE TABLE [dbo].[Docentes](
    [Id]            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Codigo]        VARCHAR(20)   NOT NULL,
    [Nombre]        NVARCHAR(100) NOT NULL,
    [Apellido]      NVARCHAR(100) NOT NULL,
    [DNI]           NVARCHAR(20)  NULL,
    [Clase]         VARCHAR(50)   NULL,
    [Categoria]     VARCHAR(50)   NULL,
    [Departamento]  VARCHAR(100)  NULL,
    [Correo]        VARCHAR(100)  NULL
);

-- Tabla: Cursos
CREATE TABLE [dbo].[Cursos](
    [Id]              INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre]          VARCHAR(150)  NOT NULL,
    [Codigo]          VARCHAR(20)   NULL,
    [Creditos]        INT           NULL,
    [Tipo]            VARCHAR(5)    NULL,
    [Escuela]         VARCHAR(100)  NULL,
    [MallaCurricular] VARCHAR(20)   NULL
);

-- Tabla: Periodos
CREATE TABLE [dbo].[Periodos](
    [Id]       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre]   VARCHAR(50) NOT NULL UNIQUE,
    [EsActivo] BIT DEFAULT 0
);

-- Tabla: HorarioClases
CREATE TABLE [dbo].[HorarioClases](
    [Id]         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [DocenteId]  INT      NOT NULL,
    [CursoId]    INT      NOT NULL,
    [DiaSemana]  INT      NOT NULL,
    [HoraInicio] TIME(7)  NOT NULL,
    [HoraFin]    TIME(7)  NOT NULL,
    [PeriodoId]  INT      NULL,
    [TipoClase]  VARCHAR(20) DEFAULT 'TEORÍA' NOT NULL
);

-- Tabla: RegistroAsistencia
CREATE TABLE [dbo].[RegistroAsistencia](
    [Id]         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [DocenteId]  INT      NULL,
    [FechaHora]  DATETIME NULL,
    [HoraSalida] DATETIME NULL,
    [CursoId]    INT      NULL,
    [PeriodoId]  INT      NULL
);

-- Tabla: ConfiguracionSistema
CREATE TABLE [dbo].[ConfiguracionSistema] (
    [Clave]       NVARCHAR(100) PRIMARY KEY,
    [Valor]       INT           NULL,
    [Descripcion] NVARCHAR(255) NULL,
    [ValorTxt]    NVARCHAR(MAX) NULL
);

-- ======== UNIQUE ========
ALTER TABLE [dbo].[Docentes] ADD CONSTRAINT UQ_Docentes_Codigo UNIQUE ([Codigo]);
ALTER TABLE [dbo].[Cursos]   ADD CONSTRAINT UQ_Cursos_Nombre   UNIQUE ([Nombre]);

-- ======== CHECK ========
ALTER TABLE [dbo].[Docentes] ADD CONSTRAINT CK_Docentes_Codigo
    CHECK (LEN(Codigo) = 6 AND Codigo NOT LIKE '%[^A-Z0-9]%' COLLATE Latin1_General_BIN);

ALTER TABLE [dbo].[Docentes] ADD CONSTRAINT CK_Docentes_DNI
    CHECK (LEN(DNI) = 8 AND DNI NOT LIKE '%[^0-9]%');

ALTER TABLE [dbo].[Cursos] ADD CONSTRAINT CK_Cursos_Codigo
    CHECK (Codigo IS NULL OR (LEN(Codigo) <= 6 AND Codigo NOT LIKE '%[^A-Z0-9]%' COLLATE Latin1_General_BIN));

ALTER TABLE [dbo].[Cursos] ADD CONSTRAINT CK_Cursos_Nombre
    CHECK (Nombre = UPPER(Nombre) COLLATE SQL_Latin1_General_CP1_CS_AS);

ALTER TABLE [dbo].[Cursos] ADD CONSTRAINT CK_Cursos_Creditos
    CHECK (Creditos IS NULL OR Creditos IN (2, 3, 4, 5, 6));

-- ======== FOREIGN KEYS ========
ALTER TABLE [dbo].[RegistroAsistencia] ADD CONSTRAINT FK_Registro_Docente
    FOREIGN KEY ([DocenteId]) REFERENCES [dbo].[Docentes]([Id]);

ALTER TABLE [dbo].[RegistroAsistencia] ADD CONSTRAINT FK_Registro_Curso
    FOREIGN KEY ([CursoId]) REFERENCES [dbo].[Cursos]([Id]);

ALTER TABLE [dbo].[RegistroAsistencia] ADD CONSTRAINT FK_Registro_Periodo
    FOREIGN KEY ([PeriodoId]) REFERENCES [dbo].[Periodos]([Id]);

ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT FK_Horario_Docente
    FOREIGN KEY ([DocenteId]) REFERENCES [dbo].[Docentes]([Id]);

ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT FK_Horario_Curso
    FOREIGN KEY ([CursoId]) REFERENCES [dbo].[Cursos]([Id]);

ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT FK_Horario_Periodo
    FOREIGN KEY ([PeriodoId]) REFERENCES [dbo].[Periodos]([Id]);

ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT UQ_Horario_Unico
    UNIQUE ([DocenteId], [CursoId], [DiaSemana], [HoraInicio], [PeriodoId]);
