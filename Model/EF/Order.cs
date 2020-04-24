namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UserID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string StreetAddress { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string Note { get; set; }
    }
}