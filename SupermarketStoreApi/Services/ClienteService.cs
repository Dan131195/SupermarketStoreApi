using Microsoft.AspNetCore.Identity;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Cliente;
using SupermarketStoreApi.Models.Auth;
using SupermarketStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SupermarketStoreApi.Services
{
    public class ClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ClienteService> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ClienteResponseDto?> CreateAsync(ClienteCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return null;

            var clienteEsistente = await _context.Clienti.FirstOrDefaultAsync(c => c.UserId == dto.UserId);
            if (clienteEsistente != null) return null;

            var cliente = new Cliente
            {
                ClienteId = Guid.NewGuid(),
                CodiceFiscale = dto.CodiceFiscale,
                UserId = dto.UserId
            };

            _context.Clienti.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteResponseDto
            {
                ClienteId = cliente.ClienteId,
                CodiceFiscale = cliente.CodiceFiscale,
                UserId = cliente.UserId,
                Email = user.Email,
                NomeCompleto = $"{user.FirstName} {user.LastName}"
            };
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            return await _context.Clienti.Include(c => c.User).FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<List<ClienteResponseDto>> GetAllAsync()
        {
            return await _context.Clienti
                .Include(c => c.User)
                .Select(c => new ClienteResponseDto
                {
                    ClienteId = c.ClienteId,
                    CodiceFiscale = c.CodiceFiscale,
                    UserId = c.UserId,
                    Email = c.User.Email,
                    NomeCompleto = $"{c.User.FirstName} {c.User.LastName}"
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null) return false;

            cliente.CodiceFiscale = dto.CodiceFiscale;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null) return false;

            _context.Clienti.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
