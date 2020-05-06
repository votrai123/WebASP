using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { set; get; }

        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string FullName { set; get; }

        [Display(Name = "Country")]
        public string Country { set; get; }

        [Display(Name = "Street Address")]
        public string StreetAddress { set; get; }

        [Display(Name = "Phone")]
        public string Phone { set; get; }

        public string CreatedDate { set; get; }

        public string Status { set; get; }

        public string role { set; get; }
    }
}