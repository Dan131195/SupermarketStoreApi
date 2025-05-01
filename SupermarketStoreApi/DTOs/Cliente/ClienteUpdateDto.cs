using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Cliente
{
    public class ClienteUpdateDto
    {
        [StringLength(16, MinimumLength = 16)]
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? CodiceFiscale { get; set; }
        public string? Email { get; set; }
        public string? Indirizzo { get; set; }
        public IFormFile? ImmagineFile { get; set; }
    }
}
