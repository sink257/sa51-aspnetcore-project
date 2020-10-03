using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.Models;

namespace Team8CA.DataAccess
{
    public class DBInitialiser
    {
        public DBInitialiser (AppDbContext db)
        {
            db.CustomerLogin.Add(new CustomerLogin("test", "test"));
        }
    }
}
