namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "You must provide a Content Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "MetaTitle")]
        [StringLength(250)]
        public string MetaTitle { get; set; }
        
        [Display(Name = "Description")]
        [StringLength(10000)]
        public string Description { get; set; }
        
        [Display(Name = "Image")]
        [StringLength(250)]
        public string Image { get; set; }

        [Required(ErrorMessage = "You must provide a ID Category")]
        [Display(Name = "Category")]
        public long? CategoryID { get; set; }

        [Display(Name = "Detail")]
        [StringLength(10000)]
        public string Detail { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Status")]
        public bool? Status { get; set; }

        [Display(Name = "Top Hot")]
        public DateTime? TopHot { get; set; }


        [Display(Name = "View Count")]
        public int? ViewCount { get; set; }

        [Required(ErrorMessage = "You must provide a TAGs")]
        [StringLength(500)]
        public string Tags { get; set; }

    }
}
