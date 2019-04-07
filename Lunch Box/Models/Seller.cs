using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch_Box.Models
{
    public class Seller
    {
        public int itemId { get; set; }
        public string snackType { get; set; }
        public string snackState { get; set; }
        public Nullable<double> basePrice { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> unit { get; set; }
    }
}