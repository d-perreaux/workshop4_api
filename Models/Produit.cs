using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class Produit{
        public int? ProduitId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Conseil> Conseils { get; set;} = new List<Conseil>();
    }
}