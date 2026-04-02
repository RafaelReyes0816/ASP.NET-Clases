using Microsoft.EntityFrameworkCore;
using MiApiBackend.Data;
using MiApiBackend.Models;

namespace MiApiBackend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DBContext _context;

    public UserRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
