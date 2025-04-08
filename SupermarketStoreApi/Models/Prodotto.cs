using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketStoreApi.Models
{
    public class Prodotto
    {
        [Key]
        public Guid ProdottoId { get; set; }

        [Required]
        [StringLength(50)]
        public required string NomeProdotto { get; set; }

        [Required]
        public required string ImmagineProdotto { get; set; }

        [Required]
        [StringLength(1000)]
        public required string DescrizioneProdotto { get; set; }

        [Required]
        public decimal PrezzoProdotto { get; set; }

        [Required]
        public int Stock { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        public ICollection<ProdottoCarrello>? ProdottiCarrello { get; set; }

        public ICollection<ProdottoOrdine>? ProdottiOrdine { get; set; }
    }
}