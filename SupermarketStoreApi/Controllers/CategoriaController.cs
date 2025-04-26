using Microsoft.AspNetCore.Mvc;
using SupermarketStoreApi.DTOs.Categoria;
using SupermarketStoreApi.Services;

namespace SupermarketStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(CategoriaService service, ILogger<CategoriaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categorie = await _service.GetAllAsync();
                return Ok(categorie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero di tutte le categorie");
                return StatusCode(500, "Errore interno del server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var categoria = await _service.GetByIdAsync(id);
                return categoria != null ? Ok(categoria) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero della categoria con ID {Id}", id);
                return StatusCode(500, "Errore interno del server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var categoria = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della categoria");
                return StatusCode(500, "Errore interno del server");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della categoria con ID {Id}", id);
                return StatusCode(500, "Errore interno del server");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della categoria con ID {Id}", id);
                return StatusCode(500, "Errore interno del server");
            }
        }
    }

}
