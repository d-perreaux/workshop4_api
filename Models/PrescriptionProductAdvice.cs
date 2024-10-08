using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class PrescriptionProductAdvice {
        public int PrescriptionId { get; set; }
        public Prescription Prescription{ get; set; } = new Prescription();

        public int ProductId { get; set; }
        public Product Product{ get; set; } = new Product();

        public int AdviceId { get; set; }
        public Advice Advice{ get; set; } = new Advice();

        public PrescriptionProduct? PrescriptionProduct{ get; set; }
    }
}