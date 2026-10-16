USE AsistenciaFacultad;
GO

DELETE FROM RegistroAsistencia;
DELETE FROM Docentes;
DELETE FROM Cursos;
DBCC CHECKIDENT ('RegistroAsistencia', RESEED, 0);
DBCC CHECKIDENT ('Docentes', RESEED, 0);
DBCC CHECKIDENT ('Cursos', RESEED, 0);
GO

INSERT INTO Docentes (Codigo, Nombre, Apellido, DNI, Clase, Categoria, Departamento, Correo) VALUES 
(N'DOC001', N'ALAN', N'TURING', N'12345678', N'TP 20 hrs.', N'Principal', N'Computación Científica', N'alan.turing@unmsm.edu.pe'),
(N'DOC002', N'ADA', N'LOVELACE', N'87654321', N'TP 20 hrs.', N'Principal', N'Computación Científica', N'ada.lovelace@unmsm.edu.pe'),
(N'DOC003', N'MARIE', N'CURIE', N'11223344', N'TP 20 hrs.', N'Asociado', N'Matemática', N'marie.curie@unmsm.edu.pe'),
(N'DOC004', N'ALBERT', N'EINSTEIN', N'44332211', N'TP 40 hrs.', N'Principal', N'Matemática', N'albert.einstein@unmsm.edu.pe'),
(N'DOC005', N'ISAAC', N'NEWTON', N'55667788', N'TP 40 hrs.', N'Asociado', N'Matemática', N'isaac.newton@unmsm.edu.pe'),
(N'DOC006', N'NIKOLA', N'TESLA', N'88776655', N'TP 20 hrs.', N'Principal', N'Investigación Operativa', N'nikola.tesla@unmsm.edu.pe'),
(N'DOC007', N'GRACE', N'HOPPER', N'99887766', N'TP 40 hrs.', N'Asociado', N'Computación Científica', N'grace.hopper@unmsm.edu.pe'),
(N'DOC008', N'STEPHEN', N'HAWKING', N'66778899', N'TP 40 hrs.', N'Principal', N'Estadística', N'stephen.hawking@unmsm.edu.pe'),
(N'DOC009', N'RICHARD', N'FEYNMAN', N'22334455', N'TP 20 hrs.', N'Asociado', N'Estadística', N'richard.feynman@unmsm.edu.pe'),
(N'DOC010', N'CARL', N'SAGAN', N'55443322', N'TP 20 hrs.', N'Principal', N'Investigación Operativa', N'carl.sagan@unmsm.edu.pe');
GO

INSERT INTO Cursos (Nombre, Codigo, Creditos, Tipo) VALUES 
(N'CÁLCULO I', N'MAT101', 4, N'O'),
(N'CÁLCULO II', N'MAT102', 4, N'O'),
(N'FÍSICA I', N'FIS101', 4, N'O'),
(N'PROGRAMACIÓN BÁSICA', N'INF101', 5, N'O'),
(N'BASE DE DATOS', N'INF201', 5, N'O'),
(N'INTELIGENCIA ARTIFICIAL', N'INF301', 6, N'E'),
(N'ESTRUCTURAS DE DATOS', N'INF102', 5, N'O'),
(N'SISTEMAS OPERATIVOS', N'INF202', 4, N'O'),
(N'REDES DE COMPUTADORAS', N'INF302', 4, N'E'),
(N'COMPUTACIÓN CUÁNTICA', N'INF401', 6, N'E');
GO
