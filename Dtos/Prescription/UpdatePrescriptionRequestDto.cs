using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Prescription
{
    public class UpdatePrescriptionRequestDto
    {
        public int PharmacyId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}