using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Prescription
{
    public class PrescriptionDto
    {
        public int PrescriptionId { get; set; }
        public int PharmacyId { get; set; }
        public DateTime Date { get; set; }
    }
}
