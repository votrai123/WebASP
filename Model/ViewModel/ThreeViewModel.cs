using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ThreeViewModel
    {
        public long IDCategory { set; get; }
        public long IDProduct { set; get; }

        public long IDProductCategory { set; get; }

        public string NameCategory { set; get; }
        public string NameProductCategory { set; get; }

        public string MetaTitleCategory { set; get; }
        public string MetaTitleProductCategory { set; get; }
        public string MetaTitleProduct { set; get; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }
    }
}
