using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
   public  class Author
    {
        
        [Key]
        [Display(Name = "Mã tác giả")]
        [Required(ErrorMessage = "Hãy nhập mã số")]
        public string author_id { get; set; }
        [Display(Name = "Tên tác giả")]
        [Required(ErrorMessage = "Hãy nhập tên tác giả")]
        public string author_name { get; set; }
        [Display(Name = "Ảnh")]
        
        public string image { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Hãy nhập mô tả")]
        public string Description { get; set; }

        public virtual ICollection<Product> Produts { get; set; }
    }
}
