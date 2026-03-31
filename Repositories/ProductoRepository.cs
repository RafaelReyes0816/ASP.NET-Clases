using MiApiBackend.Data;
using MiApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MiApiBackend.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAll(); //Listar
        Task Add(Producto producto); //Crear
    }

    public class ProductoRepository:IProductoRepository
    {
        private readonly DBContext _context;

        public ProductoRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Producto>> GetAll()
        => await _context.Productos.ToListAsync();

        public async Task Add(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }
    }
}