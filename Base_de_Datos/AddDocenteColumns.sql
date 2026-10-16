-- Script para agregar nuevas columnas a la tabla de Docentes
-- Ejecutar en la base de datos AsistenciaFacultad

ALTER TABLE Docentes
ADD 
    Clase VARCHAR(50) NULL,
    Categoria VARCHAR(50) NULL,
    Departamento VARCHAR(100) NULL,
    Correo VARCHAR(100) NULL;
GO
