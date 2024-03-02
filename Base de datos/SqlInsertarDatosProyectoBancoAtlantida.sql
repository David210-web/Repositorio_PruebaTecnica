--Insertando datos

INSERT INTO TarjetaCredito (NombreTitular, NumeroTarjeta, SaldoActual, LimiteCredito, SaldoDisponible)
VALUES
    ('Ana Garcia', '4111111111111111', 114.47, 1000.00, 885.53),
    ('Carlos Rodriguez', '5555444433332222', 250.00, 1500.00, 1250.00),
    ('Laura Martinez', '378282246310005', 800.00, 2000.00, 1200.00),
    ('Juan Hernandez', '4111111111111111', 1500.00, 3000.00, 1500.00),
    ('Sofia Gomez', '5555666677778888', 1200.00, 5000.00, 3800.00),
    ('Diego Sanchez', '4111111111111111', 700.00, 1000.00, 300.00),
    ('Maria Torres', '4111111111111111', 3000.00, 5000.00, 2000.00),
    ('Eduardo Vargas', '4111111111111111', 450.00, 800.00, 350.00),
    ('Camila Ruiz', '4111111111111111', 2000.00, 3000.00, 1000.00),
    ('Roberto Mendoza', '4111111111111111', 1800.00, 2000.00, 200.00);


INSERT INTO Compra (IDTarjetaCredito, FechaCompra, Descripcion, Monto)
VALUES
    (1, '2024-02-01', 'Supermercado', 20.00),
    (1, '2024-02-05', 'Restaurante', 30.47),
    (2, '2024-02-10', 'Electrodomésticos', 150.00),
    (3, '2024-02-15', 'Ropa', 80.00),
    (4, '2024-02-20', 'Gasolina', 50.00),
    (5, '2024-02-25', 'Libros', 20.00),
    (6, '2024-02-28', 'Electrónicos', 100.00);


INSERT INTO Pago (IDTarjetaCredito, FechaPago, Monto)
VALUES
    (1, '2024-02-08', 50.00),
    (2, '2024-02-18', 75.00),
    (3, '2024-02-22', 30.00),
    (4, '2024-02-27', 100.00),
    (5, '2024-02-10', 30.00),
    (6, '2024-02-15', 50.00),
    (7, '2024-02-28', 120.00);

INSERT INTO Configuracion (PorcentajeInteresConfigurable, PorcentajeConfigurableSaldoMinimo)
VALUES
    ( 25.00, 5.00),
    (20.00, 4.00),
    (30.00, 6.00),
    (22.50, 4.50),
    (18.75, 3.75),
    (27.50, 5.50),
    (21.25, 4.25);