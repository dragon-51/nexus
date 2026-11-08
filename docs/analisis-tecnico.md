# Análisis técnico de alternativas para NEXUS

**Fecha de consulta:** 8 de noviembre de 2026. **Estado:** investigación y recomendación; WPF y SQL Server aún no están implementados.

## 1. Criterios

Se comparan alternativas por mantenibilidad, viabilidad respecto a C#/.NET 10, experiencia del jugador, persistencia y esfuerzo.

## 2. Alternativas de interfaz

| Opción | Ventajas | Desventajas | Decisión |
|---|---|---|---|
| Consola | Reutiliza lo existente y es rápida. | Limita mapa interactivo, animaciones y composición visual. | Mantener temporalmente para probar reglas. |
| Windows Forms | Diseñador visual y controles de escritorio; curva inicial sencilla. | Para mapa dinámico, estilos y animaciones puede requerir más trabajo manual. | Válida si el equipo ya domina WinForms. |
| WPF + XAML + MVVM | Data binding, estilos, layout, gráficos y animaciones; separa presentación y comportamiento. | Solo Windows; requiere aprender XAML y MVVM. | **Recomendado** si el objetivo es Windows. |
| MAUI/Avalonia | Permiten estudiar un destino multiplataforma. | Añaden decisiones y aprendizaje innecesarios si el proyecto es Windows. | No priorizar salvo requisito multiplataforma. |

WPF es la opción recomendada para las pantallas de menú, mapa, misión e inventario. Microsoft documenta XAML, binding y recursos gráficos. La decisión depende de confirmar Windows como plataforma de entrega.

## 3. Alternativas de arquitectura

| Opción | Ventajas | Desventajas | Evaluación |
|---|---|---|---|
| Todo en `Program.cs` | Sin reorganización. | Acoplamiento alto y pruebas difíciles. | No usar como arquitectura final. |
| Carpetas en un proyecto | Sencillo para empezar. | Dependencias menos controladas. | Paso inicial aceptable. |
| Desktop, Application, Core e Infrastructure | Límites claros y dominio independiente de WPF/SQL. | Más configuración inicial. | Recomendada por etapas; empezar con Desktop y Core. |

La interfaz debe llamar a servicios de aplicación, y estos a reglas de dominio y contratos de persistencia. La UI no debe consultar SQL directamente.

## 4. Alternativas de acceso a datos

| Opción | Ventajas | Desventajas | Decisión |
|---|---|---|---|
| ADO.NET / Microsoft.Data.SqlClient | Control directo del SQL; útil para consultas específicas y aprender SQL. | Requiere escribir consultas, mapeo, conexiones y transacciones manualmente. | Usar si la consigna exige SQL directo o hay consultas específicas. |
| Entity Framework Core 10 | Entidades C#, LINQ, seguimiento de cambios, migraciones y transacciones. | Requiere aprender DbContext y relaciones; algunas consultas pueden necesitar optimización. | **Recomendado** para guardar expediciones e inventario. |
| JSON local | Fácil para prototipos sin servidor. | No satisface el requisito SQL Server y dificulta relaciones y consultas. | Solo respaldo temporal/offline. |

Se recomienda `Microsoft.EntityFrameworkCore.SqlServer` 10.x. El proveedor oficial conecta EF Core con SQL Server y usa Microsoft.Data.SqlClient internamente. EF Core 10 requiere .NET 10, igual que el proyecto actual.

## 5. Alternativas SQL Server

| Opción | Ventajas | Desventajas | Uso |
|---|---|---|---|
| SQL Server Express LocalDB | Instancia local orientada a desarrolladores y sencilla para pruebas. | No es un servidor remoto compartido. | Desarrollo individual en una PC. |
| SQL Server Express | Instancia de servidor que puede aceptar conexiones de otros equipos. | Requiere instalación/configuración y tiene límites propios de Express. | Demostración o red pequeña. |
| SQL Server/Azure SQL administrado | Centraliza datos para varios equipos. | Requiere infraestructura, red, credenciales y administración/costo. | Etapa futura multiusuario. |

**Decisión:** LocalDB durante desarrollo individual. Si varios equipos deben compartir partidas, utilizar SQL Server Express o un servicio SQL administrado. No presentar LocalDB como solución remota multiusuario.

## 6. Recomendaciones

1. Conservar C# y .NET 10.
2. Extraer primero las reglas de H3 a `Nexus.Core`.
3. Separar movimiento, combate, inventario, misión y sesión en servicios.
4. Probar el dominio antes de construir pantallas.
5. Crear WPF con MVVM; la vista muestra y envía comandos, no calcula reglas.
6. Agregar EF Core 10 y SQL Server para guardar progreso.
7. Mantener modo local si la base no está disponible, avisando que no se guardará.
8. Mover la IP TCP de IRIS a configuración y mantener esa integración como opcional.
9. Añadir pruebas para rangos, entropía, movimiento, combate, inventario y misión.
10. No ampliar a multijugador ni misiones múltiples antes de completar guardado y recuperación.

## 7. Riesgos y mitigaciones

| Riesgo | Mitigación |
|---|---|
| Migrar UI y lógica simultáneamente | Extraer y probar el dominio primero. |
| WPF no cumple requisito multiplataforma | Confirmar sistema objetivo antes de implementarlo. |
| Guardado parcial | Persistir expedición, inventario y misión en una transacción. |
| Base inaccesible | Reintento y modo local claramente indicado. |
| Arquitectura excesiva | Introducir capas por etapas, sin microservicios. |
| Credenciales expuestas | No incluir secretos en Git; usar configuración local y privilegios mínimos. |

## 8. Verificación de información obtenida con IA

La IA se utilizó como apoyo para comparar alternativas y organizar el borrador. No se considera fuente primaria. Las afirmaciones técnicas se contrastaron con Microsoft Learn.

| Afirmación | Verificación documental | Resultado |
|---|---|---|
| WPF incluye XAML, data binding, layout y gráficos/animación. | WPF overview oficial. | Confirmado. |
| WPF solo funciona en Windows. | Apartado de plataformas de WPF overview. | Confirmado; recomendación condicionada a Windows. |
| Existe guía WPF para .NET 10. | Tutorial oficial WPF con .NET 10. | Confirmado. |
| EF Core dispone de proveedor SQL Server. | Página oficial del proveedor y tabla de proveedores. | Confirmado; paquete `Microsoft.EntityFrameworkCore.SqlServer`. |
| EF Core 10 requiere .NET 10. | Notas oficiales de EF Core 10. | Confirmado. |
| EF Core ofrece transacciones y migraciones. | Documentación oficial de transacciones y migraciones. | Confirmado. |
| LocalDB está orientado al desarrollo local. | Documentación SQL Server LocalDB. | Confirmado; tiene limitaciones frente a instancia compartida. |

### Uso responsable de IA

- **Uso:** comparar tecnologías, estructurar tablas, proponer entidades y resumir documentación.
- **Verificación pendiente antes de programar:** instalar versiones compatibles, compilar, conectar a SQL Server y probar guardado/recuperación.
- **No afirmar:** que WPF, EF Core o SQL Server ya están implementados; que la conexión TCP actual es una arquitectura cliente-servidor completa; ni que la IA ejecutó pruebas del proyecto.
- **Evidencia posterior:** paquetes instalados, migración generada, base creada y prueba real de una partida recuperada.

## 9. Fuentes técnicas consultadas

Documentación oficial de Microsoft:

1. [WPF overview](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview)
2. [WPF data binding](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/)
3. [Tutorial WPF con .NET 10](https://learn.microsoft.com/es-es/dotnet/desktop/wpf/get-started/create-app-visual-studio)
4. [Common .NET application architectures](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
5. [Proveedor SQL Server para EF Core](https://learn.microsoft.com/en-us/ef/core/providers/sql-server/)
6. [Novedades de EF Core 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
7. [Transacciones en EF Core](https://learn.microsoft.com/en-us/ef/core/saving/transactions)
8. [Migraciones en EF Core](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations)
9. [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

## 10. Conclusión

La evolución más equilibrada es conservar C#/.NET 10, separar el dominio actual, construir una interfaz WPF y guardar progreso en SQL Server mediante EF Core 10. La implementación gradual mejora mantenibilidad y experiencia sin reescribir NEXUS de una sola vez.
