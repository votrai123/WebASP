namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "You must provide a Username")]
        [Display(Name = "UserName")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "You must provide a Password")]
        [Display(Name = "Password")]
        [StringLength(32)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must provide a Full Name")]
        [Display(Name = "FullName")]
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(50)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [StringLength(50)]
        [Display(Name = "Country")]

        public string Country { get; set; }

        [Required(ErrorMessage = "You must provide Street Address")]
        [Display(Name = "Street Address")]
        [StringLength(250)]
        public string StreetAddress { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        public bool? Status { get; set; }
        public bool? role { get; set; }

    }
}
