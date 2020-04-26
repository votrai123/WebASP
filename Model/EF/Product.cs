namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "You must provide a Name Product")]
        [Display(Name = "Product Name")]

        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(1500)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "You must provide a Description")]
        [MaxLength(ErrorMessage = "max 1500")]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Required(ErrorMessage = "You must provide a Prrice")]
        [Display(Name = "Prrice")]
        [Range(0, Int32.MaxValue,ErrorMessage ="Format Number")]
        public decimal? Prrice { get; set; }

        [Required(ErrorMessage = "You must provide a Promotion")]
        [Display(Name = "Promotion")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Format Number")]
        public decimal? Promotion { get; set; }

        [Required(ErrorMessage = "You must provide a Quantity")]
        [Display(Name = "Quantity")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Format Number")]
        public int? Quantity { get; set; }

        [Display(Name = "Category")]
        public long? CategoryID { get; set; }

        [StringLength(1500)]
        [Display(Name = "Detail")]
        [Required(ErrorMessage = "You must provide a Detail")]
        [MaxLength(ErrorMessage = "max 1500")]
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

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
