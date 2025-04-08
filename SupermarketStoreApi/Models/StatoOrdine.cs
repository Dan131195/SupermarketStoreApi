using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.Models
{
    public class StatoOrdine
    {
        [Key]
        public int StatoOrdineId { get; set; }

        [Required]
        public string NomeStato { get; set; } 

        public ICollection<Ordine> Ordini { get; set; }
    }
}
