using Microsoft.AspNetCore.Mvc;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Services;

namespace MiApiBackend.Controllers;

[Route("api/pedidos")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _service;

    public PedidosController(IPedidoService service)
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
    public async Task<IActionResult> Post(PedidoCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}
