--Procedimientos almacenados
CREATE PROCEDURE sp_GetEstadoCuenta--
    @IDTarjetaCredito INT
AS
BEGIN
    DECLARE @NombreTitular NVARCHAR(100)
    DECLARE @NumeroTarjeta NVARCHAR(16)
    DECLARE @SaldoActual DECIMAL(10, 2)
    DECLARE @LimiteCredito DECIMAL(10, 2)
    DECLARE @SaldoDisponible DECIMAL(10, 2)
    DECLARE @InteresBonificable DECIMAL(10, 2)
    DECLARE @CuotaMinima DECIMAL(10, 2)
    DECLARE @MontoTotalAPagar DECIMAL(10, 2)
    DECLARE @MontoTotalContadoIntereses DECIMAL(10, 2)

    SELECT
        @NombreTitular = NombreTitular,
        @NumeroTarjeta = NumeroTarjeta,
        @SaldoActual = SaldoActual,
        @LimiteCredito = LimiteCredito,
        @SaldoDisponible = SaldoDisponible
    FROM
        TarjetaCredito
    WHERE
        IDTarjetaCredito = @IDTarjetaCredito;

    SET @InteresBonificable = @SaldoActual * (SELECT TOP 1 PorcentajeInteresConfigurable FROM Configuracion);
    SET @CuotaMinima = @SaldoActual * (SELECT TOP 1 PorcentajeConfigurableSaldoMinimo FROM Configuracion);
    SET @MontoTotalAPagar = @SaldoActual * (SELECT TOP 1 SUM(Monto) FROM Compra WHERE IDTarjetaCredito = @IDTarjetaCredito);
    SET @MontoTotalContadoIntereses = @SaldoActual + @InteresBonificable;

    SELECT
        @NombreTitular AS 'Nombre Titular',
        @NumeroTarjeta AS 'Numero Tarjeta',
        @SaldoActual AS 'Saldo Actual',
        @LimiteCredito AS 'Limite Credito',
        @SaldoDisponible AS 'Saldo Disponible',
        @InteresBonificable AS 'Interes Bonificable',
        @CuotaMinima AS 'Cuota Minima',
        @MontoTotalAPagar AS 'Monto Total A Pagar',
        @MontoTotalContadoIntereses AS 'Monto Total Contado Intereses';
END

Exec sp_GetEstadoCuenta 1


CREATE PROCEDURE sp_GetComprasMes
    @IDTarjetaCredito INT
AS
BEGIN
    SELECT
        FechaCompra,
        Descripcion,
        Monto
    FROM
        Compra
    WHERE
        IDTarjetaCredito = @IDTarjetaCredito
        AND MONTH(FechaCompra) = MONTH(GETDATE());
END;


CREATE PROCEDURE sp_CalcularCuotaMinima
    @IDTarjetaCredito INT
AS
BEGIN
    DECLARE @SaldoTotal DECIMAL(10, 2)

    SELECT @SaldoTotal = SaldoActual FROM TarjetaCredito WHERE IDTarjetaCredito = @IDTarjetaCredito;

    SELECT @SaldoTotal * (SELECT PorcentajeConfigurableSaldoMinimo FROM Configuracion) AS CuotaMinima;
END;

CREATE PROCEDURE sp_ObtenerTodasTarjetasCredito--
AS
BEGIN
    SELECT *
    FROM TarjetaCredito;
END;

CREATE PROCEDURE sp_ObtenerTodasTarjetasCreditoPorId--
	@Id int
AS
BEGIN
    SELECT *
    FROM TarjetaCredito where IDTarjetaCredito = @Id;
END;


EXEC sp_GetEstadoCuenta 1

SELECT * FROM Compra

CREATE PROCEDURE Sp_ObtenerHistorialTransacciones
	@IDTarjetaCredito int
AS
BEGIN
	-- Seleccionar compras del mes actual
	SELECT
		CAST(C.IDCompra AS int) AS TransaccionId,  -- Convertir a 'int' si necesario
		C.IDTarjetaCredito,
		C.FechaCompra,
		C.Descripcion,
		C.Monto,
		'Compra' AS Tipo
	FROM
		Compra C
	WHERE
		C.IDTarjetaCredito = @IDTarjetaCredito
		AND MONTH(C.FechaCompra) = MONTH(GETDATE())  -- Filtrar por mes actual

	UNION ALL

	-- Seleccionar pagos del mes actual
	SELECT
		CAST(P.IDPago AS int) AS TransaccionId,  -- Convertir a 'int' si necesario
		P.IDTarjetaCredito,
		P.FechaPago,
		'Pago' AS Descripcion,
		P.Monto,
		'Pago' AS Tipo
	FROM
		Pago P
	WHERE
		P.IDTarjetaCredito = @IDTarjetaCredito
		AND MONTH(P.FechaPago) = MONTH(GETDATE())  -- Filtrar por mes actual

	ORDER BY
		FechaCompra DESC;
END

exec Sp_ObtenerHistorialTransacciones 1

SELECT * FROM Compra

CREATE PROCEDURE Sp_insertarPago
	@IdTarjetaCredito int,
	@FechaPago date,
	@Monto varchar(150)
AS
BEGIN
	INSERT INTO Pago
	(IDTarjetaCredito,FechaPago,Monto)
	VALUES
	(@IdTarjetaCredito,@FechaPago,@Monto)
END


CREATE PROCEDURE sp_InsertarCompra
@IdTarjetaCredito int,
@FechaCompra date,
@descripcion varchar(50),
@monto decimal
AS
BEGIN
	INSERT INTO Compra
	(IDTarjetaCredito,FechaCompra,Descripcion,Monto)
	VALUES
	(@IdTarjetaCredito,@FechaCompra,@descripcion,@monto)
END

SELECT * FROM Pago

