using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CommentViewModel
    {
        public long ID { set; get; }
        public long IDContent { set; get; }

        public long IDUser { set; get; }

        public string FullName { set; get; }

        public string Content { set; get; }

        public DateTime? CommentCreatedDate { set; get; }

    }
}
