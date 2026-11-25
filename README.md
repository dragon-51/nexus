# NEXUS — Sistema de Exploración y Supervivencia 2D

## Descripción del proyecto

NEXUS Training System es un videojuego de exploración, acción y supervivencia en 2D, desarrollado en C# mediante Windows Forms y SQL Server.

El jugador asume el papel de un cadete que debe explorar mundos desconocidos, enfrentarse a enemigos, administrar sus recursos y completar misiones dentro de un entorno de ciencia ficción.

El proyecto evolucionó desde una aplicación de consola hasta una aplicación gráfica con generación de mundos y persistencia de datos.

## Objetivo del proyecto

Desarrollar un videojuego interactivo que integre programación orientada a objetos, interfaces gráficas, generación de escenarios, sistemas de combate y almacenamiento de información mediante una base de datos relacional.

## Funcionalidades principales

- Menú principal mediante Windows Forms.
- Generación de mundos mediante semillas.
- Exploración de escenarios y plataformas en 2D.
- Movimiento horizontal y doble salto.
- Sistema de combate mediante disparos.
- Generación de enemigos.
- Administración de salud, energía y estabilidad.
- Sistema de experiencia.
- Gestión del inventario.
- Recuperación de energía.
- Sistema de misiones y recompensas.
- Guardado y recuperación de partidas mediante SQL Server.
- Selección de partidas guardadas mediante DataGridView.
- Persistencia del estado y posición de los enemigos.

## Controles del juego

| Tecla | Acción |
|---|---|
| A | Moverse hacia la izquierda |
| D | Moverse hacia la derecha |
| Espacio | Saltar y realizar doble salto |
| J | Atacar mediante disparos |
| R | Recargar energía |

## Sistema de misiones

### La anomalía perdida

El objetivo del jugador consiste en eliminar a los enemigos presentes en el mundo y alcanzar el final del escenario.

Al completar la misión, el jugador recibe una recompensa de experiencia.

El estado de la misión se almacena en SQL Server, permitiendo continuar el progreso después de cerrar el videojuego y evitando otorgar nuevamente la recompensa de una misión completada.

## Tecnologías utilizadas

| Tecnología | Aplicación |
|---|---|
| C# | Lenguaje de programación |
| .NET 8 | Plataforma de desarrollo |
| Windows Forms | Interfaz gráfica |
| SQL Server | Persistencia de datos |
| Microsoft.Data.SqlClient | Conexión con la base de datos |
| Visual Studio | Desarrollo y depuración |
| SSMS | Administración de la base de datos |
| Git | Control de versiones |
| GitHub | Repositorio del proyecto |
| JSON | Serialización de información |

## Arquitectura del sistema

El proyecto organiza sus responsabilidades en tres componentes principales.

### Interfaz gráfica

Responsable de la interacción con el usuario, la presentación de menús, la visualización del mundo y la selección de partidas guardadas.

### Lógica del videojuego

Administra el comportamiento del jugador, los enemigos, el combate, las plataformas, las estadísticas y el cumplimiento de las misiones.

### Acceso a datos

Gestiona la comunicación entre el videojuego y SQL Server, permitiendo almacenar y recuperar el progreso del jugador.

Flujo general:

**Usuario → Windows Forms → Lógica del videojuego → Acceso a datos → SQL Server**

## Estructura del proyecto

| Archivo o carpeta | Descripción |
|---|---|
| Program.cs | Punto de entrada y clases principales |
| menu_principal.cs | Menú principal del videojuego |
| juego2d.cs | Lógica y funcionamiento del mundo 2D |
| enemigo2d.cs | Representación de enemigos en el escenario |
| partidasSQL.cs | Conexión, guardado y recuperación de datos |
| cargarpartidas.cs | Interfaz de selección de partidas |
| ConsoleApp9.csproj | Configuración y dependencias |
| ConsoleApp9.slnx | Solución del proyecto |
| sprites/ | Recursos gráficos |
| Sounds/ | Recursos de audio |
| sql/nexus.sql | Script de creación de la base de datos |
| docs/ | Documentación técnica |
| README.md | Información general del proyecto |

## Base de datos

NEXUS utiliza una base de datos relacional denominada NEXUS, administrada mediante SQL Server.

### Tablas principales

| Tabla | Clave primaria | Función |
|---|---|---|
| Partidas | Id | Almacena el progreso del jugador |
| Inventario | IdObjeto | Registra los objetos y sus cantidades |
| Enemigos | IdEnemigo | Almacena la posición y salud de los enemigos |
| Misiones | IdMision | Registra el estado de las misiones |

### Relaciones

La tabla Partidas se relaciona con Inventario, Enemigos y Misiones mediante la clave foránea IdPartida.

Las relaciones son de uno a muchos (1:N):

- Partidas 1:N Inventario.
- Partidas 1:N Enemigos.
- Partidas 1:N Misiones.

Estas relaciones permiten mantener la integridad referencial y asociar correctamente la información con cada partida.

### Información persistente

El sistema permite almacenar:

- Nombre y edad del cadete.
- Salud y salud máxima.
- Energía y estabilidad.
- Experiencia.
- Posición del jugador.
- Nombre y semilla del mundo.
- Objetos del inventario.
- Salud y posición de los enemigos.
- Estado de la misión.
- Fecha de guardado.

El procedimiento de guardado utiliza transacciones SQL para mantener la consistencia de los datos. Si ocurre un error durante el proceso, se revierten las operaciones de la transacción.

## Requisitos de instalación

- Sistema operativo Windows.
- SDK de .NET 8 o compatible.
- Visual Studio con desarrollo de escritorio de .NET.
- SQL Server Express.
- SQL Server Management Studio.
- Git.

## Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/dragon-51/nexus.git
```

### 2. Acceder a la carpeta

```bash
cd nexus
```

### 3. Configurar la base de datos

1. Abrir SQL Server Management Studio.
2. Conectarse a la instancia local de SQL Server.
3. Abrir el archivo sql/nexus.sql.
4. Ejecutar el script en una instalación nueva.
5. Verificar la creación de la base de datos NEXUS y sus cuatro tablas.

El script crea la base de datos si todavía no existe. No debe ejecutarse nuevamente sin revisar las tablas existentes.

### 4. Configurar la conexión

La cadena de conexión utilizada por defecto es:

```csharp
Server=.\SQLEXPRESS;Database=NEXUS;Integrated Security=True;TrustServerCertificate=True;
```

Si la instancia de SQL Server tiene un nombre diferente, se debe modificar la cadena de conexión en partidasSQL.cs.

### 5. Restaurar las dependencias

```bash
dotnet restore ConsoleApp9.csproj
```

### 6. Compilar el proyecto

```bash
dotnet build ConsoleApp9.csproj
```

### 7. Ejecutar el videojuego

```bash
dotnet run --project ConsoleApp9.csproj
```

También es posible abrir ConsoleApp9.slnx desde Visual Studio y ejecutar el proyecto mediante F5.

## Conceptos de programación aplicados

- Programación orientada a objetos.
- Clases y objetos.
- Constructores.
- Atributos y propiedades.
- Encapsulamiento.
- Herencia.
- Polimorfismo.
- Abstracción.
- Eventos de Windows Forms.
- Validación de datos.
- Manejo de excepciones.
- Consultas SQL parametrizadas.
- Claves primarias y foráneas.
- Transacciones SQL.
- Persistencia de datos.
- Control de versiones.

## Pruebas funcionales realizadas

- Generación de mundos.
- Movimiento y doble salto.
- Combate contra enemigos.
- Guardado de estadísticas del jugador.
- Almacenamiento del inventario.
- Almacenamiento del estado de los enemigos.
- Recuperación de partidas guardadas.
- Conservación de la experiencia.
- Guardado y recuperación del estado de la misión.
- Verificación de recompensas de misión.

## Evolución del proyecto

### Primera etapa

- Aplicación de consola.
- Exploración mediante matrices.
- Creación de personajes.
- Inventario y combate básico.

### Segunda etapa

- Implementación de Windows Forms.
- Interfaz gráfica.
- Generación de mundos.
- Plataformas y combate en 2D.

### Etapa actual

- Integración con SQL Server.
- Guardado y recuperación de partidas.
- Modelo de datos relacional.
- Persistencia de enemigos e inventario.
- Sistema de misiones y recompensas.
- Script SQL para la instalación de la base de datos.

## Mejoras futuras

- Incorporación de nuevos enemigos.
- Ampliación de mundos y escenarios.
- Implementación de nuevas misiones.
- Incorporación de jefes finales.
- Mejoras gráficas y animaciones.
- Ampliación del sistema de combate.
- Implementación de pruebas automatizadas.

## Autor

**Andrés Martínez Chura**

GitHub: [dragon-51](https://github.com/dragon-51)

Repositorio: [NEXUS](https://github.com/dragon-51/nexus)

## Estado del proyecto

En desarrollo activo, con sistema de exploración 2D, combate, misiones y persistencia mediante SQL Server.
