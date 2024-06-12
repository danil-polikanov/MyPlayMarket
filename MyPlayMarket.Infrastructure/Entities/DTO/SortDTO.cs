using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class SortDTO
    {
        [DataType(DataType.Text)]
        public string SortBy { get; set; }

    }
}
