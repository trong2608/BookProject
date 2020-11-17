using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
  public  class Customer
    {
        [Key]
        public string cus_id { get; set; }
        [Display(Name = "tên khách hàng")]

        [Required(ErrorMessage = "Hãy nhập tên khách hàng")]
        public string cus_name { get; set; }
        [Display(Name = "Email")]

        [Required(ErrorMessage = "Hãy nhập Email")]
        public string email { get; set; }
        [Display(Name = "Mã danh mục")]
        [Required(ErrorMessage = "Hãy nhập mã số")]
        public string password { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Hãy nhậpSố điện thoại")]
        public string phone { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Hãy nhập Địa chỉ")]
        public string address { get; set; }
        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Hãy nhập Trạng thái")]
        public bool status { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime create_date { get; set; }


        public virtual ICollection<Oders> Oderss { get; set; }
    }
}
