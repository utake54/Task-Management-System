using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.SearchModel
{
    public class SearchModel
    {
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public string? Status { get; set; } 
    }
}
