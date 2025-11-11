CREATE DATABASE LabFerreteria;
GO
USE LabFerreteria;
GO
CREATE LOGIN usrFerreteria WITH PASSWORD = '123456',
	CHECK_POLICY = ON,
	CHECK_EXPIRATION = OFF,
	DEFAULT_DATABASE = LabFerreteria
GO
CREATE USER usrferreteria FOR LOGIN usrferreteria
GO
ALTER ROLE db_owner ADD MEMBER usrferreteria
GO

DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS DetalleVenta;

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Clave VARCHAR(100) NOT NULL,
);

CREATE TABLE Cliente (
    IdCliente INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Direccion VARCHAR(150),
);

CREATE TABLE Producto (
    IdProducto INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
);

CREATE TABLE Venta (
    IdVenta INT IDENTITY PRIMARY KEY,
    Fecha DATETIME DEFAULT GETDATE(),
    IdUsuario INT,
    IdCliente INT,
    Total DECIMAL(10,2),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
);

CREATE TABLE DetalleVenta (
    IdDetalle INT IDENTITY PRIMARY KEY,
    IdVenta INT,
    IdProducto INT,
    Cantidad INT,
    Subtotal DECIMAL(10,2),
    FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

ALTER TABLE Usuario ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuario ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuario ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

ALTER TABLE Cliente ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Cliente ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Cliente ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

ALTER TABLE Producto ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Producto ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Producto ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

ALTER TABLE Venta ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Venta ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Venta ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

ALTER TABLE DetalleVenta ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE DetalleVenta ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE DetalleVenta ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

GO

DROP PROCEDURE IF EXISTS paProductoListar;
GO

CREATE PROCEDURE paProductoListar @parametro VARCHAR(100)
AS
    SELECT IdProducto, Nombre, Precio, Stock, usuarioRegistro,fechaRegistro, estado
    FROM Producto
    WHERE estado>-1 AND Nombre LIKE '%' + REPLACE(@parametro,' ','%')+'%'
    ORDER BY estado DESC, Nombre ASC;


EXECUTE paProductoListar 'Martillo';



INSERT INTO Usuario (Nombre, Clave)
VALUES ('admin', 'dAFoRWBCRBpcRyECjAsQqw==');

INSERT INTO Producto (Nombre, Precio, Stock)
VALUES 
('Martillo de acero', 35.50, 20),
('Destornillador plano', 15.00, 50),
('Taladro eléctrico 500W', 450.00, 10),
('Llave inglesa ajustable', 28.90, 25),
('Serrucho de mano', 22.00, 18),
('Cinta métrica 5m', 12.50, 40),
('Caja de clavos 2”', 8.00, 100),
('Broca para metal 8mm', 10.00, 60),
('Silicona industrial', 25.00, 30),
('Pintura blanca 1L', 55.00, 15);

INSERT INTO Cliente (nombre,telefono,direccion)
VALUES ('Dario',74544429,'Av. Bolivar');

SELECT * FROM Usuario;
SELECT * FROM Producto;
SELECT * FROM Cliente;