ALTER TABLE RegistroAsistencia ADD CursoId INT NULL;
GO
UPDATE r SET r.CursoId = c.Id FROM RegistroAsistencia r INNER JOIN Cursos c ON r.Curso = c.Nombre;
GO
ALTER TABLE RegistroAsistencia DROP CONSTRAINT FK_Registro_Curso;
GO
ALTER TABLE RegistroAsistencia DROP COLUMN Curso;
GO
ALTER TABLE RegistroAsistencia ADD CONSTRAINT FK_Registro_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(Id);
GO
