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

DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS TipoEntrega;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Marca;
DROP TABLE IF EXISTS Usuario;
GO

SELECT name FROM sys.tables;

ALTER TABLE DetalleVenta DROP CONSTRAINT FK_DetalleVenta_Cliente; -- nombre de la FK real
ALTER TABLE Venta DROP CONSTRAINT FK_Venta_Cliente;
DROP TABLE Cliente;


CREATE TABLE Usuario (
    IdUsuario INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Clave VARCHAR(100) NOT NULL
);

CREATE TABLE Marca (
    IdMarca INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR (50)
);

CREATE TABLE Cliente (
    IdCliente INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Direccion VARCHAR(150),
    Entrega VARCHAR(10) NOT NULL 
        CHECK (Entrega IN ('Adomicilio', 'En Tienda')) 
        DEFAULT 'En Tienda'
);

CREATE TABLE Producto (
    IdProducto INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    IdMarca INT,
    FOREIGN KEY (IdMarca) REFERENCES Marca(IdMarca)
);

CREATE TABLE Venta (
    IdVenta INT IDENTITY PRIMARY KEY,
    IdCliente INT,
    Total DECIMAL(10,2) DEFAULT 0,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);

CREATE TABLE DetalleVenta (
    IdDetalle INT IDENTITY PRIMARY KEY,
    IdVenta INT,
    IdCliente INT,
    IdProducto INT,
    Cantidad INT,
    Subtotal DECIMAL(10,2),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto),
    FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta)
);

ALTER TABLE Usuario ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuario ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuario ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

ALTER TABLE Marca ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Marca ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Marca ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

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
BEGIN
    SELECT p.IdProducto, p.Nombre, p.Precio, p.Stock, m.Nombre AS Marca,
           p.usuarioRegistro, p.fechaRegistro, p.estado
    FROM Producto p
    LEFT JOIN Marca m ON p.IdMarca = m.IdMarca
    WHERE p.estado > -1 AND p.Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%'
    ORDER BY p.estado DESC, p.Nombre ASC;
END;
GO


EXECUTE paProductoListar 'Martillo';

DROP PROCEDURE IF EXISTS paClienteListar;
GO

CREATE PROCEDURE paClienteListar @parametro VARCHAR(100)
AS
BEGIN
    SELECT IdCliente, Nombre, Telefono, Direccion, estado, Entrega, usuarioRegistro, fechaRegistro
    FROM Cliente
    WHERE estado > -1 AND Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%'
    ORDER BY estado DESC, Nombre ASC;
END;
GO

EXECUTE paClienteListar 'Dario';
GO

DROP PROCEDURE IF EXISTS paDetalleVentaListar;
GO


CREATE PROCEDURE paDetalleVentaListar @parametro VARCHAR(100)
AS
BEGIN
    SELECT 
        dv.IdDetalle,
        c.Nombre AS Cliente,
        p.Nombre AS Producto,
        p.Precio,
        dv.Cantidad,
        c.Entrega,
        dv.Subtotal,
        (dv.Subtotal * dv.Cantidad) AS Total,
        dv.usuarioRegistro,
        dv.fechaRegistro,
        dv.estado
    FROM DetalleVenta dv
    INNER JOIN Cliente c ON dv.IdCliente = c.IdCliente
    INNER JOIN Producto p ON dv.IdProducto = p.IdProducto
    WHERE dv.estado > -1
      AND (c.Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%'
           OR p.Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%')
    ORDER BY dv.fechaRegistro DESC;
END;
GO


EXEC paDetalleVentaListar 'Taladro';


INSERT INTO Usuario (Nombre, Clave)
VALUES ('admin', 'dAFoRWBCRBpcRyECjAsQqw=='); --Clave:4321

INSERT INTO Marca (Nombre) 
VALUES 
('Tramontina'), ('Bosch'), ('Makita'), ('Stanley'), ('Truper'), 
('3M'), ('Sherwin-Williams'), ('Sin Marca');

INSERT INTO Producto (Nombre, Precio, Stock, IdMarca) VALUES
('Martillo de acero', 35.50, 20, 1),
('Destornillador plano', 15.00, 50, 2),
('Taladro eléctrico 500W', 450.00, 10, 3),
('Llave inglesa ajustable', 28.90, 25, 4),
('Serrucho de mano', 22.00, 18, 5),
('Cinta métrica 5m', 12.50, 40, 1),
('Caja de clavos 2”', 8.00, 100, 8),
('Broca para metal 8mm', 10.00, 60, 2),
('Silicona industrial', 25.00, 30, 6),
('Pintura blanca 1L', 55.00, 15, 7);

INSERT INTO Cliente (Nombre,Telefono,Direccion, Entrega)
VALUES 
('Dario',74544429,'Av. Bolivar', 'Adomicilio'),
('Mario',72857449,'Calle Loa', 'En Tienda'),
('Mario Rojas', 78965412, 'Av. San Martín #45', 'Adomicilio');

INSERT INTO Venta (IdCliente) VALUES (1), (3);

INSERT INTO DetalleVenta (IdVenta, IdCliente, IdProducto, Cantidad, Subtotal) VALUES
(1, 1, 3, 2, 450.00),
(1, 1, 1, 1, 35.50),
(2, 3, 5, 1, 22.00),
(2, 3, 4, 2, 57.80);


SELECT * FROM Usuario;
SELECT * FROM Producto;
SELECT * FROM Cliente;
SELECT * FROM Venta;
SELECT * FROM DetalleVenta;


DROP DATABASE Ferreteria;
GO

SELECT name FROM sys.databases;
USE master;
GO

DROP DATABASE Ferreteria;
GO

USE master;
GO

ALTER DATABASE Ferreteria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE Ferreteria;
GO

SELECT name FROM sys.databases;

DROP DATABASE LabFerreteria

ALTER DATABASE LabFerreteria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE LabFerreteria;

USE master;
GO

ALTER DATABASE LabFerreteria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE LabFerreteria;
GO