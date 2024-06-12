using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class FilterDTO
    {
        public string Name { get; set; }
        public string Company { get; set; }
        [Range(1900, 2100,ErrorMessage = "Invalid year")]
        public int Release { get; set; }
        
    }
}
