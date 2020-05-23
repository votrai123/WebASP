namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentContent")]
    public partial class CommentContent
    {
        [Key]
        public long ID { get; set; }

        public long IDContent { get; set; }

        public long IDUser { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}