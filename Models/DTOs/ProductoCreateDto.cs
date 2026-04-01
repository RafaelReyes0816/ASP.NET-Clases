using System.ComponentModel.DataAnnotations;
namespace MiApiBackend.Models.DTOs;

public class ProductoCreateDto
{
    [Required(ErrorMessage = "El código es requerido")]
    [MaxLength(50)]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida")]
    [MaxLength(200)]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es requerido")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio debe estar entre 0.01 y 99999.99")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor a 0")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "La categoría es requerida")]
    public int IdCategoria { get; set; }
}