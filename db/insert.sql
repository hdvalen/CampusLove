INSERT INTO generos(nombre) VALUES
('Masculino'),
('Femenino'),
('No binario'),
('Prefiero no decirlo');

INSERT INTO carreras (nombre) VALUES
('Ingeniería de Sistemas'),
('Medicina'),
('Psicología'),
('Derecho'),
('Administración'),
('Diseño Gráfico'),
('Arquitectura'),
('Matemáticas'),
('Física'),
('Biología'),
('Química'),
('Filosofía'),
('Economía'),
('Educación'),
('Comunicación Social');

INSERT INTO intereses (nombre) VALUES
('Música'),
('Cine'),
('Lectura'),
('Deportes'),
('Tecnología'),
('Viajes'),
('Fotografía'),
('Videojuegos'),
('Cocina'),
('Arte'),
('Naturaleza'),
('Política'),
('Historia'),
('Idiomas'),
('Cómics');

INSERT INTO usuarios (nombre, Edad, id_genero, id_carrera, FrasePerfil, Login, Password, CreditosDisponibles) VALUES
('Laura Gómez', 24, 2, 3, 'Amante de los libros y el café.', 'laura24', 'pass123', 20),
('Carlos Pérez', 28, 1, 1, 'Siempre buscando algo nuevo que aprender.', 'carlos28', 'pass123', 15),
('Sofía Rodríguez', 22, 2, 2, 'La medicina es mi pasión.', 'sofiaR', 'pass123', 10),
('Daniel Ortega', 25, 1, 5, 'Emprendedor por naturaleza.', 'danielO', 'pass123', 18),
('Ana Torres', 27, 2, 6, 'Creativa y soñadora.', 'anaT', 'pass123', 12),
('Lucía Ramírez', 23, 2, 4, 'Fan del cine clásico.', 'luciaR', 'pass123', 10),
('Marcos Díaz', 30, 1, 8, 'Apasionado por los números.', 'marcosD', 'pass123', 14),
('Luis Herrera', 21, 1, 9, 'Explorador de lo desconocido.', 'luisH', 'pass123', 16),
('Carmen Salas', 26, 2, 7, 'Diseño es arte funcional.', 'carmenS', 'pass123', 19),
('Elena Núñez', 29, 2, 10, 'Amo las plantas y la ciencia.', 'elenaN', 'pass123', 11),
('Mateo Silva', 24, 1, 11, 'Químico de corazón.', 'mateoS', 'pass123', 13),
('Andrés Bravo', 22, 1, 12, 'Pensador empedernido.', 'andresB', 'pass123', 17),
('Isabel Castillo', 25, 2, 13, 'Curiosa del comportamiento humano.', 'isabelC', 'pass123', 15),
('Tomás Vargas', 31, 1, 14, 'Amante de la enseñanza.', 'tomasV', 'pass123', 9),
('Valeria Luna', 28, 2, 15, 'Comunicadora de vocación.', 'valeriaL', 'pass123', 8);

INSERT INTO usuarioIntereses (id_usuario, id_interes) VALUES
(1, 3), (1, 1),
(2, 4), (2, 5),
(3, 2), (3, 6),
(4, 5), (4, 10),
(5, 1), (5, 7),
(6, 2), (6, 3),
(7, 8), (7, 9),
(8, 11), (8, 5),
(9, 1), (9, 10),
(10, 11), (10, 3),
(11, 5), (11, 6),
(12, 13), (12, 3),
(13, 3), (13, 12),
(14, 4), (14, 14),
(15, 15), (15, 2);

INSERT INTO interacciones (id_UsuarioOrigen, id_UsuarioDestino, Tipo) VALUES
(1, 2, 'L'),
(1, 3, 'L'),
(2, 1, 'L'),
(3, 4, 'D'),
(4, 3, 'L'),
(5, 6, 'L'),
(6, 5, 'L'),
(7, 8, 'L'),
(8, 7, 'D'),
(9, 10, 'L'),
(10, 9, 'L'),
(11, 12, 'D'),
(12, 11, 'L'),
(13, 14, 'L'),
(14, 13, 'L');

INSERT INTO coincidencias (id_usuario1, id_usuario2) VALUES
(1, 2),
(5, 6),
(9, 10),
(13, 14);

