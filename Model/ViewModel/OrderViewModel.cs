using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderViewModel
    {
        public long ProductID { set; get; }

        public long OrderID { set; get; }


        public decimal? OrderPrice { set; get; }


        public int? OrderQuantity { set; get; }

        public DateTime? OrderCreatedDate { set; get; }


        public string ImageProduct { set; get; }
        public string NameProduct { set; get; }

    }
}
