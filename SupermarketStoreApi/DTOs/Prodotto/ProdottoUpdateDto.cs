﻿using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.DTOs.Prodotto
{
    public class ProdottoUpdateDto
    {
        [StringLength(50)]
        public string? NomeProdotto { get; set; }

        public IFormFile? ImmagineFile { get; set; }

        [StringLength(1000)]
        public string? DescrizioneProdotto { get; set; }

        public decimal PrezzoProdotto { get; set; }

        public int Stock { get; set; }

        public string? NomeCategoria { get; set; }
    }

}
