# Diseño del videojuego NEXUS

**Documento:** Diseño del juego  
**Versión propuesta:** 1.0  
**Base de diseño:** código desarrollado durante H3 en C#  
**Estado:** propuesta de evolución; los elementos descritos como mejoras todavía no se consideran implementados.

---

## 1. Visión del juego

### 1.1. Concepto

NEXUS es un videojuego de exploración y supervivencia de ciencia ficción. El jugador asume el papel de un **cadete** que ingresa a distintas realidades para investigar anomalías, recuperar recursos y regresar de forma segura a la base.

La propuesta parte del prototipo de H3, que ya permite registrar al cadete, seleccionar una realidad (Alpha, Beta o Gamma), recorrer un mapa representado mediante una matriz, encontrar eventos aleatorios, combatir enemigos, administrar un inventario y buscar un núcleo energético para completar la misión.

La evolución busca convertir esa simulación en una experiencia más clara y progresiva, manteniendo inicialmente el formato de aplicación de consola en C# y la estructura orientada a objetos existente.

### 1.2. Papel del jugador y objetivo

El jugador controla a un cadete de NEXUS. Su objetivo principal es explorar el entorno seleccionado, localizar el núcleo energético de la anomalía y alcanzar el punto de extracción sin que su salud, energía o estabilidad lleguen a niveles críticos.

Durante la partida deberá:

- Elegir una realidad para explorar.
- Moverse por el mapa y evitar obstáculos.
- Investigar los sectores y responder a los eventos que aparezcan.
- Enfrentarse a unidades hostiles cuando sea necesario.
- Recoger y utilizar objetos de supervivencia.
- Administrar energía y estabilidad para evitar el colapso.
- Encontrar el núcleo y completar la extracción.

### 1.3. Motivación para continuar

La motivación principal será superar misiones de dificultad creciente, descubrir las características de las distintas realidades y conseguir recursos y experiencia. Como evolución, los puntos de experiencia podrán utilizarse para desbloquear mejoras del cadete o preparar futuras expediciones. Esta progresión ampliada es una propuesta y no una función completa del código actual.

---

## 2. Ciclo principal de juego

El ciclo de una partida se plantea de la siguiente manera:

```text
┌─────────────────────────────┐
│       MENÚ PRINCIPAL        │
│       Iniciar misión        │
└──────────────┬──────────────┘
               ▼
┌─────────────────────────────┐
│ IDENTIFICACIÓN DEL CADETE   │
│ Nombre, edad y parámetros   │
│ iniciales                   │
└──────────────┬──────────────┘
               ▼
┌─────────────────────────────┐
│ SELECCIÓN DE REALIDAD       │
│ Alpha / Beta / Gamma        │
└──────────────┬──────────────┘
               ▼
┌─────────────────────────────┐
│ EXPLORACIÓN DEL MAPA        │
│ Movimiento con W, A, S, D   │
└──────────────┬──────────────┘
               ▼
┌─────────────────────────────┐
│ EVENTO EN EL SECTOR         │
│ Enemigo / objeto / radiación│
│ / zona sin evento           │
└──────────────┬──────────────┘
               ▼
┌─────────────────────────────┐
│ GESTIÓN DE RECURSOS         │
│ Salud, energía, estabilidad │
│ e inventario                │
└──────────────┬──────────────┘
               ▼
       ¿Se encontró el núcleo?
          │             │
         NO             SÍ
          │             ▼
          │    ┌──────────────────────┐
          │    │ DIRIGIRSE A LA       │
          │    │ EXTRACCIÓN SEGURA    │
          │    └──────────┬───────────┘
          │               ▼
          │    ┌──────────────────────┐
          │    │ MISIÓN COMPLETADA    │
          │    │ Recompensa y XP      │
          │    └──────────┬───────────┘
          │               ▼
          │    ┌──────────────────────┐
          │    │ PROGRESIÓN Y         │
          │    │ SIGUIENTE EXPEDICIÓN │
          │    └──────────────────────┘
          │
          └──────► Continuar explorando

Si la salud llega a cero o la entropía supera el límite,
se produce una desconexión de emergencia y termina la sesión.
```

**Aclaración:** en el código H3 ya existe la búsqueda del núcleo y la extracción. La recompensa de 100 XP se entrega al completar esa misión. La selección de nuevas misiones y una campaña con varias etapas son ampliaciones propuestas.

---

## 3. Mecánicas y reglas

### 3.1. Cadete y exploradores

En la versión H3 existe una clase `Cadete`, derivada de `Personaje`. El cadete posee nombre, edad, salud, energía, estabilidad, experiencia, posición en el mapa e inventario.

La propuesta mantiene al cadete como personaje principal. En una etapa posterior se podrán incorporar otros exploradores con atributos o especialidades diferentes, reutilizando la clase base `Personaje` y el polimorfismo que ya se utiliza para los enemigos. No se considera que esos exploradores adicionales estén implementados actualmente.

**Reglas propuestas para el cadete:**

- La salud representa la integridad biológica y disminuye al recibir daño.
- La energía se consume al realizar determinadas acciones, como atacar, y puede recuperarse con células de energía.
- La estabilidad representa la capacidad del cadete para resistir las anomalías.
- La experiencia registra el avance conseguido al completar misiones.
- La posición indica la ubicación del cadete dentro de la matriz del mapa.

Los valores de energía y estabilidad se manejan en una escala de 0 a 100. La evolución deberá mantener ese rango en todas las modificaciones, no solamente al crear el personaje.

### 3.2. Mapas y exploración

La clase `MapaRealidad` representa cada entorno mediante una matriz bidimensional de caracteres. El jugador se desplaza con las teclas W, A, S y D. La tecla Q permite salir del modo de exploración y volver al panel de control.

El prototipo contiene tres realidades:

- **Alpha – Nexo Urbano.**
- **Beta – Yermo Cuántico.**
- **Gamma – Vortex Espacial.**

Los mapas contienen obstáculos y casillas transitables. La representación utiliza caracteres y colores para facilitar su lectura.

| Símbolo | Significado |
|---|---|
| C | Posición actual del cadete (se dibuja sobre el mapa) |
| █ | Obstáculo no transitable |
| . o espacio | Sector transitable |
| N | Núcleo energético que debe localizarse |
| ~, R, O y * | Elementos o zonas especiales representados en los mapas |

**Reglas de movimiento:**

1. El cadete solo puede desplazarse dentro de los límites de la matriz.
2. No puede atravesar obstáculos.
3. Al entrar en una casilla se puede activar un evento.
4. Al encontrar el núcleo N, el objetivo principal queda asegurado y se habilita la extracción.
5. Después de recuperar el núcleo, el jugador debe regresar por un sector transitable y completar la extracción.

Como mejora, cada realidad podrá tener una identidad visual, eventos y distribución de obstáculos propios, conservando la matriz como base de representación.

### 3.3. Energía, estabilidad, salud y entropía

La supervivencia depende de administrar tres indicadores:

- **Salud:** comienza en 100 puntos y disminuye cuando el cadete recibe ataques.
- **Energía:** se utiliza en acciones y combate. Puede recuperarse con células de energía o mediante la operación de sobrecarga disponible en el panel.
- **Estabilidad:** puede recuperarse con objetos de escudo y representa la resistencia del cadete frente a las anomalías.

El sistema calcula la entropía con la fórmula:

`Entropía = (100 - Estabilidad) × (100 - Energía)`

Si la entropía supera 5600 o la salud llega a cero, el sistema genera una alerta crítica y detiene la sesión mediante una desconexión de emergencia.

La interfaz deberá mostrar estos indicadores de forma permanente durante la exploración y el combate, para que el jugador pueda tomar decisiones antes de llegar a una condición crítica.

### 3.4. Misiones

La misión principal actual se denomina **“La anomalía perdida”**. Consiste en localizar el núcleo energético marcado con N y realizar una extracción segura.

**Reglas de la misión actual:**

1. La misión comienza después de validar los datos y parámetros iniciales del cadete.
2. El objetivo aparece en el panel como la búsqueda del núcleo energético.
3. Al entrar en la casilla N, el sistema marca la misión como completada.
4. La extracción no se permite antes de localizar el núcleo.
5. Al extraer al cadete después de completar el objetivo, se otorgan 100 puntos de experiencia.

**Evolución propuesta:** incorporar un registro de misiones que muestre nombre, descripción, objetivo, estado y recompensa. En futuras versiones se podrán añadir misiones secundarias de exploración y recolección, sin eliminar la misión principal.

### 3.5. Eventos y combate

Al explorar una casilla, el prototipo puede activar distintos resultados aleatorios:

- Encuentro con un enemigo.
- Hallazgo de un objeto.
- Pérdida de energía por radiación.
- Ningún evento especial.

Los enemigos actuales son la **Unidad Centinela Automatizada** y el **Infiltrado Kreeg de la Anomalía**. Ambos heredan de `Personaje` y cuentan con ataques propios. El combate se desarrolla por turnos.

Durante un combate, el jugador puede:

1. Atacar con el rifle cuántico, consumiendo energía.
2. Utilizar un objeto del inventario.
3. Intentar escapar.

La evolución deberá mostrar con claridad el daño causado, la salud restante y el consumo de energía después de cada turno.

### 3.6. Inventario

El cadete comienza con una célula de energía y un escudo. Durante la exploración puede encontrar células de energía, escudos y módulos de ataque.

El inventario actual permite consultar objetos, utilizarlos o eliminarlos. Sus efectos principales son:

- **Célula de energía:** recupera energía.
- **Escudo:** recupera estabilidad.
- **Módulo de ataque:** representa un objeto de mejora temporal previsto por el prototipo.

Como mejora, cada objeto tendrá un nombre, una descripción y un efecto claramente indicado antes de utilizarlo. También se propone mostrar la cantidad disponible cuando el sistema evolucione a un inventario con unidades acumulables.

---

## 4. Diseño de pantallas

Los siguientes wireframes son bocetos conceptuales de la interfaz. No son pantallas funcionales ni representan una interfaz gráfica ya implementada. La propuesta conserva la presentación en consola y organiza mejor la información que actualmente se muestra mediante menús y paneles de texto.

### 4.1. Pantalla 1 – Menú principal

**Objetivo:** permitir iniciar una expedición, consultar información y salir del juego.

```text
╔════════════════════════════════════════════╗
║                                            ║
║              N E X U S                     ║
║         SISTEMA DE EXPLORACIÓN             ║
║                                            ║
║       "La realidad está en tus manos"      ║
║                                            ║
║           ┌──────────────────┐             ║
║           │  1. Nueva misión │             ║
║           │  2. Continuar    │             ║
║           │  3. Exploradores │             ║
║           │  4. Opciones     │             ║
║           │  5. Salir        │             ║
║           └──────────────────┘             ║
║                                            ║
║             NEXUS TRAINING SYSTEM          ║
╚════════════════════════════════════════════╝
```

**Elementos visibles:** título del juego, breve frase de ambientación y opciones principales.

**Navegación:** “Nueva misión” lleva a la identificación del cadete y selección de realidad. “Continuar” podrá habilitarse cuando exista guardado de partidas. “Exploradores” mostrará los datos del personaje y “Opciones” permitirá ajustar preferencias básicas.

**Relación con H3:** el prototipo ya presenta una bienvenida y solicita los datos del cadete, pero todavía no cuenta con este menú principal completo. Las opciones adicionales son propuestas.

### 4.2. Pantalla 2 – Exploración o mapa

**Objetivo:** mostrar el entorno, la posición del cadete y sus indicadores de supervivencia.

```text
╔════════════════════════════════════════════╗
║ REALIDAD: BETA – YERMO CUÁNTICO            ║
║ CADETE: [Nombre]                           ║
║ SALUD: 100%  ENERGÍA: 75%  ESTABILIDAD: 80%║
╠════════════════════════════════════════════╣
║                                            ║
║       █ █ █ █ █ █ █ █                      ║
║       █ C . . ~ . N █                      ║
║       █ R . . . . . █                      ║
║       █ █ █ █ █ █ █ █                      ║
║                                            ║
║ LEYENDA:                                   ║
║ C: Cadete   N: Núcleo   █: Obstáculo       ║
║ ~: Zona especial   R: Recurso              ║
║                                            ║
╠════════════════════════════════════════════╣
║ OBJETIVO: Localizar el núcleo energético   ║
║ CONTROLES: W A S D = Mover | Q = Regresar  ║
╚════════════════════════════════════════════╝
```

**Elementos visibles:** nombre de la realidad, nombre del cadete, salud, energía, estabilidad, mapa, leyenda, objetivo y controles.

**Navegación:** desde el menú de operaciones se entra a exploración. W, A, S y D mueven al cadete; Q vuelve al panel de control. Al entrar en una casilla especial se muestra el resultado del evento.

**Relación con H3:** esta pantalla se basa directamente en `MapaRealidad.Renderizar()` y `MenuMovimiento()`, que ya dibujan la matriz, la posición del cadete y los controles.

### 4.3. Pantalla 3 – Misiones

**Objetivo:** explicar qué debe conseguir el jugador y cuál es el estado de cada objetivo.

```text
╔════════════════════════════════════════════╗
║               REGISTRO DE MISIONES         ║
╠════════════════════════════════════════════╣
║                                            ║
║  MISIÓN PRINCIPAL                          ║
║  La anomalía perdida                       ║
║                                            ║
║  Objetivo: localizar el núcleo energético  ║
║  Sector: realidad seleccionada             ║
║  Estado: [ EN PROGRESO ]                   ║
║                                            ║
║  Recompensa: 100 XP                        ║
║                                            ║
║  Pasos:                                    ║
║  [ ] Explorar el mapa                      ║
║  [ ] Localizar el núcleo N                 ║
║  [ ] Llegar al punto de extracción         ║
║                                            ║
╠════════════════════════════════════════════╣
║  [1] Ver objetivo  [2] Volver              ║
╚════════════════════════════════════════════╝
```

**Elementos visibles:** nombre de la misión, descripción, objetivo, estado, pasos y recompensa.

**Navegación:** se accede desde el panel principal. Al seleccionar una misión se consulta su objetivo. Al completarla, el estado cambia y se informa al jugador de la recompensa.

**Relación con H3:** actualmente el objetivo y su estado aparecen en el panel de estadísticas mediante `_misionCompletada`. El registro detallado y la lista de pasos son una mejora propuesta.

### 4.4. Pantalla 4 – Exploradores e inventario

**Objetivo:** consultar el estado del cadete y administrar los objetos recolectados.

```text
╔════════════════════════════════════════════╗
║          CADETE E INVENTARIO               ║
╠════════════════════════════════════════════╣
║  EXPLORADOR ACTUAL: [Nombre del cadete]     ║
║                                            ║
║  Salud:       [████████████████] 100       ║
║  Energía:     [████████████░░░░]  75       ║
║  Estabilidad: [█████████████░░░]  80       ║
║  Experiencia: 0 XP                         ║
║                                            ║
╠════════════════════════════════════════════╣
║  INVENTARIO                                ║
║                                            ║
║  1. Célula de energía                      ║
║     Recupera energía                       ║
║  2. Escudo                                 ║
║     Recupera estabilidad                   ║
║  3. Módulo de ataque                       ║
║     Mejora temporal                        ║
║                                            ║
║  [1] Usar objeto                           ║
║  [2] Eliminar objeto                       ║
║  [3] Regresar                              ║
╚════════════════════════════════════════════╝
```

**Elementos visibles:** datos del cadete, barras de salud, energía y estabilidad, experiencia y lista de objetos con sus efectos.

**Navegación:** se accede desde el menú principal o panel de operaciones. El jugador selecciona un objeto para usarlo o eliminarlo y después regresa al menú anterior.

**Relación con H3:** se apoya en las propiedades de `Cadete`, el método `MostrarInventario()` y `MenuInventario()`. La presentación agrupada y las descripciones son mejoras visuales propuestas.

### 4.5. Navegación general

```text
MENÚ PRINCIPAL
   ├── Nueva misión
   │      └── Identificación del cadete
   │             └── Selección de realidad
   │                    └── Panel de operaciones
   │                           ├── Explorar mapa
   │                           ├── Misiones
   │                           ├── Explorador e inventario
   │                           ├── Escanear / recuperar energía
   │                           └── Extracción
   ├── Continuar (futuro: requiere guardado)
   ├── Exploradores
   ├── Opciones
   └── Salir
```

El combate se abrirá como una pantalla temporal cuando ocurra un encuentro durante la exploración. Al terminar, el jugador regresará al mapa si el cadete sigue en condiciones de continuar.

---

## 5. Experiencia de usuario

La interfaz debe ser sencilla, consistente y adecuada para una aplicación de consola. El jugador debe comprender rápidamente su situación y saber qué acción puede realizar.

### 5.1. Legibilidad

- Utilizar títulos cortos y descriptivos para identificar cada pantalla.
- Mantener alineados los indicadores y las opciones.
- Evitar mostrar demasiada información al mismo tiempo.
- Usar símbolos fáciles de distinguir en el mapa.
- Mantener una leyenda visible para los caracteres especiales.

### 5.2. Jerarquía visual

La información más importante será la supervivencia del cadete: salud, energía y estabilidad. En segundo lugar se mostrará el objetivo de la misión y, después, las opciones disponibles.

Los colores conservarán una función constante: verde para estados positivos, amarillo para advertencias, rojo para peligro y cian para elementos tecnológicos o informativos. El color no será la única forma de comunicar un estado; también se utilizarán palabras como “ALERTA”, “COMPLETADO” o “BLOQUEADO”.

### 5.3. Consistencia

- Mantener el mismo formato de títulos, paneles y barras en las pantallas.
- Utilizar los mismos nombres para los recursos y acciones en todo el juego.
- Conservar controles coherentes: W, A, S y D para desplazamiento y Q para regresar desde la exploración.
- Mostrar las opciones numéricas junto con su acción.

### 5.4. Facilidad de navegación

- Permitir regresar al panel principal desde las pantallas secundarias.
- No ocultar el objetivo actual de la misión.
- Solicitar confirmación antes de eliminar objetos importantes o abandonar una misión, como mejora futura.
- Validar las entradas y explicar cómo corregirlas cuando sean incorrectas.

### 5.5. Retroalimentación

Cada acción debe producir una respuesta clara. Por ejemplo:

- Si el jugador intenta atravesar una pared, mostrar que el paso está bloqueado.
- Si recoge un objeto, indicar su nombre y añadirlo al inventario.
- Si pierde energía, mostrar cuánta energía perdió y el valor restante.
- Si gana un combate, comunicar el resultado.
- Si encuentra el núcleo, actualizar el objetivo.
- Si completa la extracción, mostrar la recompensa obtenida.
- Si ocurre una condición crítica, explicar por qué se interrumpe la sesión.

---

## 6. Alcance y restricciones

La evolución se realizará por etapas para conservar una base viable y evitar que el proyecto crezca más de lo que puede desarrollarse y probarse.

### 6.1. Funcionalidades prioritarias

| Prioridad | Funcionalidad | Justificación |
|---|---|---|
| Alta | Mantener identificación y selección de las tres realidades | Ya existen en H3 y forman el inicio de la partida. |
| Alta | Mejorar la presentación del panel y el mapa | Facilita comprender el estado y el objetivo. |
| Alta | Conservar movimiento y límites del mapa | Es la base de la exploración actual. |
| Alta | Mantener salud, energía, estabilidad y control de entropía | Son el núcleo del sistema de supervivencia. |
| Alta | Mantener eventos, enemigos y combate por turnos | Dan riesgo y variedad a la exploración. |
| Alta | Mejorar el inventario y los mensajes de uso | Permite administrar recursos con mayor claridad. |
| Alta | Mostrar misión, estado y extracción | Hace más comprensible el progreso de la partida. |

### 6.2. Funcionalidades opcionales o futuras

- Incorporar varios exploradores seleccionables con atributos distintos.
- Añadir misiones secundarias y más objetivos por realidad.
- Guardar y cargar partidas.
- Incorporar nuevos mapas y eventos.
- Añadir progresión mediante experiencia y mejoras desbloqueables.
- Incluir efectos de sonido y animaciones de texto.
- Incorporar una interfaz gráfica en una etapa posterior, si el tiempo y los requisitos lo permiten.
- Ampliar la comunicación con un servidor externo. El prototipo ya intenta conectarse a un servidor TCP y continúa en modo local si no está disponible; la experiencia principal no debe depender de esa conexión.

### 6.3. Restricciones técnicas y de proyecto

1. La propuesta debe partir del código H3 y conservar sus mecánicas principales.
2. La implementación inicial continuará en C# y en la aplicación de consola .NET existente.
3. Se mantendrá la organización orientada a objetos, aprovechando clases como `Personaje`, `Cadete`, `Enemigo`, `centinela`, `MapaRealidad` y `SistemaNexus`.
4. Los mapas seguirán representándose inicialmente mediante matrices bidimensionales.
5. La energía y la estabilidad deberán permanecer dentro del rango de 0 a 100 después de cualquier operación.
6. Las nuevas funciones deberán poder probarse sin depender obligatoriamente de un servidor externo.
7. Se priorizará que la partida principal sea estable antes de añadir contenido opcional.
8. Los bocetos de este documento describen una propuesta de diseño; no implican que las pantallas ya estén programadas.

### 6.4. Resultado esperado

Al completar la primera etapa de evolución, NEXUS deberá ofrecer una partida de consola más organizada y fácil de entender, en la que el jugador pueda iniciar una expedición, explorar una realidad, gestionar sus recursos, enfrentar eventos, localizar el núcleo energético y completar la extracción. Las mejoras posteriores podrán ampliar la variedad de exploradores, misiones y mapas sin perder la identidad del proyecto original.

---

## Referencia al proyecto H3

Este diseño toma como referencia el código actual de `Program.cs` y la descripción del proyecto en `README.md` del repositorio. Las funciones descritas como existentes corresponden al prototipo consultado; las identificadas como propuestas o futuras representan la evolución planificada y no deben confundirse con funcionalidades ya implementadas.
