
##💖 C A M P U S L O V E 💖

CampusLove es una app pensada para Conectar con personas de campus, descubre intereses en común y crea conexiones reales de forma divertida. 💘🎓

## ✨ Características Principales

- **Perfiles de Usuario**: Crea y personaliza tu perfil con información académica y personal
- **Sistema de Likes**: Interactúa con otros usuarios mediante likes, dislikes y superlikes
- **Matches**: Conecta con otros usuarios cuando ambos muestran interés mutuo
- **Algoritmo de Compatibilidad**: Encuentra coincidencias basadas en intereses, carrera y preferencias
- **Rankings**: Descubre los perfiles más populares del campus
- **Interfaz Amigable**: Diseño intuitivo y atractivo para una experiencia de usuario óptima
- **Privacidad**: Control total sobre quién puede ver tu perfil y tus datos


## 🏗️ Arquitectura

CampusLove está construido siguiendo una arquitectura hexagonal (puertos y adaptadores) que separa claramente las responsabilidades:

- **Dominio**: Contiene las entidades de negocio y reglas de dominio
- **Puertos**: Interfaces que definen los contratos para interactuar con el dominio
- **Adaptadores**: Implementaciones concretas de los puertos (repositorios)
- **Aplicación**: Orquesta los casos de uso conectando la UI con el dominio
- **UI**: Interfaz de consola para interactuar con el sistema


## 🛠️ Tecnologías Utilizadas

- **Lenguaje**: C# 8.0
- **Framework**: .NET 
- **Base de Datos**: MySQL 9.3
- **ORM**: ADO.NET (acceso directo a datos)
- **Control de Versiones**: Git
- **Herramientas de Desarrollo**: Visual Studio 


## 📊 Estructura de la Base de Datos
## 📱 Funcionalidades

### Sistema de Usuarios

El sistema de usuarios permite:

- Registro de nuevos usuarios con información académica y personal
- Edición de perfiles
- Búsqueda de usuarios por diferentes criterios
- Visualización de perfiles


### Sistema de Interacciones

Las interacciones son la base de la comunicación entre usuarios:

- Like (L): Muestra interés en otro usuario
- Dislike (D): Indica falta de interés
- SuperLike (S): Muestra un interés especial


Cada interacción se registra con:

- Fecha y hora
- Tipo de interacción
- Descripción opcional
- Usuario origen y destino


### Sistema de Matches

Los matches ocurren cuando dos usuarios muestran interés mutuo:

- Se crean automáticamente cuando hay likes recíprocos
- Tienen diferentes estados (Activo, Inactivo, Bloqueado)
- Permiten la comunicación entre usuarios


### Sistema de Coincidencias

El algoritmo de coincidencias sugiere posibles matches basados en:

- Carrera universitaria
- Intereses comunes
- Edad y preferencias
- Ubicación en el campus


Cada coincidencia tiene un porcentaje de compatibilidad calculado.

### Rankings y Estadísticas

El sistema ofrece diferentes rankings:

- Usuarios con más likes
- Usuarios con más matches
- Usuarios más populares del mes
- Estadísticas generales de uso


### Estructura del Proyecto

CampusLove/
├── CampusLove.Domain/           # Entidades y reglas de negocio
│   ├── Entities/                # Clases de entidades
│   └── Ports/                   # Interfaces (puertos)
├── CampusLove.Infrastructure/   # Implementaciones concretas
│   ├── Repositories/            # Repositorios para acceso a datos
│   └── Services/                # Servicios de infraestructura
├── CampusLove.Application/      # Lógica de aplicación
│   ├── Services/                # Servicios de aplicación
│   └── DTOs/                    # Objetos de transferencia de datos
├── CampusLove.UI.Console/       # Interfaz de usuario de consola
│   ├── Menus/                   # Clases de menús
│   └── Program.cs               # Punto de entrada
├── CampusLove.Tests/            # Pruebas unitarias e integración
├── database/                    # Scripts de base de datos
│   ├── schema.sql               # Estructura de la BD
│   └── seed.sql                 # Datos de prueba
└── README.md                    # Este archivo



## 🔄 Flujo de Trabajo

1. **Registro de Usuario**: El usuario se registra proporcionando información básica
2. **Configuración de Perfil**: Completa su perfil con detalles académicos e intereses
3. **Exploración**: Navega por perfiles sugeridos basados en compatibilidad
4. **Interacción**: Realiza likes, dislikes o superlikes a otros perfiles
5. **Matches**: Recibe notificaciones cuando hay un match (interés mutuo)
6. **Comunicación**: Puede iniciar conversaciones con sus matches
7. **Rankings**: Visualiza los rankings y estadísticas de la plataforma


