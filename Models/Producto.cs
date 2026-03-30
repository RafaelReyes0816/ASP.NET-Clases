using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiApiBackend.Models;

public class Producto
{
    [Key]
    public int Id { get; set; }
    public string codigo { get; set; } = string.Empty;
    [Required(ErrorMessage = "El codigo es requerido")]
    public string? descripcion { get; set; }
    [Required(ErrorMessage = "La descripción es requerida")]
    public int precio { get; set; }
    [Required(ErrorMessage = "El precio es requerido")]
    [Range (1,100)]
    public int IdCategoria { get; set; }
    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }
}