using MiApiBackend.Models;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Repositories;

namespace MiApiBackend.Services;

public interface IProductoService
{
    Task<List<ProductoCreateDto>> GetAllAsync();
    Task<ProductoCreateDto?> GetByIdAsync(int id);
    Task<ProductoCreateDto> CreateAsync(ProductoCreateDto dto);
}

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repo;

    public ProductoService(IProductoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProductoCreateDto>> GetAllAsync()
    {
        var productos = await _repo.GetAllAsync();
        return productos.Select(MapToDto).ToList();
    }

    public async Task<ProductoCreateDto?> GetByIdAsync(int id)
    {
        var producto = await _repo.GetByIdAsync(id);
        return producto == null ? null : MapToDto(producto);
    }

    public async Task<ProductoCreateDto> CreateAsync(ProductoCreateDto dto)
    {
        var producto = new Producto
        {
            codigo = dto.Codigo,
            descripcion = dto.Descripcion,
            precio = (int)Math.Round(dto.Precio, MidpointRounding.AwayFromZero),
            stock = dto.Stock,
            IdCategoria = dto.IdCategoria
        };

        await _repo.AddAsync(producto);
        await _repo.SaveChangesAsync();
        return MapToDto(producto);
    }

    private static ProductoCreateDto MapToDto(Producto p) => new()
    {
        Codigo = p.codigo,
        Descripcion = p.descripcion ?? string.Empty,
        Precio = p.precio,
        Stock = p.stock,
        IdCategoria = p.IdCategoria
    };
}