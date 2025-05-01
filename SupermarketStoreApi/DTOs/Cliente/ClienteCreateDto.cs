using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Cliente
{
    public class ClienteCreateDto
    {
        [StringLength(16, MinimumLength = 16)]
        public required string CodiceFiscale { get; set; }

        public required string UserId { get; set; } 
    }
}
