
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.DTOs.Cliente;
using SupermarketStoreApi.Models.Auth;
using SupermarketStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Services;

namespace SupermarketStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteCreateDto dto)
        {
            try
            {
                var result = await _clienteService.CreateAsync(dto);
                if (result == null)
                    return BadRequest(new { Message = "Utente non trovato o cliente già esistente." });

                return CreatedAtAction(nameof(GetById), new { id = result.ClienteId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del cliente");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var cliente = await _clienteService.GetByIdAsync(id);
                return cliente != null ? Ok(cliente) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del cliente");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clienti = await _clienteService.GetAllAsync();
                return Ok(clienti);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei clienti");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClienteUpdateDto dto)
        {
            try
            {
                var updated = await _clienteService.UpdateAsync(id, dto);
                return updated ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del cliente");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _clienteService.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del cliente");
                return StatusCode(500, "Errore interno");
            }
        }
    }


}
