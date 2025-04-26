using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Categoria
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        public string? NomeCategoria { get; set; }
    }
}
