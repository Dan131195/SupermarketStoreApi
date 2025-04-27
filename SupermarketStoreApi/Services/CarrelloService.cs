using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.Models;
using SupermarketStoreApi.DTOs.Carrello;
using Microsoft.AspNetCore.Authorization;

namespace SupermarketStoreApi.Services
{
    public class CarrelloService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarrelloService> _logger;

        public CarrelloService(ApplicationDbContext context, ILogger<CarrelloService> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        public async Task<List<CarrelloItemDto>> GetByUserIdAsync(string userId)
        {
            return await _context.ProdottiCarrello
                .Include(pc => pc.Prodotto)
                .Where(pc => pc.UserId == userId)
                .Select(pc => new CarrelloItemDto
                {
                    ProdottoCarrelloId = pc.ProdottoCarrelloId,
                    UserId = pc.UserId,
                    ProdottoId = pc.ProdottoId,
                    NomeProdotto = pc.Prodotto.NomeProdotto,
                    Quantita = pc.Quantita,
                    PrezzoUnitario = pc.Prodotto.PrezzoProdotto,
                    ImmagineFile = pc.Prodotto.ImmagineProdotto
                })
                .ToListAsync();
        }

        public async Task<bool> AggiungiAsync(AggiungiAlCarrelloDto dto)
        {
            var prodotto = await _context.Prodotti
                .FirstOrDefaultAsync(p => p.ProdottoId == dto.ProdottoId);

            if (prodotto == null)
                throw new Exception("Prodotto non trovato.");

            if (prodotto.Stock < dto.Quantita)
                throw new Exception("Stock insufficiente per aggiungere al carrello.");

            var esistente = await _context.ProdottiCarrello
                .FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.ProdottoId == dto.ProdottoId);

            if (esistente != null)
            {
                esistente.Quantita += dto.Quantita;
            }
            else
            {
                var nuovo = new ProdottoCarrello
                {
                    ProdottoCarrelloId = Guid.NewGuid(),
                    UserId = dto.UserId,
                    ProdottoId = dto.ProdottoId,
                    Quantita = dto.Quantita
                };
                _context.ProdottiCarrello.Add(nuovo);
            }

            prodotto.Stock -= dto.Quantita;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ModificaQuantitaAsync(Guid id, int nuovaQuantita)
        {
            var item = await _context.ProdottiCarrello.FindAsync(id);
            if (item == null) return false;

            var prodotto = await _context.Prodotti.FirstOrDefaultAsync(p => p.ProdottoId == item.ProdottoId);
            if (prodotto == null) return false;

            var differenza = nuovaQuantita - item.Quantita;

            if (differenza > 0)
            {
                if (prodotto.Stock < differenza)
                    throw new Exception("Stock insufficiente");

                prodotto.Stock -= differenza;
            }
            else if (differenza < 0)
            {
                prodotto.Stock += Math.Abs(differenza);
            }

            item.Quantita = nuovaQuantita;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RimuoviAsync(Guid id)
        {
            var item = await _context.ProdottiCarrello.FindAsync(id);
            if (item == null) return false;

            var prodotto = await _context.Prodotti.FirstOrDefaultAsync(p => p.ProdottoId == item.ProdottoId);
            if (prodotto != null)
            {
                prodotto.Stock += item.Quantita; 
            }

            _context.ProdottiCarrello.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<int> SvuotaCarrelloAsync(string userId)
        {
            var items = await _context.ProdottiCarrello
                .Where(c => c.UserId == userId)
                .ToListAsync();

            foreach (var item in items)
            {
                var prodotto = await _context.Prodotti.FirstOrDefaultAsync(p => p.ProdottoId == item.ProdottoId);
                if (prodotto != null)
                {
                    prodotto.Stock += item.Quantita;
                }
            }

            _context.ProdottiCarrello.RemoveRange(items);
            return await _context.SaveChangesAsync();
        }

    }
}
