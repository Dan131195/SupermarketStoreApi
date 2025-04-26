namespace SupermarketStoreApi.DTOs.Prodotto
{
    public class ProdottoDto
    {
        public Guid ProdottoId { get; set; }
        public string? NomeProdotto { get; set; }
        public string? ImmagineFile { get; set; }
        public string? DescrizioneProdotto { get; set; }
        public decimal PrezzoProdotto { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public string? CategoriaNome { get; set; }
    }
}
