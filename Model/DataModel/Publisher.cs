using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
 public   class Publisher
    {
        [Key]
        [Display(Name = "Mã NXB")]
        [Required(ErrorMessage = "Hãy nhập Mã NXB")]
        public string pub_id { get; set; }
        [Display(Name = "Tên NXB ")]
        [Required(ErrorMessage = "Hãy nhập tên NXB")]
        public string pub_name { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Hãy nhập Mô tả sản phẩm")]
        public string Description { get; set; }


        public virtual ICollection<Product> Produts { get; set; }
    }
}
