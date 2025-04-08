using SupermarketStoreApi.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.Models
{
    public class ProdottoCarrello
    {
        [Key]
        public Guid ProdottoCarrelloId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public Guid ProdottoId { get; set; }

        [ForeignKey("ProdottoId")]
        public Prodotto Prodotto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1")]
        public int Quantita { get; set; }
    }
}

