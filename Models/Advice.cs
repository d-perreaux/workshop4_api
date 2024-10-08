using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class Advice {
        public int? AdviceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Product? Product { get; set; }
    }
}