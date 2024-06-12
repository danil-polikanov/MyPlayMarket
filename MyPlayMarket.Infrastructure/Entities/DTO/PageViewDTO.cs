using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class PageViewDTO
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageItems { get; set; }
        public int TotalPages { get; set; }
        public PageViewDTO(int currentPage, int totalItems, int pageItems)
        {
            CurrentPage = currentPage;
            TotalItems = totalItems;
            PageItems = pageItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageItems);
        }
    }
}
