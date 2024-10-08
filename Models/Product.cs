using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class Product{
        public int? ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CIP { get; set; }
        public List<Advice> Advices { get; set;} = new List<Advice>();
    }
}