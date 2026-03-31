using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiBackend.Data;
using MiApiBackend.Models;


namespace MiApiBackend.Controllers;

[Route("api/categorias")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly DBContext _context;

    public CategoriaController(DBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Categorias.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Post(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }
}