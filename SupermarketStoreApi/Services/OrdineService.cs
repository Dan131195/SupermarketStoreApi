using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Ordine;
using SupermarketStoreApi.Models;
using SupermarketStoreApi.Models.Auth;

namespace SupermarketStoreApi.Services
{
    public class OrdineService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdineService> _logger;
        private readonly EmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public OrdineService(ApplicationDbContext context, ILogger<OrdineService> logger, UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<bool> ConfermaOrdineAsync(string userId, DateTime oraRitiro)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var carrello = await _context.ProdottiCarrello
                .Include(pc => pc.Prodotto)
                .Where(pc => pc.UserId == userId)
                .ToListAsync();

            if (!carrello.Any()) return false;

            var now = DateTime.UtcNow;
            if (oraRitiro < now.AddHours(1)) return false;

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

            var prodottiHtml = string.Join("", carrello.Select(c => $@"
                <tr>
                    <td style='padding: 8px; border: 1px solid #ccc;'>{c.Prodotto.NomeProdotto}</td>
                    <td style='padding: 8px; border: 1px solid #ccc; text-align: center;'>{c.Quantita}</td>
                    <td style='padding: 8px; border: 1px solid #ccc; text-align: right;'>€{c.Prodotto.PrezzoProdotto:F2}</td>
                </tr>
            "));

            var emailBody = $@"
            <div style='font-family: Arial, sans-serif; background-color: #fff3e0; padding: 20px;'>
                <h2 style='color: #e65100;'>Ordine Confermato</h2>
                <p>Ciao {user.FirstName},</p>
                <p>Grazie per il tuo ordine! 💚</p>

                <p>Il tuo ID ordine : <strong>{ordine.OrdineId}.</strong>

                <h4>Dettagli ordine:</h4>
                <table style='width: 100%; border-collapse: collapse; margin-bottom: 15px;'>
                    <thead>
                        <tr style='background-color: #ffe0b2;'>
                            <th style='padding: 8px; border: 1px solid #ccc; text-align: left;'>Prodotto</th>
                            <th style='padding: 8px; border: 1px solid #ccc;'>Quantità</th>
                            <th style='padding: 8px; border: 1px solid #ccc; text-align: right;'>Prezzo</th>
                        </tr>
                    </thead>
                    <tbody>
                        {prodottiHtml}
                    </tbody>
                </table>

                <p><strong>Totale:</strong> €{ordine.Totale:F2}</p>
                <p>Ti avviseremo quando sarà pronto per il ritiro.</p>
                <hr style='border: none; border-top: 1px solid #ccc;' />
                <p style='font-size: 14px; color: #666;'>SpeedMarket - Spesa facile, veloce e comoda</p>
            </div>";

            await _emailService.SendEmailAsync(user.Email, "Conferma Ordine", emailBody);

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
                        PrezzoUnitario = po.PrezzoUnitario,
                        ImmagineProdotto = po.Prodotto.ImmagineProdotto
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<OrdineDto>> GetAllAsync()
        {
            return await _context.Ordini
                .Include(o => o.ProdottiOrdine).ThenInclude(po => po.Prodotto)
                .Include(o => o.StatoOrdine)
                .Include(o => o.User)
                .Select(o => new OrdineDto
                {
                    OrdineId = o.OrdineId,
                    UserId = o.UserId,
                    UserEmail = o.User.Email, 
                    DataOrdine = o.DataOrdine,
                    Totale = o.Totale,
                    Stato = o.StatoOrdine.Nome,
                    Prodotti = o.ProdottiOrdine.Select(po => new OrdineDettaglioDto
                    {
                        NomeProdotto = po.Prodotto.NomeProdotto,
                        Quantita = po.Quantita,
                        PrezzoUnitario = po.PrezzoUnitario,
                        ImmagineProdotto = po.Prodotto.ImmagineProdotto
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
                        PrezzoUnitario = po.PrezzoUnitario,
                        ImmagineProdotto = po.Prodotto.ImmagineProdotto
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CambiaStatoAsync(Guid ordineId, int nuovoStatoId)
        {
            var ordine = await _context.Ordini
                .Include(o => o.ProdottiOrdine)
                .ThenInclude(po => po.Prodotto)
                .FirstOrDefaultAsync(o => o.OrdineId == ordineId);

            if (ordine == null) return false;

            if (nuovoStatoId == 4 && ordine.StatoOrdineId != 4)
            {
                foreach (var prodottoOrdine in ordine.ProdottiOrdine)
                {
                    prodottoOrdine.Prodotto.Stock += prodottoOrdine.Quantita;
                }
            }

            ordine.StatoOrdineId = nuovoStatoId;
            await _context.SaveChangesAsync(); 

            var stato = await _context.StatiOrdine
                .FirstOrDefaultAsync(s => s.StatoOrdineId == nuovoStatoId);

            var user = await _userManager.FindByIdAsync(ordine.UserId);
            if (user != null && stato != null)
            {
                await _emailService.SendEmailAsync(
                    user.Email,
                    "Stato Ordine Aggiornato",
                    $@"
                    <div style='font-family: Arial, sans-serif; background-color: #e3f2fd; padding: 20px;'>
                        <h2 style='color: #1565c0;'>Aggiornamento Stato Ordine</h2>
                        <p>Il tuo ordine <strong>{ordine.OrdineId}</strong> è stato aggiornato.</p>
                        <p>Nuovo stato: <span style='font-weight: bold; color: #0d47a1;'>{stato.Nome}</span></p>
                        <hr style='border: none; border-top: 1px solid #ccc;' />
                        <p style='font-size: 14px; color: #555;'>Grazie per aver scelto SpeedMarket!</p>
                    </div>"
                );

            }

            return true;
        }

    }

}
