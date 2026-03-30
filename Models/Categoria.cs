using System.ComponentModel.DataAnnotations;

namespace MiApiBackend.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
}