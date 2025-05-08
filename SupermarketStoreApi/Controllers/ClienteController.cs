using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.DTOs.Cliente;
using SupermarketStoreApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace SupermarketStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del cliente");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            try
            {
                var clienteDto = await _clienteService.GetByIdAsync(userId);
                if (clienteDto == null)
                    return NotFound();

                _logger.LogInformation(clienteDto.ToString());
                return Ok(clienteDto);
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

        [HttpPut("{id}/modifica")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ClienteUpdateDto dto)
        {
            var result = await _clienteService.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
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
