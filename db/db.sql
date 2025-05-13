CREATE DATABASE campusLove;
USE campusLove;

CREATE TABLE IF NOT EXISTS carreras{
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50)
};
CREATE TABLE IF NOT EXISTS genero{
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50)
};
CREATE TABLE IF NOT EXISTS usuario{
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50),
    edad INT,
    frasePerfil VARCHAR(80),
    id_genero INT,
    id_carrera INT,
    CONSTRAINT id_genero_fk FOREIGN KEY id_genero REFERENCES genero(id),
    CONSTRAINT id_carrera_fk FOREIGN KEY id_carrera REFERENCES carrera(id)
};
CREATE TABLE IF NOT EXISTS interes{
    id INT PRIMARY KEY AUTO_INCREMENT,
    
}
