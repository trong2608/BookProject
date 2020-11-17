using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
 public   class Oders_Detail
    {
        [Key]
        [Display(Name = "Mã đơn hàng chi tiết ")]
        public int Order_Detail { get; set; }
        [Display(Name = "Mã đơn hàng ")]
        public string Oders_id { get; set; }
        [Display(Name = "Mã sản phẩm ")]
        public string pro_id { get; set; }

        [Display(Name = "Số lượng ")]
        public int quantity { get; set; }

        [Display(Name = "giá sản phẩm ")]
        public double detail_price { get; set; }
        [Display(Name = "Giá sau khi sale ")]
        public double detail_sale_price { get; set; }
        [Display(Name = "Tổng đơn giá ")]
        public double total_price { get; set; }
        [ForeignKey("Oders_id")]
        public virtual Oders Oderss { get; set; }
        [ForeignKey("pro_id")]
        public virtual Product Products { get; set; }
    }
}
