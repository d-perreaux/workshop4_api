using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class PrescriptionProductAdvice {
        public int PrescriptionId { get; set; }

        public int ProductId { get; set; }

        public int AdviceId { get; set; }

        public PrescriptionProduct? PrescriptionProduct{ get; set; }
        public Advice Advice { get; set; } = new Advice();
    }
}