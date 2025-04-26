using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Categoria;
using SupermarketStoreApi.Models;

namespace SupermarketStoreApi.Services
{
    public class CategoriaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ApplicationDbContext context, ILogger<CategoriaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CategoriaDto>> GetAllAsync()
        {
            try
            {
                return await _context.Categorie
                    .Select(c => new CategoriaDto
                    {
                        CategoriaId = c.CategoriaId,
                        NomeCategoria = c.NomeCategoria
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero delle categorie");
                throw;
            }
        }

        public async Task<CategoriaDto?> GetByIdAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorie.FindAsync(id);
                if (categoria == null) return null;

                return new CategoriaDto
                {
                    CategoriaId = categoria.CategoriaId,
                    NomeCategoria = categoria.NomeCategoria
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero della categoria con ID {Id}", id);
                throw;
            }
        }

        public async Task<Categoria> CreateAsync(CategoriaCreateDto dto)
        {
            try
            {
                var categoria = new Categoria
                {
                    NomeCategoria = dto.NomeCategoria
                };

                _context.Categorie.Add(categoria);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Categoria creata con ID {Id}", categoria.CategoriaId);

                return categoria;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della categoria");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, CategoriaUpdateDto dto)
        {
            try
            {
                var categoria = await _context.Categorie.FindAsync(id);
                if (categoria == null) return false;

                categoria.NomeCategoria = dto.NomeCategoria;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Categoria con ID {Id} aggiornata", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della categoria con ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorie.FindAsync(id);
                if (categoria == null) return false;

                _context.Categorie.Remove(categoria);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Categoria con ID {Id} eliminata", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della categoria con ID {Id}", id);
                throw;
            }
        }
    }

}
