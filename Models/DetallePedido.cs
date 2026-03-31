using System.ComponentModel.DataAnnotations;

namespace MiApiBackend.Models;

public class DetallePedido
{
    [Key]
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Subtotal { get; set; }
    public Pedido Pedido { get; set; } = null!;
    public Producto Producto { get; set; } = null!;
}