using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class UpdateProductRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public int CIP { get; set; }

        public string DCI { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public bool FlagIsDelete { get; set; } = false;
    }
}