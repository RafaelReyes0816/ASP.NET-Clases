using MiApiBackend.Models;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Repositories;

namespace MiApiBackend.Services;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetAllAsync();
    Task<CategoriaDto?> GetByIdAsync(int id);
    Task<CategoriaDto> CreateAsync(CategoriaCreateDto dto);
}

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repo;

    public CategoriaService(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CategoriaDto>> GetAllAsync()
    {
        var categorias = await _repo.GetAllAsync();
        return categorias.Select(c => new CategoriaDto { Id = c.Id, Nombre = c.Nombre }).ToList();
    }

    public async Task<CategoriaDto?> GetByIdAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        return c == null ? null : new CategoriaDto { Id = c.Id, Nombre = c.Nombre };
    }

    public async Task<CategoriaDto> CreateAsync(CategoriaCreateDto dto)
    {
        var categoria = new Categoria { Nombre = dto.Nombre };
        await _repo.AddAsync(categoria);
        await _repo.SaveChangesAsync();
        return new CategoriaDto { Id = categoria.Id, Nombre = categoria.Nombre };
    }
}
