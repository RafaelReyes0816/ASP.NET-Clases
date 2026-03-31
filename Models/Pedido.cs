using System.ComponentModel.DataAnnotations;

namespace MiApiBackend.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal Total { get; set; }
    public List<DetallePedido> Detalles { get; set; } = new();
}