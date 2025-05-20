CREATE DATABASE IF NOT EXISTS campusLove;
USE campusLove;

CREATE TABLE IF NOT EXISTS generos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE  IF NOT EXISTS carreras (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE   IF NOT EXISTS intereses (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE  IF NOT EXISTS usuarios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    Edad INT NOT NULL,
    id_genero INT NOT NULL,
    id_carrera INT NOT NULL,
    FrasePerfil TEXT,
    Login VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(70) NOT NULL,
    CreditosDisponibles INT DEFAULT 10,
    CONSTRAINT id_genero_fk FOREIGN KEY (id_genero) REFERENCES generos(id),
    CONSTRAINT id_carrera_fk FOREIGN KEY (id_carrera) REFERENCES carreras(id)
);

CREATE TABLE   IF NOT EXISTS usuarioIntereses (
    id_usuario INT,
    id_interes INT,
    PRIMARY KEY (id_usuario, id_interes),
    CONSTRAINT id_usuario_fk FOREIGN KEY (id_usuario) REFERENCES usuarios(id),
    CONSTRAINT id_interes_fk FOREIGN KEY (id_interes) REFERENCES intereses(id)
);

CREATE TABLE  IF NOT EXISTS interacciones (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_UsuarioOrigen INT NOT NULL,
    id_UsuarioDestino INT NOT NULL,
    Tipo CHAR(1) CHECK (Tipo IN ('L', 'D')),
    Fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_UsuarioOrigen_fk FOREIGN KEY (id_UsuarioOrigen) REFERENCES usuarios(id),
    CONSTRAINT id_UsuarioDestino_fk FOREIGN KEY (id_UsuarioDestino) REFERENCES usuarios(id)
);

CREATE TABLE  IF NOT EXISTS coincidencias (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario1 INT NOT NULL,
    id_usuario2 INT NOT NULL,
    FechaMatch DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_usuario1_fk FOREIGN KEY (id_usuario1) REFERENCES usuarios(id),
    CONSTRAINT id_usuario2_fk FOREIGN KEY (id_usuario2) REFERENCES usuarios(id)
);
CREATE TABLE IF NOT EXISTS likes (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario1 INT NOT NULL,
    id_usuario2 INT NOT NULL,
    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT likes_usuario1_fk FOREIGN KEY (id_usuario1) REFERENCES usuarios(id),
    CONSTRAINT likes_usuario2_fk FOREIGN KEY (id_usuario2) REFERENCES usuarios(id)
);
CREATE TABLE IF NOT EXISTS administradores (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(70) NOT NULL,
    usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(70) NOT NULL,
    correo VARCHAR(70) NOT NULL,
    nivel_acceso INT NOT NULL DEFAULT 1,
    fecha_creacion DATETIME NOT NULL,
    ultimo_acceso DATETIME NULL,
    activo BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS likes (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario1 INT NOT NULL,
    id_usuario2 INT NOT NULL,
    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT likes_usuario1_fk FOREIGN KEY (id_usuario1) REFERENCES usuarios(id),
    CONSTRAINT likes_usuario2_fk FOREIGN KEY (id_usuario2) REFERENCES usuarios(id)
);

INSERT INTO administradores (nombre, usuario, contrasena, correo, nivel_acceso, fecha_creacion, activo)
VALUES ('Administrador', 'admin', 'admin123', 'admin@campuslove.com', 3, NOW(), TRUE);
