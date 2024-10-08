using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
    public class ProductAdvice {
        public int ProductId { get; set; }
        public Product Product{ get; set; }

        public int AdviceId { get; set; }
        public Advice Advice { get; set; }
    }
}