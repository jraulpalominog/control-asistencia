-- =====================================================
-- MIGRACIÓN: Tabla HorarioClases
-- Ejecutar sobre la base de datos AsistenciaFacultad
-- =====================================================
USE AsistenciaFacultad;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HorarioClases')
BEGIN
    CREATE TABLE [dbo].[HorarioClases](
        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [DocenteId] [int] NOT NULL,
        [CursoId] [int] NOT NULL,
        [DiaSemana] [int] NOT NULL,           -- 1=Lunes, 2=Martes, 3=Miércoles, 4=Jueves, 5=Viernes, 6=Sábado
        [HoraInicio] [time](7) NOT NULL,      -- Ej: '08:00:00'
        [HoraFin] [time](7) NOT NULL,         -- Ej: '10:00:00'
        [PeriodoId] [int] NULL,
        CONSTRAINT FK_Horario_Docente FOREIGN KEY ([DocenteId]) REFERENCES [dbo].[Docentes]([Id]),
        CONSTRAINT FK_Horario_Curso FOREIGN KEY ([CursoId]) REFERENCES [dbo].[Cursos]([Id]),
        CONSTRAINT FK_Horario_Periodo FOREIGN KEY ([PeriodoId]) REFERENCES [dbo].[Periodos]([Id]),
        CONSTRAINT UQ_Horario_Unico UNIQUE ([DocenteId], [CursoId], [DiaSemana], [HoraInicio], [PeriodoId])
    );

    PRINT 'Tabla HorarioClases creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla HorarioClases ya existe.';
END
GO

-- =====================================================
-- DATOS DE EJEMPLO (Opcional)
-- =====================================================
-- Ejemplo: DOC001 (ALAN TURING) dicta CÁLCULO I los Lunes de 08:00 a 10:00
--          y BASE DE DATOS los Lunes de 14:00 a 16:00
-- INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId)
-- VALUES (1, 1, 1, '08:00', '10:00', (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1));
-- INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId)
-- VALUES (1, 5, 1, '14:00', '16:00', (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1));
