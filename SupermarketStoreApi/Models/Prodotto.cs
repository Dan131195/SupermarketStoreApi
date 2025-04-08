using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.Models
{
    public class Prodotto
    {
        [Key]
        public Guid ProdottoId { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeProdotto { get; set; }
        [Required]


        [Required]
        [StringLength(1000)]
        public string DescrizioneProdotto { get; set; }

        [Required]
        public decimal PrezzoProdotto { get; set; }

        [Required]
        public int Stock {  get; set; }
    }
}
