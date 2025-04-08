using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeCategoria { get; set; }

        public ICollection<Prodotto>? Prodotti { get; set; }
    }
}

