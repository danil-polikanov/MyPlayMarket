using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities.DTO
{
    public record LoginUserDTO([Required] string Email,
        [Required] string Password);
}
