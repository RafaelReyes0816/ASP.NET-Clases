using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiApiBackend.Repositories;
using MiApiBackend.Models;

namespace MiApiBackend.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoseController : ControllerBase
    {
        private readonly IProductoRepository _repo;

        public ProductoseController(IProductoRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok (await _repo.GetAll());
        [HttpPost]
        public async Task<IActionResult> Post(Producto producto)
        {
            await _repo.Add(producto);
            return Ok(producto);
        }
    }
}