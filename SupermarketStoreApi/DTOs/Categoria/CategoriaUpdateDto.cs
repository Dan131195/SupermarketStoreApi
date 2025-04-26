using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Categoria
{
    public class CategoriaUpdateDto
    {
        [StringLength(100)]
        public required string NomeCategoria { get; set; }
    }
}
