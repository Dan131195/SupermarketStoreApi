using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Ordine;
using SupermarketStoreApi.Models;

namespace SupermarketStoreApi.Services
{
    public class OrdineService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdineService> _logger;

        public OrdineService(ApplicationDbContext context, ILogger<OrdineService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> ConfermaOrdineAsync(string userId, DateTime oraRitiro)
        {
            var carrello = await _context.ProdottiCarrello
                .Include(pc => pc.Prodotto)
                .Where(pc => pc.UserId == userId)
                .ToListAsync();

            if (!carrello.Any()) return false;

            var now = DateTime.UtcNow;
            if (oraRitiro < now.AddHours(1))
            {
                return false;
            }

            var ordine = new Ordine
            {
                OrdineId = Guid.NewGuid(),
                UserId = userId,
                DataOrdine = now,
                OraRitiro = oraRitiro, 
                StatoOrdineId = 1,
                Totale = carrello.Sum(c => c.Quantita * c.Prodotto.PrezzoProdotto),
                ProdottiOrdine = carrello.Select(c => new ProdottoOrdine
                {
                    ProdottoId = c.ProdottoId,
                    Quantita = c.Quantita,
                    PrezzoUnitario = c.Prodotto.PrezzoProdotto
                }).ToList()
            };

            _context.Ordini.Add(ordine);
            _context.ProdottiCarrello.RemoveRange(carrello);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrdineDto>> GetStoricoAsync(string userId)
        {
            return await _context.Ordini
                .Include(o => o.ProdottiOrdine).ThenInclude(po => po.Prodotto)
                .Include(o => o.StatoOrdine)
                .Where(o => o.UserId == userId)
                .Select(o => new OrdineDto
                {
                    OrdineId = o.OrdineId,
                    UserId = o.UserId,
                    DataOrdine = o.DataOrdine,
                    Totale = o.Totale,
                    Stato = o.StatoOrdine.Nome,
                    Prodotti = o.ProdottiOrdine.Select(po => new OrdineDettaglioDto
                    {
                        NomeProdotto = po.Prodotto.NomeProdotto,
                        Quantita = po.Quantita,
                        PrezzoUnitario = po.PrezzoUnitario
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrdineDto?> GetDettagliOrdineAsync(Guid ordineId)
        {
            return await _context.Ordini
                .Include(o => o.ProdottiOrdine).ThenInclude(po => po.Prodotto)
                .Include(o => o.StatoOrdine)
                .Where(o => o.OrdineId == ordineId)
                .Select(o => new OrdineDto
                {
                    OrdineId = o.OrdineId,
                    UserId = o.UserId,
                    DataOrdine = o.DataOrdine,
                    Totale = o.Totale,
                    Stato = o.StatoOrdine.Nome,
                    Prodotti = o.ProdottiOrdine.Select(po => new OrdineDettaglioDto
                    {
                        NomeProdotto = po.Prodotto.NomeProdotto,
                        Quantita = po.Quantita,
                        PrezzoUnitario = po.PrezzoUnitario
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CambiaStatoAsync(Guid ordineId, int nuovoStatoId)
        {
            var ordine = await _context.Ordini.FindAsync(ordineId);
            if (ordine == null) return false;

            ordine.StatoOrdineId = nuovoStatoId;
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
