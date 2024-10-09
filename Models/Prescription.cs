using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class Prescription {
        public int PrescriptionId { get; set; }
        public int PharmacyId { get; set; }
        public DateTime Date { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public List<PrescriptionProduct> PrescriptionProducts { get; set; } = new List<PrescriptionProduct>();

    }
}