CREATE DATABASE AsistenciaFacultad;
GO
USE AsistenciaFacultad;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Docentes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Nombre] [nvarchar](100) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Apellido] [nvarchar](100) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[DNI] [nvarchar](20) COLLATE Modern_Spanish_CI_AS NULL,
	[Clase] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[Categoria] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[Departamento] [varchar](100) COLLATE Modern_Spanish_CI_AS NULL,
	[Correo] [varchar](100) COLLATE Modern_Spanish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Cursos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](150) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Codigo] [varchar](20) COLLATE Modern_Spanish_CI_AS NULL,
	[Creditos] [int] NULL,
	[Tipo] [varchar](5) COLLATE Modern_Spanish_CI_AS NULL,
	[Escuela] [varchar](100) COLLATE Modern_Spanish_CI_AS NULL,
	[MallaCurricular] [varchar](20) COLLATE Modern_Spanish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Periodos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL UNIQUE,
	[EsActivo] [bit] DEFAULT 0,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[RegistroAsistencia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocenteId] [int] NULL,
	[FechaHora] [datetime] NULL,
	[HoraSalida] [datetime] NULL,
	[CursoId] [int] NULL,
	[PeriodoId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Docentes] ON 

INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (1, N'DOC001', N'ALAN', N'TURING', N'12345678', N'TP 20 hrs.', N'Principal', N'Computación Científica', N'alan.turing@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (2, N'DOC002', N'ADA', N'LOVELACE', N'87654321', N'TP 20 hrs.', N'Principal', N'Computación Científica', N'ada.lovelace@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (3, N'DOC003', N'MARIE', N'CURIE', N'11223344', N'TP 20 hrs.', N'Asociado', N'Matemática', N'marie.curie@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (4, N'DOC004', N'ALBERT', N'EINSTEIN', N'44332211', N'TP 40 hrs.', N'Principal', N'Matemática', N'albert.einstein@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (5, N'DOC005', N'ISAAC', N'NEWTON', N'55667788', N'TP 40 hrs.', N'Asociado', N'Matemática', N'isaac.newton@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (6, N'DOC006', N'NIKOLA', N'TESLA', N'88776655', N'TP 20 hrs.', N'Principal', N'Investigación Operativa', N'nikola.tesla@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (7, N'DOC007', N'GRACE', N'HOPPER', N'99887766', N'TP 40 hrs.', N'Asociado', N'Computación Científica', N'grace.hopper@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (8, N'DOC008', N'STEPHEN', N'HAWKING', N'66778899', N'TP 40 hrs.', N'Principal', N'Estadística', N'stephen.hawking@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (9, N'DOC009', N'RICHARD', N'FEYNMAN', N'22334455', N'TP 20 hrs.', N'Asociado', N'Estadística', N'richard.feynman@unmsm.edu.pe')
INSERT [dbo].[Docentes] ([Id], [Codigo], [Nombre], [Apellido], [DNI], [Clase], [Categoria], [Departamento], [Correo]) VALUES (10, N'DOC010', N'CARL', N'SAGAN', N'55443322', N'TP 20 hrs.', N'Principal', N'Investigación Operativa', N'carl.sagan@unmsm.edu.pe')
SET IDENTITY_INSERT [dbo].[Docentes] OFF

SET IDENTITY_INSERT [dbo].[Cursos] ON 
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (1, N'CÁLCULO I', N'MAT101', 4, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (2, N'CÁLCULO II', N'MAT102', 4, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (3, N'FÍSICA I', N'FIS101', 4, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (4, N'PROGRAMACIÓN BÁSICA', N'INF101', 5, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (5, N'BASE DE DATOS', N'INF201', 5, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (6, N'INTELIGENCIA ARTIFICIAL', N'INF301', 6, N'E')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (7, N'ESTRUCTURAS DE DATOS', N'INF102', 5, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (8, N'SISTEMAS OPERATIVOS', N'INF202', 4, N'O')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (9, N'REDES DE COMPUTADORAS', N'INF302', 4, N'E')
INSERT [dbo].[Cursos] ([Id], [Nombre], [Codigo], [Creditos], [Tipo]) VALUES (10, N'COMPUTACIÓN CUÁNTICA', N'INF401', 6, N'E')
SET IDENTITY_INSERT [dbo].[Cursos] OFF
SET ANSI_PADDING ON

ALTER TABLE [dbo].[Docentes] ADD UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON

ALTER TABLE [dbo].[Cursos] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

ALTER TABLE [dbo].[Docentes] ADD CONSTRAINT CK_Docentes_Codigo CHECK (LEN(Codigo) = 6 AND Codigo NOT LIKE '%[^A-Z0-9]%' COLLATE Latin1_General_BIN);
ALTER TABLE [dbo].[Docentes] ADD CONSTRAINT CK_Docentes_DNI CHECK (LEN(DNI) = 8 AND DNI NOT LIKE '%[^0-9]%');

ALTER TABLE [dbo].[Cursos] ADD CONSTRAINT CK_Cursos_Codigo CHECK (Codigo IS NULL OR (LEN(Codigo) <= 6 AND Codigo NOT LIKE '%[^A-Z0-9]%' COLLATE Latin1_General_BIN));
ALTER TABLE [dbo].[Cursos] ADD CONSTRAINT CK_Cursos_Nombre CHECK (Nombre = UPPER(Nombre) COLLATE SQL_Latin1_General_CP1_CS_AS);
ALTER TABLE [dbo].[Cursos] ADD CONSTRAINT CK_Cursos_Creditos CHECK (Creditos IS NULL OR Creditos IN (2, 3, 4, 5, 6));
ALTER TABLE [dbo].[RegistroAsistencia] ADD CONSTRAINT FK_Registro_Docente FOREIGN KEY ([DocenteId]) REFERENCES [dbo].[Docentes]([Id]);
ALTER TABLE [dbo].[RegistroAsistencia] ADD CONSTRAINT FK_Registro_Curso FOREIGN KEY ([CursoId]) REFERENCES [dbo].[Cursos]([Id]);
ALTER TABLE [dbo].[RegistroAsistencia] ADD CONSTRAINT FK_Registro_Periodo FOREIGN KEY ([PeriodoId]) REFERENCES [dbo].[Periodos]([Id]);

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[HorarioClases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocenteId] [int] NOT NULL,
	[CursoId] [int] NOT NULL,
	[DiaSemana] [int] NOT NULL,
	[HoraInicio] [time](7) NOT NULL,
	[HoraFin] [time](7) NOT NULL,
	[PeriodoId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT FK_Horario_Docente FOREIGN KEY ([DocenteId]) REFERENCES [dbo].[Docentes]([Id]);
ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT FK_Horario_Curso FOREIGN KEY ([CursoId]) REFERENCES [dbo].[Cursos]([Id]);
ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT FK_Horario_Periodo FOREIGN KEY ([PeriodoId]) REFERENCES [dbo].[Periodos]([Id]);
ALTER TABLE [dbo].[HorarioClases] ADD CONSTRAINT UQ_Horario_Unico UNIQUE ([DocenteId], [CursoId], [DiaSemana], [HoraInicio], [PeriodoId]);
