CREATE PROCEDURE Visitas_Create
    @NombreCliente NVARCHAR(MAX),
    @FechaVisita DATETIME2,
    @NombreVendedor NVARCHAR(MAX),
    @Notas NVARCHAR(500),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Visitas (NombreCliente, FechaVisita, NombreVendedor, Notas)
    VALUES (@NombreCliente, @FechaVisita, @NombreVendedor, @Notas);

    SET @Id = SCOPE_IDENTITY();
END;
GO