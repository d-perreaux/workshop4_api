using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Advice;

namespace api.Dtos.Product
{
    public class ProductWithAdvicesDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CIP { get; set; }
        public string DCI { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public List<AdviceDto> Advices { get; set; } = new List<AdviceDto>();
    }

}