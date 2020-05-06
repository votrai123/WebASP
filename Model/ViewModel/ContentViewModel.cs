using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ContentViewModel
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CateName { get; set; }

        public string Detail { get; set; }

        public DateTime? CreatedDate { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
