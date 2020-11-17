using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Sem3.Models
{
    public class RegisterViewModel
    {
        
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }

        
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất là 6 kí tự")]
        [Column(TypeName = "varchar")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string PassWord { get; set; }

        
        [Column(TypeName = "varchar")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string ConfirmPW { get; set; }

        
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Tên thành viên  không được để trống")]
        public string UserName { get; set; }

        
        [MaxLength(20)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phải nhập số!")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; }

       

        
        [MaxLength(256)]
        [Required(ErrorMessage = "Hãy nhập địa chỉ")]
        public string Address { get; set; }

    }
}