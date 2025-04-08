using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Prodotto
{
    public class ProdottoCreateDto
    {
        [StringLength(50)]
        public string? NomeProdotto { get; set; }

        public string? ImmagineProdotto { get; set; }

        [StringLength(1000)]
        public string? DescrizioneProdotto { get; set; }

        public decimal PrezzoProdotto { get; set; }

        public int Stock { get; set; }

        public int CategoriaId { get; set; }
    }
}

