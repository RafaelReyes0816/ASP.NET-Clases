using MiApiBackend.Data;
using MiApiBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApiBackend.Repositories;

public interface ICategoriaRepository
{
    Task<List<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task AddAsync(Categoria categoria);
    Task SaveChangesAsync();
}

public class CategoriaRepository : ICategoriaRepository
{
    private readonly DBContext _context;

    public CategoriaRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task AddAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
