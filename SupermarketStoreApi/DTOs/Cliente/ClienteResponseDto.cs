namespace SupermarketStoreApi.DTOs.Cliente
{
    public class ClienteResponseDto
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }
        public string Email { get; set; }
        public string? Indirizzo { get; set; }
        public string? ImmagineProfilo { get; set; }

        public string UserId { get; set; }
    }

}
