namespace SupermarketStoreApi.DTOs.Cliente
{
    public class ClienteResponseDto
    {
        public Guid ClienteId { get; set; }

        public string CodiceFiscale { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string NomeCompleto { get; set; }
    }

}
