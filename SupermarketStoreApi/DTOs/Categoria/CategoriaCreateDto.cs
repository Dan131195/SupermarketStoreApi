using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Categoria
{
    public class CategoriaCreateDto
    {
        [StringLength(100)]
        public required string NomeCategoria { get; set; }
    }
}
