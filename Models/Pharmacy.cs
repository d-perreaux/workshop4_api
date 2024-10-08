using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class Pharmacy {
        public int PharmacyId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Prescription>? Prescriptions { get; set;} = new List<Prescription>();
    }
}