using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Prodotto
{
    public class ProdottoCreateDto
    {
        [StringLength(50)]
        public required string NomeProdotto { get; set; }

        public required IFormFile ImmagineFile { get; set; }

        [StringLength(1000)]
        public required string DescrizioneProdotto { get; set; }

        public decimal PrezzoProdotto { get; set; }

        public int Stock { get; set; }

        public required string NomeCategoria { get; set; }
    }
}

