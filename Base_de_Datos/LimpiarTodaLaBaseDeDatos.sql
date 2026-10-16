USE AsistenciaFacultad;
GO

-- 1. Eliminar todo el historial de asistencias (se hace primero por la relación de llaves foráneas)
DELETE FROM RegistroAsistencia;

-- 2. Eliminar toda la lista de docentes
DELETE FROM Docentes;

-- 3. Eliminar toda la lista de cursos
DELETE FROM Cursos;

-- 4. Reiniciar los contadores (Identity) a 0 para que los próximos registros empiecen nuevamente desde el ID 1
DBCC CHECKIDENT ('RegistroAsistencia', RESEED, 0);
DBCC CHECKIDENT ('Docentes', RESEED, 0);
DBCC CHECKIDENT ('Cursos', RESEED, 0);

PRINT '===============================================================';
PRINT '¡Limpieza completada!';
PRINT 'Se han eliminado todas las asistencias, docentes y cursos.';
PRINT 'La base de datos está totalmente en blanco y lista para usarse.';
PRINT '===============================================================';
GO
