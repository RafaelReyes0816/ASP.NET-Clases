using MiApiBackend.Models;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Repositories;

namespace MiApiBackend.Services;

public interface IPedidoService
{
    Task<List<PedidoDto>> GetAllAsync();
    Task<PedidoDto?> GetByIdAsync(int id);
    Task<PedidoDto> CreateAsync(PedidoCreateDto dto);
}

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repo;

    public PedidoService(IPedidoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<PedidoDto>> GetAllAsync()
    {
        var pedidos = await _repo.GetAllAsync();
        return pedidos.Select(MapToDto).ToList();
    }

    public async Task<PedidoDto?> GetByIdAsync(int id)
    {
        var pedido = await _repo.GetByIdAsync(id);
        return pedido == null ? null : MapToDto(pedido);
    }

    public async Task<PedidoDto> CreateAsync(PedidoCreateDto dto)
    {
        var pedido = new Pedido
        {
            ClienteId = dto.ClienteId,
            Total = dto.Detalles.Sum(d => d.Subtotal),
            Detalles = dto.Detalles.Select(d => new DetallePedido
            {
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                Subtotal = d.Subtotal
            }).ToList()
        };

        await _repo.AddAsync(pedido);
        await _repo.SaveChangesAsync();
        return MapToDto(pedido);
    }

    private static PedidoDto MapToDto(Pedido p) => new()
    {
        Id = p.Id,
        ClienteId = p.ClienteId,
        Total = p.Total,
        Detalles = p.Detalles.Select(d => new DetallePedidoDto
        {
            ProductoId = d.ProductoId,
            Cantidad = d.Cantidad,
            Subtotal = d.Subtotal
        }).ToList()
    };
}
