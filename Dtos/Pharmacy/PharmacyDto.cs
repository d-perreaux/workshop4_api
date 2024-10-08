using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Pharmacy
{
    public class PharmacyDto
    {
        public int? PharmacyId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}