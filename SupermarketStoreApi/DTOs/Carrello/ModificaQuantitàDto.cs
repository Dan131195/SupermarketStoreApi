using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Carrello
{
    public class ModificaQuantitaDto
    {
        public Guid ProdottoCarrelloId { get; set; }
        public int Quantita { get; set; }
    }
}
