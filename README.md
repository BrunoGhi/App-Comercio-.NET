# SISTEMA GESTIÓN DE STOCK Y VENTAS DE UN COMERCIO DE ELECTRODOMÉSTICOS
## Descripción del Proyecto
Este proyecto consiste en el desarrollo de un sistema para gestionar las compras de un pequeño negocio de electrodomésticos y dispositivos electrónicos. La aplicación está construida utilizando C# con el framework .NET mediante WinForms.

## Requisitos no funcionales
**Sistema de autenticación**: Se garantiza el uso del sistema para aquellos usuarios que corresponde, discriminado niveles de permiso entre usuarios y administradores.

**Gestión de Clientes**: Se manipula la lista de clientes, que pueden ser estándar o premium. Se realiza el alta, baja o modificación de los mismos. Los clientes premium tienen la ventaja de poder optar por una garantía de 30, 60 o 90 días en sus compras. Los clientes estandar tienen un 10% de recargo en cada una de sus compras.

**Gestión de Productos**: Se mantiene una lista de productos con sus respectivos niveles de stock. Con posibilidad de realizar altas, bajas y modificaciones.

**Gestión de Compras**: Los clientes pueden realizar una o varias compras y en cada compra adquirir uno o varios productos con posibilidad de garantía si son premiums. Se registran los historiales de compras con detalles de cada cliente. Además se pueden visualizar estadísticas mediante gráficos.

## Tecnologías Utilizadas y Detalles:
- C# .NET
- Programación Orientada a objetos
- Arquitectura de software en capas ( 6 capas )
- Capa exclusiva de seguridad con encriptación
- Sistema de login para usuarios y administradores
- Visualización de estadísticas mediante gráficos
- Base de datos SQL ( SqlServer )
- Store Procedures con parámetros
- Persistencia de datos en XML
- Implementación de interfaces en capa de abstracción
- Manejo de errores
- Controles de usuario personalizados
- Validaciones mediante REGEX
