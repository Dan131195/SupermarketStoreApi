using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Account
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string CodiceFiscale { get; set; }

        [Required]
        [RegularExpression("User|Seller", ErrorMessage = "Ruolo non valido. Ammessi: User, Seller")]
        public string Ruolo { get; set; } 
    }
}
