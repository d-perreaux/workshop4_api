using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Advice
{
    public class CreateAdviceRequestDto
    {
        public string Content { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}