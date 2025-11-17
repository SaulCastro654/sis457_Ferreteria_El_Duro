# Modelo Entidad–Relación (MER) – Ferretería 

## Descripción del negocio
La Ferretería “El Durón” es un negocio dedicado a la venta de herramientas, materiales de construcción y artículos de ferretería en general.  
Este sistema permitirá gestionar los productos, empleados, clientes y las ventas diarias de manera eficiente, asegurando un control de inventario preciso y trazabilidad de todas las operaciones.

---

## Entidades principales

### MARCA
**Atributos:**
- IdMarca (PK)  
- Nombre
- usuarioRegistro
- fechaRegistro
- estado

### PRODUCTO
**Atributos:**
- IdProducto (PK)  
- Nombre  
- Precio  
- Stock
- IdMarca (FK)
- usuarioRegistro  
- fechaRegistro  
- estado

### USUARIO
**Atributos:**
- IdUsuario (PK)  
- Nombre  
- Clave  
- usuarioRegistro  
- fechaRegistro  
- estado  

### CLIENTE
**Atributos:**
- IdCliente (PK)  
- Nombre  
- Telefono  
- Direccion
- Entrega ("Adomicilio, "En Tienda")
- usuarioRegistro  
- fechaRegistro  
- estado  

### VENTA
**Atributos:**
- IdVenta (PK)  
- IdCliente (FK → CLIENTE)  
- Total 
- usuarioRegistro  
- fechaRegistro  
- estado  

### DETALLEVENTA
**Atributos:**
- IdDetalle (PK)  
- Cantidad  
- Subtotal  
- IdVenta (FK → VENTA)
- IdCliente (FK → CLIENTE)  
- IdProducto (FK → PRODUCTO)  
- usuarioRegistro  
- fechaRegistro  
- estado  

---

## Tecnologías
- Visual Studio .NET (C#)  
- Windows Forms / WPF (Capa de Presentación)  
- Entity Framework / ADO.NET (Capa Lógica y Acceso a Datos)

---

## Integración del equipo
-Johnny Saul Castro Torricos
-Hussain Cazas Zelaya 

>>>>>>> e232522 (Creacion del README.)
>>>>>>> b775996 (Creacion del README.)
