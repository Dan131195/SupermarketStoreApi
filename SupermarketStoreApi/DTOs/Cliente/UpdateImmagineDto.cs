using Microsoft.AspNetCore.Mvc;

namespace SupermarketStoreApi.DTOs.Cliente
{
    public class UpdateImmagineDto
    {
        [FromForm]
        public IFormFile ImmagineFile { get; set; }
    }
}
