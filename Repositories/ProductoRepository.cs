using MiApiBackend.Data;
using MiApiBackend.Models;
using MiApiBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MiApiBackend.Repositories;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAll();
    Task<List<Producto>> GetAllAsync();
    Task Add(Producto producto);
    Task<Producto?> GetByIdAsync(int id);
    Task<bool> ExisteCodigoAsync(string codigo, int? excludeId = null);
    Task AddAsync(Producto producto);
    Task SaveChangesAsync();
    Task ActualizarAsync(int id, ProductoCreateDto dto);
}

public class ProductoRepository : IProductoRepository
{
    private readonly DBContext _context;

    public ProductoRepository(DBContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Producto>> GetAll() =>
        await GetAllAsync();

    public Task<List<Producto>> GetAllAsync() =>
        _context.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .OrderBy(p => p.Id)
            .ToListAsync();

    public async Task Add(Producto producto)
    {
        await AddAsync(producto);
        await SaveChangesAsync();
    }

    public Task<Producto?> GetByIdAsync(int id) =>
        _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

    public Task<bool> ExisteCodigoAsync(string codigo, int? excludeId = null) =>
        _context.Productos.AnyAsync(p =>
            p.codigo == codigo && (excludeId == null || p.Id != excludeId));

    public async Task AddAsync(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public async Task ActualizarAsync(int id, ProductoCreateDto dto)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (producto is null)
            return;

        producto.codigo = dto.Codigo;
        producto.descripcion = dto.Descripcion;
        producto.precio = (int)Math.Round(dto.Precio, MidpointRounding.AwayFromZero);
        producto.stock = dto.Stock;
        producto.IdCategoria = dto.IdCategoria;

        await SaveChangesAsync();
    }
}
