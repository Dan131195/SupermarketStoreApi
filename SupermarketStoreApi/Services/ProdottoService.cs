using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Prodotto;
using SupermarketStoreApi.Models;

namespace SupermarketStoreApi.Services
{
    public class ProdottoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProdottoService> _logger;

        public ProdottoService(ApplicationDbContext context, ILogger<ProdottoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProdottoDto>> GetAllAsync()
        {
            try
            {
                return await _context.Prodotti
                    .Include(p => p.Categoria)
                    .Select(p => new ProdottoDto
                    {
                        ProdottoId = p.ProdottoId,
                        NomeProdotto = p.NomeProdotto,
                        ImmagineFile = p.ImmagineProdotto,
                        DescrizioneProdotto = p.DescrizioneProdotto,
                        PrezzoProdotto = p.PrezzoProdotto,
                        Stock = p.Stock,
                        CategoriaId = p.CategoriaId,
                        CategoriaNome = p.Categoria.NomeCategoria
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei prodotti");
                throw;
            }
        }

        public async Task<ProdottoDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var prodotto = await _context.Prodotti
                    .Include(p => p.Categoria)
                    .FirstOrDefaultAsync(p => p.ProdottoId == id);

                if (prodotto == null) return null;

                return new ProdottoDto
                {
                    ProdottoId = prodotto.ProdottoId,
                    NomeProdotto = prodotto.NomeProdotto,
                    ImmagineFile = prodotto.ImmagineProdotto,
                    DescrizioneProdotto = prodotto.DescrizioneProdotto,
                    PrezzoProdotto = prodotto.PrezzoProdotto,
                    Stock = prodotto.Stock,
                    CategoriaId = prodotto.CategoriaId,
                    CategoriaNome = prodotto.Categoria.NomeCategoria
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante il recupero del prodotto con ID {id}");
                throw;
            }
        }

        public async Task<Prodotto> CreateAsync(ProdottoCreateDto dto)
        {
            try
            {
                var categoria = await _context.Categorie
                    .FirstOrDefaultAsync(c => c.NomeCategoria.ToLower() == dto.NomeCategoria.ToLower());

                if (categoria == null)
                {
                    categoria = new Categoria { NomeCategoria = dto.NomeCategoria };
                    _context.Categorie.Add(categoria);
                    await _context.SaveChangesAsync();
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "prodotti");
                Directory.CreateDirectory(uploadsFolder); 

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImmagineFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImmagineFile.CopyToAsync(stream);
                }

                var relativePath = $"/uploads/prodotti/{fileName}";

                var prodotto = new Prodotto
                {
                    ProdottoId = Guid.NewGuid(),
                    NomeProdotto = dto.NomeProdotto,
                    ImmagineProdotto = relativePath, 
                    DescrizioneProdotto = dto.DescrizioneProdotto,
                    PrezzoProdotto = dto.PrezzoProdotto,
                    Stock = dto.Stock,
                    CategoriaId = categoria.CategoriaId
                };

                _context.Prodotti.Add(prodotto);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Prodotto creato con ID {prodotto.ProdottoId}");
                return prodotto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del prodotto");
                throw;
            }
        }


        public async Task<bool> UpdateAsync(Guid id, ProdottoUpdateDto dto, IWebHostEnvironment env)
        {
            try
            {
                var prodotto = await _context.Prodotti.FindAsync(id);
                if (prodotto == null)
                {
                    _logger.LogWarning($"Prodotto con ID {id} non trovato per l'aggiornamento");
                    return false;
                }

                var categoria = await _context.Categorie
                    .FirstOrDefaultAsync(c => c.NomeCategoria.ToLower() == dto.NomeCategoria.ToLower());

                if (categoria == null)
                {
                    categoria = new Categoria { NomeCategoria = dto.NomeCategoria };
                    _context.Categorie.Add(categoria);
                    await _context.SaveChangesAsync();
                }

                if (dto.ImmagineFile != null)
                {
                    var uploadsFolder = Path.Combine(env.WebRootPath, "uploads", "prodotti");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImmagineFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.ImmagineFile.CopyToAsync(stream); 
                    }

                    prodotto.ImmagineProdotto = $"/uploads/prodotti/{fileName}";
                }

                prodotto.NomeProdotto = dto.NomeProdotto;
                prodotto.DescrizioneProdotto = dto.DescrizioneProdotto;
                prodotto.PrezzoProdotto = dto.PrezzoProdotto;
                prodotto.Stock = dto.Stock;
                prodotto.CategoriaId = categoria.CategoriaId;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Prodotto con ID {id} aggiornato");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'aggiornamento del prodotto con ID {id}");
                throw;
            }
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var prodotto = await _context.Prodotti.FindAsync(id);
                if (prodotto == null)
                {
                    _logger.LogWarning($"Prodotto con ID {id} non trovato per l'eliminazione");
                    return false;
                }

                _context.Prodotti.Remove(prodotto);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Prodotto con ID {id} eliminato");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'eliminazione del prodotto con ID {id}");
                throw;
            }
        }
    }

}
