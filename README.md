<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> b775996 (Creacion del README.)
# Modelo Entidad–Relación (MER) – Ferretería 

## Descripción del negocio
La Ferretería “El Durón” es un negocio dedicado a la venta de herramientas, materiales de construcción y artículos de ferretería en general.  
Este sistema permitirá gestionar los productos, empleados, clientes y las ventas diarias de manera eficiente, asegurando un control de inventario preciso y trazabilidad de todas las operaciones.

---

## Entidades principales

### PRODUCTO
**Atributos:**
- IdProducto (PK)  
- Nombre  
- Precio  
- Stock  
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
- usuarioRegistro  
- fechaRegistro  
- estado  

### VENTA
**Atributos:**
- IdVenta (PK)  
- Fecha  
- Total  
- IdCliente (FK → CLIENTE)  
- IdUsuario (FK → USUARIO)  
- usuarioRegistro  
- fechaRegistro  
- estado  

### DETALLEVENTA
**Atributos:**
- IdDetalle (PK)  
- Cantidad  
- Subtotal  
- IdVenta (FK → VENTA)  
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
- Integrante 1: (nombre)  
- Integrante 2: (nombre)  
=======
# Modelo Entidad–Relación (MER) – Ferretería

## Descripción del negocio
La **Ferretería “El Duro”** es un negocio dedicado a la venta de herramientas, materiales de construcción y artículos de mantenimiento.  
El sistema tiene como objetivo **gestionar los productos, clientes, usuarios y las ventas diarias**, permitiendo un control más eficiente del inventario y de las transacciones realizadas.

## Entidades principales

### USUARIO
**Atributos:**
- IdUsuario (PK)
- NombreUsuario
- Clave

### CLIENTE
**Atributos:**
- IdCliente (PK)
- Nombre
- Telefono
- Direccion

### PRODUCTO
**Atributos:**
- IdProducto (PK)
- Nombre
- Precio
- Stock

### VENTA
**Atributos:**
- IdVenta (PK)
- Fecha
- IdUsuario (FK)
- IdCliente (FK)
- Total

### DETALLEVENTA
**Atributos:**
- IdDetalle (PK)
- IdVenta (FK)
- IdProducto (FK)
- Cantidad
- Subtotal

## Integrantes del equipo
**Integrantes:**
- Castro Torricos Johnny Saul
<<<<<<< HEAD
- Hussain Cazas Zelaya 
=======
- Hussain Cazas Zelaya  
>>>>>>> e232522 (Creacion del README.)
>>>>>>> b775996 (Creacion del README.)
