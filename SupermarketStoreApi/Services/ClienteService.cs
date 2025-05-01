using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Cliente;
using SupermarketStoreApi.Models.Auth;
using SupermarketStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Migrations;
using Cliente = SupermarketStoreApi.Models.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace SupermarketStoreApi.Services
{
    public class ClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClienteService> _logger;
        private readonly IWebHostEnvironment _environment;

        public ClienteService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ClienteService> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _environment = environment;
        }

        private async Task<string?> SaveImageAsync(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "clienti");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("uploads", "clienti", uniqueFileName).Replace("\\", "/");
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
                Nome = user.FirstName,
                Cognome = user.LastName,

                ImmagineProfilo = cliente.ImmagineProfilo
            };
        }

        public async Task<ClienteResponseDto?> GetByIdAsync(string userId)
        {
            var cliente = await _context.Clienti
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cliente == null) return null;

            return new ClienteResponseDto
            {
                ClienteId = cliente.ClienteId,
                CodiceFiscale = cliente.CodiceFiscale,
                UserId = cliente.UserId,
                Email = cliente.User.Email,
                Nome = cliente.User.FirstName,
                Cognome = cliente.User.LastName,
                ImmagineProfilo = cliente.ImmagineProfilo
            };
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
                    Nome = c.User.FirstName,
                    Cognome = c.User.LastName,
                    Indirizzo = c.Domicilio,
                    ImmagineProfilo = c.ImmagineProfilo
                })
                .ToListAsync();
        }

        
        public async Task<bool> UpdateAsync(Guid id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clienti.Include(c => c.User).FirstOrDefaultAsync(c => c.ClienteId == id);
            if (cliente == null) return false;

            cliente.CodiceFiscale = dto.CodiceFiscale;
            cliente.Domicilio = dto.Indirizzo;

            cliente.User.FirstName = dto.Nome;
            cliente.User.LastName = dto.Cognome;
            cliente.User.Email = dto.Email;
            cliente.User.UserName = dto.Email;

            await _userManager.UpdateAsync(cliente.User);
            await _context.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> UpdateImmagineProfiloAsync(Guid id, IFormFile immagineFile)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null) return false;

            var imagePath = await SaveImageAsync(immagineFile);
            cliente.ImmagineProfilo = imagePath;

            await _context.SaveChangesAsync();
            return true;
        }

        // PATCH - Aggiorna solo l'indirizzo
        public async Task<bool> UpdateIndirizzoAsync(Guid id, string nuovoIndirizzo)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null) return false;

            cliente.Domicilio = nuovoIndirizzo;
            await _context.SaveChangesAsync();
            return true;
        }

        // PATCH - Aggiorna solo l'email (anche su Identity)
        public async Task<bool> UpdateEmailAsync(Guid id, string nuovaEmail)
        {
            var cliente = await _context.Clienti.Include(c => c.User).FirstOrDefaultAsync(c => c.ClienteId == id);
            if (cliente == null) return false;

            cliente.User.Email = nuovaEmail;
            cliente.User.UserName = nuovaEmail;

            var result = await _userManager.UpdateAsync(cliente.User);
            if (!result.Succeeded) return false;

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
