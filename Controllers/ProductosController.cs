using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Services;

namespace MiApiBackend.Controllers;

[Route("api/productos")]
[ApiController]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var p = await _service.GetByIdAsync(id);
        return p == null ? NotFound() : Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductoCreateDto dto)
    {
        var productoCreado = await _service.CreateAsync(dto);
        return Ok(productoCreado);
    }
}
