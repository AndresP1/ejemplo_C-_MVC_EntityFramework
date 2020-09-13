Aplicación ASP.NET MVC con C#, Entity Framework y utilización de LINQ

# ejemplo_Csharp_MVC_EntityFramework
ejemplo de obligatorio de facultad, materia Programación 3

Este segundo módulo, será utilizado por los comercios que venden los productos provistos la distribuidora de la parte 1. Permitirá la gestión de sus clientes 
y de los pedidos de productos que realiza a la distribuidora. 
El catálogo de productos lo deberá levantar de un archivo de texto que la distribuidora le provee (el generado en la aplicación de la primera parte).
IMPORTANTE: Este módulo deberá ser enteramente implementado en ASP.NET MVC 5.0, utilizando Entity Framework Code First como mecanismo de acceso a datos 
y todas las consultas deberán realizarse exclusivamente utilizando Linq.
IMPLEMENTACIÓN DEL SEGUNDO MÓDULO:
RF02-01: Identificación en el sistema (Login). Usuario habilitado: todos. Se ingresa un email y contraseña, se verifica que sea uno de los usuarios
autorizados en el sistema. Estos usuarios pueden tener rol “Empleado” o “Cliente”. Los empleados deberán estar precargados en el sistema y deberá 
haber uno con email: empleado1@empleado.com y contraseña: Empleado1
RF02-02: Carga de productos. Usuario habilitado: empleado.
Al presionar un botón se cargarán en la base de datos (utilizando EF Code First) los productos incluidos en el archivo de texto generado en la parte 1
del obligatorio. Este archivo se tomará de una carpeta "Archivos" bajo la raíz del sitio.
Se tomarán todos los datos de los productos y se deberán almacenar por separado los productos
importados y los de producción nacional. Al cargar los productos el precio de venta quedará
establecido igual a su precio sugerido de venta.
RF02-03: Asignar un precio de venta al producto. Usuario habilitado: empleado.
A partir de un listado de productos, se seleccionará uno y se redirigirá a una vista donde se podrá
asignar o modificar su precio de venta. Ese precio de venta no podrá ser superior en un 10% al precio
sugerido de venta que se tomó desde el archivo, ni tampoco podrá ser menor al precio sugerido.
RF02-04: Registro de clientes. Usuario habilitado: usuario sin identificar.
Cualquier usuario web (sin identificar) se podrá registrar en el sistema, siempre que no lo haya hecho
anteriormente como empleado o como cliente.
Para registrarse deberá ingresar un email válido, una contraseña que incluya al menos 6 caracteres
que incluyan como mínimo una letra mayúscula, una letra minúscula y un dígito. También ingresarán
su nombre y se registrará la fecha en que se realizó el registro. Al registrarse correctamente se le
asigna un ID autonumérico y el rol de cliente, y deberá quedar automáticamente identificado en el
sitio, donde podrá continuar realizando compras, verificando pedidos, o cualquier otra opción que
tengan habilitada los usuarios de tipo cliente.
RF02-05: Búsqueda y filtrado de productos. Usuario habilitado: todos (requiere identificarse).
Los productos se podrán buscar por código, por un texto incluido en el nombre, por un texto que esté
incluido en la descripción, por rango de precios de venta (los que estén incluidos entre un precio
mínimo y otro máximo), por tipo (importado o fabricado).
Esos criterios de búsqueda son independientes (se aplica un solo filtro a la vez). Si en la casilla de
búsqueda no se ingresa ningún filtro, se deberán mostrar todos los productos, ordenados por tipo
(nacionales primero e importados debajo) y dentro de tipo ordenados por nombre ascendente.
RF02-06: Agregar un producto al carrito. Usuario habilitado: cliente.
El cliente podrá agregar un producto a su carrito de compras. A partir de la búsqueda anterior
selecciona un producto y lo agrega al carrito. Por defecto en el carrito queda agregada una unidad
del producto, y el precio por unidad será el precio de venta estipulado en el producto.
RF02-07: Finalización de pedido. Usuario habilitado: cliente.
El cliente identificado podrá visualizar el contenido de su carrito, agregar o disminuir unidades de los
productos y quitar productos. Una vez listo, podrá confirmar el pedido. Si confirma el pedido este se
almacenará, y se deberá limpiar su carrito.
