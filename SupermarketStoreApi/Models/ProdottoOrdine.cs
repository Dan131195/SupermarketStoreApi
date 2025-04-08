using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketStoreApi.Models
{
    public class ProdottoOrdine
    {
        public int ProdottoOrdineId { get; set; }

        public Guid OrdineId { get; set; }

        [ForeignKey("OrdineId")]
        public Ordine Ordine { get; set; }

        public Guid ProdottoId { get; set; }

        [ForeignKey("ProdottoId")]
        public Prodotto Prodotto { get; set; }

        public int Quantità { get; set; }

        public decimal Prezzo { get; set; }
    }
}
