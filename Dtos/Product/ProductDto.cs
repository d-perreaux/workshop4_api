using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class ProductDto
    {
        public int? ProductId { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}