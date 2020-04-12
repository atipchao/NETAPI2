using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmpDataAccess;

namespace NETAPI2
{
    public class EmployeeSecurity
    {

        public static bool Login(string username, string password)
        {
            using (EmpDBEntities entitys  = new EmpDBEntities())
            {
                //this line below returnd either true or false
                return entitys.Users.Any(user => user.UserName.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}