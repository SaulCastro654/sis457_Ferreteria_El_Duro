CREATE DATABASE LabFerreteria;
GO
USE master;
GO
CREATE LOGIN usrferreteria WITH PASSWORD = '123456';

USE LabFerreteria;
CREATE USER usrferreteria FOR LOGIN usrferreteria;
ALTER ROLE db_owner ADD MEMBER usrferreteria;


DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Categoria;
DROP TABLE IF EXISTS Marca;
DROP TABLE IF EXISTS Usuario;
GO

CREATE TABLE Usuario (
    id INT IDENTITY PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    clave NVARCHAR(200) NOT NULL,
);

CREATE TABLE Marca (
    id INT IDENTITY PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
);

CREATE TABLE Categoria (
    id INT IDENTITY PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
);

CREATE TABLE Cliente (
    id INT IDENTITY PRIMARY KEY,
    nombre NVARCHAR(150) NOT NULL,
    telefono NVARCHAR(30),
    direccion NVARCHAR(250),
);

CREATE TABLE Producto (
    id INT IDENTITY PRIMARY KEY,
    nombre NVARCHAR(200) NOT NULL,
    precio DECIMAL(12,2) NOT NULL,
    stock INT NOT NULL DEFAULT 0,
	cantidadMedida VARCHAR(5) NULL,
	fechaVencimiento DATE NULL,
    idMarca INT NULL,
    idCategoria INT NULL,
    CONSTRAINT FK_Producto_Marca FOREIGN KEY (idMarca) REFERENCES Marca(id),
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(id)
);

CREATE TABLE Venta (
    id INT IDENTITY PRIMARY KEY,
    idUsuario INT NOT NULL,
    idCliente INT NOT NULL,
    total DECIMAL(12,2) NOT NULL DEFAULT 0,
	tipoEntrega NVARCHAR(30) NOT NULL 
		CHECK (TipoEntrega IN ('A domicilio','Recoger en tienda')) DEFAULT 'Recoger en tienda',
    CONSTRAINT FK_Venta_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (idCliente) REFERENCES Cliente(id)
);

CREATE TABLE DetalleVenta (
    id INT IDENTITY PRIMARY KEY,
    idVenta INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(12,2) NOT NULL,
    subtotal AS (cantidad * precioUnitario) PERSISTED,
    CONSTRAINT FK_DetalleVenta_Venta FOREIGN KEY (idVenta) REFERENCES Venta(id),
    CONSTRAINT FK_DetalleVenta_Producto FOREIGN KEY (idProducto) REFERENCES Producto(id)
);

ALTER TABLE Usuario ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuario ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuario ADD estado SMALLINT NOT NULL DEFAULT 1;

ALTER TABLE Marca ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Marca ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Marca ADD estado SMALLINT NOT NULL DEFAULT 1;

ALTER TABLE Categoria ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Categoria ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Categoria ADD estado SMALLINT NOT NULL DEFAULT 1;

ALTER TABLE Cliente ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Cliente ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Cliente ADD estado SMALLINT NOT NULL DEFAULT 1;

ALTER TABLE Producto ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Producto ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Producto ADD estado SMALLINT NOT NULL DEFAULT 1;

ALTER TABLE Venta ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Venta ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Venta ADD estado SMALLINT NOT NULL DEFAULT 1;

ALTER TABLE DetalleVenta ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE DetalleVenta ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE DetalleVenta ADD estado SMALLINT NOT NULL DEFAULT 1;
GO


INSERT INTO Usuario (nombre, clave)
VALUES ('Saul', 'dAFoRWBCRBpcRyECjAsQqw==');--Clave: 4321

INSERT INTO Marca (nombre) VALUES ('Tramontina'),('Bosch'),('Makita'),('Sin Marca');
INSERT INTO Categoria (nombre) VALUES ('Herramientas'),('Eléctricos'),('Adhesivos y Selladores');

INSERT INTO Producto (nombre, precio, stock, cantidadMedida, fechaVencimiento, idMarca, idCategoria)
VALUES 
('Martillo de acero', 35.50, 20, NULL, NULL, 1, 1),
('Destornillador plano', 15.00, 50, NULL, NULL, 2, 1),
('Llave inglesa ajustable', 28.90, 25, NULL, NULL, 3, 1),
('Serrucho de mano', 22.00, 18, NULL, NULL, 4, 1),
('Cinta métrica 5 m', 12.50, 40, NULL, NULL, 1, 1),

('Taladro eléctrico 500W', 450.00, 10, NULL, NULL, 3, 2),
('Lijadora orbital', 220.00, 5, NULL, NULL, 2, 2),
('Sierra circular', 380.00, 7, NULL, NULL, 1, 2),
('Atornillador inalámbrico', 160.00, 15, NULL, NULL, 4, 2),
('Amoladora angular', 200.00, 8, NULL, NULL, 3, 2),

('Silicona industrial', 25.00, 30, '300 g', '2026-12-31', 1, 3),
('Poxipol adhesivo', 18.50, 50, '50 g', '2025-06-30', 2, 3),
('Pegamento instantáneo', 12.00, 40, '20 g', '2025-09-30', 3, 3),
('Sellador acrílico', 30.00, 25, '280 g', '2026-03-31', 4, 3),
('Silicona neutra', 28.00, 20, '250 g', '2026-05-31', 1, 3),
('Pegamento en tubo', 10.00, 60, '50 g', '2025-12-31', 2, 3),
('Resina poliéster', 35.00, 15, '500 g', '2026-06-30', 3, 3);

INSERT INTO Cliente (nombre, telefono, direccion)
VALUES ('Dario Lopez','74544429','Av. Bolivar'),
       ('Justo Cruz','72857449','Calle Loa');
GO

SELECT * FROM Usuario;
SELECT * FROM Cliente;
SELECT * FROM Producto;
SELECT * FROM Marca;
SELECT * FROM Categoria;
SELECT * FROM DetalleVenta;
GO
