using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public long CIP { get; set; }
        public string DCI { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public bool FlagIsDelete { get; set; }

    }
}