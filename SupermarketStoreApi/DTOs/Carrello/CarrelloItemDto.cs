namespace SupermarketStoreApi.DTOs.Carrello
{
    public class CarrelloItemDto
    {
        public Guid ProdottoCarrelloId { get; set; }
        public string UserId { get; set; }
        public Guid ProdottoId { get; set; }
        public string NomeProdotto { get; set; }
        public int Quantita { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public string? ImmagineFile { get; set; } 
        public decimal Totale => Quantita * PrezzoUnitario;
    }
}
