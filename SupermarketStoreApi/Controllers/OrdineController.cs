using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.Services;
using SupermarketStoreApi.DTOs.Ordine;
using Microsoft.AspNetCore.Authorization;

namespace SupermarketStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdineController : ControllerBase
    {
        private readonly OrdineService _service;

        public OrdineController(OrdineService service)
        {
            _service = service;
        }

        [HttpPost("conferma")]
        public async Task<IActionResult> Conferma([FromBody] ConfermaOrdineRequest richiesta)
        {
            var result = await _service.ConfermaOrdineAsync(richiesta.UserId);
            return result ? Ok() : BadRequest("Carrello vuoto o errore interno");
        }

        [HttpGet("storico/{userId}")]
        public async Task<IActionResult> Storico(string userId)
        {
            var ordini = await _service.GetStoricoAsync(userId);
            return Ok(ordini);
        }

        [HttpGet("dettagli/{ordineId}")]
        public async Task<IActionResult> Dettagli(Guid ordineId)
        {
            var ordine = await _service.GetDettagliOrdineAsync(ordineId);
            return ordine != null ? Ok(ordine) : NotFound();
        }

        [HttpPatch("{ordineId}/stato")]
        public async Task<IActionResult> CambiaStato(Guid ordineId, [FromBody] CambiaStatoOrdineDto dto)
        {
            var result = await _service.CambiaStatoAsync(ordineId, dto.StatoOrdineId);
            return result ? Ok() : NotFound();
        }
    }
}
