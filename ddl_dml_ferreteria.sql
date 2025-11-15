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
    Fecha DATE NULL
);

CREATE TABLE Producto (
    IdProducto INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    Marca VARCHAR (50)
);

CREATE TABLE DetalleVenta (
    IdDetalle INT IDENTITY PRIMARY KEY,
    IdCliente INT,
    IdProducto INT,
    Cantidad INT,
    Subtotal DECIMAL(10,2),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
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

ALTER TABLE DetalleVenta ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE DetalleVenta ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE DetalleVenta ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminado, 0: Inactivo, 1: Activo

GO

DROP PROCEDURE IF EXISTS paProductoListar;
GO

CREATE PROCEDURE paProductoListar @parametro VARCHAR(100)
AS
    SELECT IdProducto, Nombre, Precio, Stock, Marca, usuarioRegistro, fechaRegistro, estado
    FROM Producto
    WHERE estado>-1 AND Nombre LIKE '%' + REPLACE(@parametro,' ','%')+'%'
    ORDER BY estado DESC, Nombre ASC;


EXECUTE paProductoListar 'Martillo';

DROP PROCEDURE IF EXISTS paClienteListar;
GO

CREATE PROCEDURE paClienteListar @parametro Varchar(100)
AS
BEGIN
    SELECT IdCliente, Nombre, Telefono, Direccion, estado, Fecha, usuarioRegistro, fechaRegistro
    FROM Cliente
    WHERE estado > -1 
      AND Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%'
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
        dv.IdDetalle, c.Nombre AS Cliente, p.Nombre AS Producto, dv.Cantidad,
        dv.Subtotal, dv.usuarioRegistro, dv.fechaRegistro, dv.estado
    FROM DetalleVenta dv
        INNER JOIN Cliente c ON dv.IdCliente = c.IdCliente
        INNER JOIN Producto p ON dv.IdProducto = p.IdProducto
    WHERE dv.estado > -1
        AND (c.Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%'
             OR p.Nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%')
    ORDER BY dv.fechaRegistro DESC;
END;
GO

EXEC paDetalleVentaListar 'Martillo';


INSERT INTO Usuario (Nombre, Clave)
VALUES ('admin', 'dAFoRWBCRBpcRyECjAsQqw=='); --Clave:4321

INSERT INTO Producto (Nombre, Precio, Stock, Marca)
VALUES
('Martillo de acero', 35.50, 20, 'Tramontina'),
('Destornillador plano', 15.00, 50, 'Bosch'),
('Taladro eléctrico 500W', 450.00, 10, 'Makita'),
('Llave inglesa ajustable', 28.90, 25, 'Stanley'),
('Serrucho de mano', 22.00, 18, 'Truper'),
('Cinta métrica 5m', 12.50, 40, 'Tramontina'),
('Caja de clavos 2”', 8.00, 100, 'Sin Marca'),
('Broca para metal 8mm', 10.00, 60, 'Bosch'),
('Silicona industrial', 25.00, 30, '3M'),
('Pintura blanca 1L', 55.00, 15, 'Sherwin-Williams');

INSERT INTO Cliente (nombre,telefono,direccion, Fecha)
VALUES 
('Dario',74544429,'Av. Bolivar', '2024-01-15'),
('Mario',72857449,'Calle Loa', '2024-03-02'),
('Mario Rojas', 78965412, 'Av. San Martín #45', '2023-11-20');

INSERT INTO DetalleVenta (IdCliente, IdProducto, Cantidad, Subtotal)
VALUES
(1, 3, 2, 900.00),   -- Cliente 1 compró 2 taladros
(1, 1, 1, 35.50);    -- Cliente 1 compró 1 martillo

INSERT INTO DetalleVenta (IdCliente, IdProducto, Cantidad, Subtotal)
VALUES
(3, 5, 1, 22.00),  -- Cliente 3 compró 1 serrucho
(3, 4, 2, 57.80);  -- Cliente 3 compró 2 llaves inglesas

UPDATE Venta
SET Total = (SELECT SUM(Subtotal) FROM DetalleVenta WHERE IdVenta = 2)
WHERE IdVenta = 2;


SELECT * FROM Usuario;
SELECT * FROM Producto;
SELECT * FROM Cliente;
SELECT * FROM DetalleVenta;

USE master;
GO

DROP DATABASE IF EXISTS LabFerreteria;
GO

DROP LOGIN IF EXISTS usrFerreteria;
GO

USE master;
GO

ALTER DATABASE LabFerreteria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE LabFerreteria;
GO
IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'usrFerreteria')
    DROP LOGIN usrFerreteria;
GO
