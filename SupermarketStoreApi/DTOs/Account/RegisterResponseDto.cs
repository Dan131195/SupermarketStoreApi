namespace SupermarketStoreApi.DTOs.Account
{
    public class RegisterResponseDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Ruolo { get; set; }
        public Guid? ClienteId { get; set; }
    }
}
