using Microsoft.AspNetCore.Mvc;
using MiApiBackend.Data;
using MiApiBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApiBackend.Controllers;

[Route("api/productos")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly DBContext _context;

    public ProductosController(DBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Productos.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Post(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return Ok(producto);
    }
}
