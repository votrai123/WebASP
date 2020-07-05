using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    class OrderDetailViewModel
    {
        public long ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string StreetAddress { get; set; }
        public string Phone { get; set; }
        public int? Status { get; set; }
        public string Note { get; set; }
        public long? UserID { get; set; }
        public User user { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
