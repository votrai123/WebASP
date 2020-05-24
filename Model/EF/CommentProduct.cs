namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentProduct")]
    public partial class CommentProduct
    {
        [Key]
        public long ID { get; set; }

        public long IDProduct { get; set; }

        public long IDUser { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}