﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class IndexPaggingModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
