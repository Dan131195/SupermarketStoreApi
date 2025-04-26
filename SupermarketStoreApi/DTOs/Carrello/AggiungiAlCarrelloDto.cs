using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Carrello
{
    public class AggiungiAlCarrelloDto
    {
        public string UserId { get; set; }
        public Guid ProdottoId { get; set; }
        public int Quantita { get; set; }
    }
}
