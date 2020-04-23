using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CategoryViewModel
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string MetaTitle { set; get; }
        public string CateName { set; get; }
        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }

    }
}
