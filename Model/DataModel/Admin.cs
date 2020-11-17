using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    public class Admin
    {
        [Key]
        
        public string id { get; set; }
        [Required(ErrorMessage = "Hãy nhập Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Hãy nhập Password")]
        public string Password { get; set; }
    }
}
