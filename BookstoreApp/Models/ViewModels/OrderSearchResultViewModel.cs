using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreApp.Models.ViewModels
{
    public class OrderSearchResultViewModel
    {
        public string orderId { get; set; }
        public string storeName { get; set; }
        public string bookName { get; set; }

        public OrderSearchResultViewModel(string ordId, string stName, string bkName)
        {
            orderId = ordId;
            storeName = stName;
            bookName = bkName;
        }
    }
}