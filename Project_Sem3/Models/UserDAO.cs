using Model;
using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Sem3.Models
{
    public class UserDAO
    {
        ProjectDbContext db = new ProjectDbContext();
        public string Insert(Customer cu)
        {
            db.Customers.Add(cu);
            db.SaveChanges();
            return cu.cus_id;
        }
        public bool CheckEmail(string email)
        {
            var data = db.Customers.Where(x => x.email.Equals(email));
            if (data.Count() >0)
            {
                return true;
            }
            return false;
        }
        
        public bool CheckPhone(string phone)
        {
            var data = db.Customers.Where(x => x.phone.Equals(phone));
            if (data.Count() > 0)
            {
                return true;
            }
            return false;
        }


    }
}