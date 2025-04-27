using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.Services;
using SupermarketStoreApi.DTOs.Ordine;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<IActionResult> ConfermaOrdine([FromBody] ConfermaOrdineRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var success = await _service.ConfermaOrdineAsync(userId, request.OraRitiro);
            if (!success) return BadRequest();
            return Ok();
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
