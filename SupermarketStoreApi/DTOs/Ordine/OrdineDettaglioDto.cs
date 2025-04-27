namespace SupermarketStoreApi.DTOs.Ordine
{
    public class OrdineDettaglioDto
    {
        public string NomeProdotto { get; set; }
        public int Quantita { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public string ImmagineProdotto { get; set; }
    }
}
