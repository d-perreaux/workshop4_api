using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class Product{
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CIP { get; set; }
        public string DCI { get; set; }
        public string Dosage { get; set; }
        public bool FlagIsDelete { get; set; }
        public List<ProductAdvice>? ProductAdvices { get; set;} = new List<ProductAdvice>();
        public List<PrescriptionProduct>? PrescriptionProducts { get; set;} = new List<PrescriptionProduct>();
    }
}