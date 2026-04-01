namespace MiApiBackend.Models.DTOs;

public class DetallePedidoDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Subtotal { get; set; }
}

public class PedidoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal Total { get; set; }
    public List<DetallePedidoDto> Detalles { get; set; } = new();
}

public class PedidoCreateDto
{
    public int ClienteId { get; set; }
    public List<DetallePedidoDto> Detalles { get; set; } = new();
}
