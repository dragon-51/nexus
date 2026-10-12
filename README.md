 NEXUS V1.0 — Sistema de Exploración y Supervivencia

**NEXUs Training System** 
es una aplicación interactiva de consola desarrollada en C# y .NET. Simula una misión de exploración en un entorno de ciencia ficción donde el jugador debe administrar sus recursos, investigar anomalías y sobrevivir a encuentros hostiles.

 **Objetivo del proyecto**

Explorar un mapa desconocido, localizar el núcleo energético perdido y completar la misión antes de que los indicadores vitales del cadete alcancen niveles críticos.

 **Funcionalidades principales**

- **Exploración del mapa:** desplazamiento por el entorno mediante controles de teclado.
- **Sistema de supervivencia:** seguimiento de salud, energía y estabilidad del cadete.
- **Inventario:** recolección, consulta, utilización y eliminación de objetos.
- **Sistema de combate:** enfrentamientos contra enemigos con ataques y daños variables.
- **Encuentros aleatorios:** posibilidad de encontrar enemigos, objetos o zonas con radiación.
- **Gestión de energía:** utilización de células de poder para recuperar energía.
- **Sistema de misiones:** búsqueda del núcleo energético y extracción segura.
- **Interfaz de consola:** panel de estadísticas, mensajes de estado y menús interactivos.

## 🧭 Controles

Durante la exploración del mapa:

| Tecla | Acción |
|---|---|
| `W` | Moverse hacia arriba |
| `A` | Moverse hacia la izquierda |
| `S` | Moverse hacia abajo |
| `D` | Moverse hacia la derecha |
| `Q` | Regresar al menú principal |

Durante el combate, se utilizan las opciones numéricas mostradas en pantalla.

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET 10
- **Entorno de desarrollo:** Visual Studio
- **Control de versiones:** Git
- **Repositorio:** GitHub

##  Estructura del proyecto

- `Program.cs`: contiene el punto de entrada, las clases y la lógica principal de la simulación.
- `ConsoleApp9.csproj`: define la configuración del proyecto y su plataforma de destino.
- `Properties/`: contiene los archivos de configuración del proyecto.
- `Dockerfile`: configuración para construir una imagen de contenedor.

##  Requisitos

- SDK de .NET 10 instalado.
- Windows o un entorno compatible con las funciones de consola utilizadas por la aplicación.

##  Cómo ejecutar el proyecto

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/dragon-51/nexus.git
   ```

2. Entrar en la carpeta:

   ```bash
   cd nexus
   ```

3. Restaurar las dependencias:

   ```bash
   dotnet restore
   ```

4. Ejecutar la aplicación:

   ```bash
   dotnet run
   ```

##  Conceptos de programación

El proyecto permite trabajar con conceptos como:

- Clases, objetos y constructores.
- Herencia y polimorfismo.
- Encapsulación mediante propiedades y modificadores de acceso.
- Métodos para representar comportamientos y acciones.
- Gestión del estado del jugador, el mapa, los objetos y los enemigos.

## 👨‍💻 Autor

**dragon-51**

## 📌 Estado del proyecto

NEXUS V1.0 — Proyecto en desarrollo y ampliación.
