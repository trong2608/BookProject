using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
  public  class Category
    {
        [Key]
        [Display(Name = "Mã danh mục")]
        [Required(ErrorMessage = "Hãy nhập mã số")]
        public string cat_id { get; set; }
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Hãy nhập tên danh mục")]
        public string cat_name { get; set; }
        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Hãy nhập trạng thái")]
        public bool status { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Hãy nhập mô tả")]
        public string Description { get; set; }


        public virtual ICollection<Product> Produts { get; set; }
    }
}
