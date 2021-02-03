using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreApp.Models.ViewModels
{
    public class SearchFormViewModel
    {
        public int sales { get; set; }

        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}