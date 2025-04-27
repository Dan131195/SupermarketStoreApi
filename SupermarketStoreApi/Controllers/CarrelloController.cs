using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.Services;
using SupermarketStoreApi.DTOs.Carrello;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SupermarketStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarrelloController : ControllerBase
    {
        private readonly CarrelloService _service;

        public CarrelloController(CarrelloService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var carrello = await _service.GetByUserIdAsync(userId);
            return Ok(carrello);
        }

        [HttpPost]
        public async Task<IActionResult> Aggiungi([FromBody] AggiungiAlCarrelloDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            dto.UserId = userId; 

            var result = await _service.AggiungiAsync(dto);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificaQuantita(Guid id, [FromBody] ModificaQuantitaDto dto)
        {
            var result = await _service.ModificaQuantitaAsync(id, dto.Quantita);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Rimuovi(Guid id)
        {
            var result = await _service.RimuoviAsync(id);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("svuota/{userId}")]
        public async Task<IActionResult> Svuota(string userId)
        {
            var count = await _service.SvuotaCarrelloAsync(userId);
            return Ok(new { Rimossi = count });
        }
    }

}
