using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models {
public class Advice
{
    public int AdviceId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime DateStart { get; set; } = DateTime.Now;
    public DateTime? DateEnd { get; set; } 
    public bool FlagIsDeleted { get; set; } = false;
    public List<ProductAdvice> ProductAdvices { get; set; } = new List<ProductAdvice>();
    public List<PrescriptionProductAdvice> PrescriptionProductAdvices { get; set; } = new List<PrescriptionProductAdvice>();
}

}