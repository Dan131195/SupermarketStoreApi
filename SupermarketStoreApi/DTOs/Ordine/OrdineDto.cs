namespace SupermarketStoreApi.DTOs.Ordine
{
    public class OrdineDto
    {
        public Guid OrdineId { get; set; }
        public string UserId { get; set; }
        public DateTime DataOrdine { get; set; }
        public decimal Totale { get; set; }
        public string Stato { get; set; }
        public List<OrdineDettaglioDto> Prodotti { get; set; }
    }
}
