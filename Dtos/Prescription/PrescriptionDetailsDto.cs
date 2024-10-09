using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;

namespace api.Dtos.Prescription
{
    public class PrescriptionDetailsDto
{
    public int PrescriptionId { get; set; }
    public int PharmacyId { get; set; }
    public DateTime Date { get; set; }
    public List<ProductWithAdvicesDto> Products { get; set; } = new List<ProductWithAdvicesDto>();
}
}
