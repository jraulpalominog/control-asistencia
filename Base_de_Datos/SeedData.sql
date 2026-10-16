USE AsistenciaFacultad;
GO

-- 1. Periodo
INSERT INTO Periodos (Nombre, EsActivo) VALUES ('Semestre 2026-I', 1);

-- 2. Cursos
INSERT INTO Cursos (Nombre, Codigo, Creditos, Tipo, Escuela, MallaCurricular) VALUES
('CALCULO I', 'MAT101', 5, 'O', 'MATEMATICA', '2018'),
('CALCULO II', 'MAT102', 5, 'O', 'MATEMATICA', '2018'),
('CALCULO III', 'MAT103', 5, 'O', 'MATEMATICA', '2018'),
('ALGEBRA LINEAL', 'MAT201', 4, 'O', 'MATEMATICA', '2018'),
('ECUACIONES DIFERENCIALES', 'MAT301', 4, 'O', 'MATEMATICA', '2018'),
('MATEMATICA BASICA', 'MAT001', 4, 'O', 'MATEMATICA', '2018'),
('GEOMETRIA ANALITICA', 'MAT002', 4, 'O', 'MATEMATICA', '2018'),
('PROGRAMACION I', 'COMP10', 4, 'O', 'COMPUTACION', '2018'),
('PROGRAMACION II', 'COMP20', 4, 'O', 'COMPUTACION', '2018'),
('METODOS NUMERICOS', 'MAT401', 3, 'O', 'MATEMATICA', '2018');

-- 3. Docentes
INSERT INTO Docentes (Codigo, Nombre, Apellido, DNI, Clase, Categoria, Departamento, Correo) VALUES
('DOC001', 'JUAN', 'PEREZ', '12345678', 'PRINCIPAL', 'TC', 'MATEMATICA PURA', 'juan.perez@unmsm.edu.pe'),
('DOC002', 'MARIA', 'GOMEZ', '87654321', 'ASOCIADO', 'TP', 'MATEMATICA APLICADA', 'maria.gomez@unmsm.edu.pe'),
('DOC003', 'CARLOS', 'LOPEZ', '11223344', 'AUXILIAR', 'TC', 'COMPUTACION', 'carlos.lopez@unmsm.edu.pe'),
('DOC004', 'ANA', 'MARTINEZ', '44332211', 'PRINCIPAL', 'TP', 'ESTADISTICA', 'ana.martinez@unmsm.edu.pe'),
('DOC005', 'LUIS', 'FERNANDEZ', '55667788', 'ASOCIADO', 'TC', 'MATEMATICA PURA', 'luis.fernandez@unmsm.edu.pe');

-- 4. HorarioClases
-- 1: Lunes, 2: Martes, 3: Miercoles, 4: Jueves, 5: Viernes, 6: Sabado
-- Evitando horas de almuerzo (12:00 a 14:00)

-- Juan Perez (DOC001)
INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId) VALUES
(1, 1, 1, '08:00', '10:00', 1), -- Lunes: Calculo I
(1, 1, 3, '08:00', '10:00', 1), -- Mierc: Calculo I
(1, 3, 2, '14:00', '16:00', 1), -- Martes: Calculo III
(1, 3, 4, '14:00', '16:00', 1); -- Jueves: Calculo III

-- Maria Gomez (DOC002)
INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId) VALUES
(2, 4, 1, '10:00', '12:00', 1), -- Lunes
(2, 4, 3, '10:00', '12:00', 1); -- Mierc

-- Carlos Lopez (DOC003)
INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId) VALUES
(3, 8, 2, '18:00', '20:00', 1), -- Martes
(3, 8, 4, '18:00', '20:00', 1), -- Jueves
(3, 9, 5, '18:00', '22:00', 1); -- Viernes

-- Ana Martinez (DOC004)
INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId) VALUES
(4, 10, 6, '08:00', '11:00', 1); -- Sabado

-- Luis Fernandez (DOC005)
INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId) VALUES
(5, 5, 1, '14:00', '16:00', 1),
(5, 5, 3, '14:00', '16:00', 1);
