using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    
   public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập mã sản phẩm ")]
        public string pro_id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm")]
        public string pro_name { get; set; }
        [Display(Name = "ảnh sản phẩm")]
        
        public string pro_image { get; set; }
        [Display(Name = "Mô tả sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập mô tả")]
        public string pro_description { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string create_date { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Hãy nhập giá")]
        public Nullable<double> pro_price { get; set; }
        [Display(Name = "Giá sau khi giảm giá")]

        public double pro_sale_price { get; set; }
        [Display(Name = "Mã Đơn hàng")]
       
        public string cat_id { get; set; }
        [Display(Name = "Mã NXB")]
      
        public string pub_id { get; set; }
        [Display(Name = "Mã tác giả")]
       
        public string author_id { get; set; }

        //navigation 

        [ForeignKey("cat_id")]
        public virtual Category Categorys { get; set; }
        [ForeignKey("pub_id")]
        public virtual Publisher Publishers { get; set; }
        [ForeignKey("author_id")]
        public virtual Author Authors { get; set; }

        public virtual ICollection<Oders_Detail> Oders_Details { get; set; }
    }
}
