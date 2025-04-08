using System.ComponentModel.DataAnnotations;
using SupermarketStoreApi.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketStoreApi.Models
{
    public class Cliente
    {
        [Key]
        public Guid ClienteId { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        public required string CodiceFiscale { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
