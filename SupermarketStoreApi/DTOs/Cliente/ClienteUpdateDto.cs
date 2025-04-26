using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Cliente
{
    public class ClienteUpdateDto
    {
        [StringLength(16, MinimumLength = 16)]
        public string? CodiceFiscale { get; set; }
    }
}
