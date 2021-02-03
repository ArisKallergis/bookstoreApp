using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreApp.Models.ViewModels
{
    public class OrderSearchFormViewModel
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        public char nameFrom { get; set; }
        public char nameTo { get; set; }
    }
}