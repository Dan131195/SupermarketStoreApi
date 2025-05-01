namespace SupermarketStoreApi.DTOs.Cliente
{
    public class ClienteDto
    {
        public Guid ClienteId { get; set; }
        public string CodiceFiscale { get; set; }
        public string Email { get; set; }
        public string? ImmagineProfilo { get; set; }
        public string NomeCompleto { get; set; } 
    }
}
