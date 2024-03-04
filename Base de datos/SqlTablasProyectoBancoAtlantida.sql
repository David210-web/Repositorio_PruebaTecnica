--Base de datos y tablas
CREATE DATABASE ProyectoBancoAtlantida
GO

USE ProyectoBancoAtlantida
go

--Creacion tablas

-- Tabla TarjetaCredito
CREATE TABLE TarjetaCredito (
    IDTarjetaCredito INT PRIMARY KEY IDENTITY(1,1),
    NombreTitular NVARCHAR(100),
    NumeroTarjeta NVARCHAR(16),
    SaldoActual DECIMAL(10, 2),
    LimiteCredito DECIMAL(10, 2),
    SaldoDisponible DECIMAL(10, 2)
);
GO

-- Tabla Compra
CREATE TABLE Compra (
    IDCompra INT PRIMARY KEY IDENTITY(1,1),
    IDTarjetaCredito INT,
    FechaCompra DATE,
    Descripcion NVARCHAR(255),
    Monto DECIMAL(10, 2),
    FOREIGN KEY (IDTarjetaCredito) REFERENCES TarjetaCredito(IDTarjetaCredito)
);
GO

-- Tabla Pago
CREATE TABLE Pago (
    IDPago INT PRIMARY KEY IDENTITY(1,1),
    IDTarjetaCredito INT,
    FechaPago DATE,
    Monto DECIMAL(10, 2),
    FOREIGN KEY (IDTarjetaCredito) REFERENCES TarjetaCredito(IDTarjetaCredito)
);
GO

CREATE TABLE Configuracion (
    IDConfiguracion INT PRIMARY KEY identity(1,1),
    PorcentajeInteresConfigurable DECIMAL(5, 2),
    PorcentajeConfigurableSaldoMinimo DECIMAL(5, 2)
);
GO
