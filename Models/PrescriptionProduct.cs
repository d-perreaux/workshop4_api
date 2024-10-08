using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class PrescriptionProduct {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public List<PrescriptionProductAdvice> PrescriptionProductAdvices { get; set; } = new List<PrescriptionProductAdvice>();
    }
}