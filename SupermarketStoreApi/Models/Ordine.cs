using SupermarketStoreApi.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.Models
{
    public class Ordine
    {
        [Key]
        public Guid OrdineId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public decimal Totale { get; set; }

        [Required]
        public DateTime? OraRitiro { get; set; }

        public ICollection<ProdottoOrdine> ProdottiOrdine { get; set; }

        [Required]
        public int StatoOrdineId { get; set; }

        [ForeignKey("StatoOrdineId")]
        public StatoOrdine StatoOrdine { get; set; }

        public DateTime DataOrdine { get; set; } = DateTime.UtcNow;
    }

}
