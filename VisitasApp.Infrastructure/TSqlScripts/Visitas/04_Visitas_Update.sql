CREATE PROCEDURE Visitas_Update
    @Id INT,
    @NombreCliente NVARCHAR(MAX),
    @FechaVisita DATETIME2,
    @NombreVendedor NVARCHAR(MAX),
    @Notas NVARCHAR(500)
AS
BEGIN
    UPDATE Visitas
    SET NombreCliente = @NombreCliente,
        FechaVisita = @FechaVisita,
        NombreVendedor = @NombreVendedor,
        Notas = @Notas
    WHERE Id = @Id;
END;
GO