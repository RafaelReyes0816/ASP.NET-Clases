using Microsoft.EntityFrameworkCore;
using MiApiBackend.Data;
using MiApiBackend.Models;

namespace MiApiBackend.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DBContext _context;

    public RefreshTokenRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task CreateAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string token)
    {
        var rt = await GetByTokenAsync(token);
        if (rt != null)
        {
            _context.RefreshTokens.Remove(rt);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAllByUserIdAsync(Guid userId)
    {
        var tokens = await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }
}
