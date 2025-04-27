using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.DTOs.Prodotto;
using SupermarketStoreApi.Services;

namespace SupermarketStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdottoController : ControllerBase
    {
        private readonly ProdottoService _service;
        private readonly ILogger<ProdottoController> _logger;
        private readonly IWebHostEnvironment _environment;

        public ProdottoController(ProdottoService service, ILogger<ProdottoController> logger, IWebHostEnvironment environment)
        {
            _service = service;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var prodotti = await _service.GetAllAsync();
                return Ok(prodotti);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero di tutti i prodotti");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var prodotto = await _service.GetByIdAsync(id);
                return prodotto != null ? Ok(prodotto) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero del prodotto con ID {id}");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProdottoCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var prodotto = await _service.CreateAsync(dto);
                return Ok(new { message = $"Prodotti trovati {prodotto.NomeProdotto} con id {prodotto.ProdottoId}"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione del prodotto");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpPut("{id}")]
//      [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProdottoUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(id, dto, _environment);
                return updated ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nell'aggiornamento del prodotto con ID {id}");
                return StatusCode(500, "Errore interno");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nella cancellazione del prodotto con ID {id}");
                return StatusCode(500, "Errore interno");
            }
        }
    }
}
