namespace MiApiBackend.Models.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class CategoriaCreateDto
{
    public string Nombre { get; set; } = string.Empty;
}
