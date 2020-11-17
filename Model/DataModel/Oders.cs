using Model.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class Oders
    {
        [Key]
        [Display(Name = "Mã đơn hàng ")]
        public string Oders_id { get; set; }
        [Display(Name = "Mã khách hàng  ")]
        public string cus_id { get; set; }

        [Display(Name = "Tổng thanh toán ")]
        public double total_amount { get; set; }
        [Display(Name = "Phương thức thanh toán ")]
        public string payment_method { get; set; }

        [Display(Name = "Mô tả")]
     
        public string Description { get; set; }
        [Display(Name = "Họ và tên  ")]
        public string fullname { get; set; }
        [Display(Name = "Địa chỉ ")]
        public string address { get; set; }
        [Display(Name = "Số điện thoại  ")]
        public string phone { get; set; }

        // navigtion 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo ")]
        public DateTime create_date { get; set; }

        [ForeignKey("cus_id")]
        public virtual Customer Customers { get; set; }
        public virtual ICollection<Oders_Detail> Oders_Details { set; get; }
    }
}
