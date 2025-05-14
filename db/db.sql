CREATE DATABASE campusLove;
USE campusLove;

CREATE TABLE IF NOT EXISTS generos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE  IF NOT EXISTS carreras (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE   IF NOT EXISTS intereses (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE  IF NOT EXISTS usuarios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    id_genero INT NOT NULL,
    id_carrera INT NOT NULL,
    FrasePerfil TEXT,
    CreditosDisponibles INT DEFAULT 10,
    CONSTRAINT id_genero_fk FOREIGN KEY (id_genero) REFERENCES generos(id),
    CONSTRAINT id_carrera_fk FOREIGN KEY (id_carrera) REFERENCES carreras(id)
);

CREATE TABLE   IF NOT EXISTS usuarioIntereses (
    id_usuario INT,
    id_interes INT,
    PRIMARY KEY (id_usuario, id_interes),
    CONSTRAINT id_usuario_fk FOREIGN KEY (id_usuario) REFERENCES usuarios(id),
    CONSTRAINT id_interes_fk FOREIGN KEY (id_interes) REFERENCES Intereses(id)
);

CREATE TABLE  IF NOT EXISTS interacciones (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_deUsuario INT NOT NULL,
    id_paraUsuario INT NOT NULL,
    Tipo CHAR(1) CHECK (Tipo IN ('L', 'D')),
    Fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_deUsuario_fk FOREIGN KEY (id_deUsuario) REFERENCES Usuarios(id),
    CONSTRAINT id_paraUsuario_fk FOREIGN KEY (id_paraUsuario) REFERENCES Usuarios(id)
);

CREATE TABLE  IF NOT EXISTS coincidencias (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario1 INT NOT NULL,
    id_usuario2 INT NOT NULL,
    FechaCoincidencia DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_usuario1_fk FOREIGN KEY (id_usuario1) REFERENCES Usuarios(id),
    CONSTRAINT id_usuario2_fk FOREIGN KEY (id_usuario2) REFERENCES Usuarios(id)
);

