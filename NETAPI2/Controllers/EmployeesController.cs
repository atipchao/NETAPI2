using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpDataAccess;

namespace NETAPI2.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            //throw new NotImplementedException();
            using (EmpDBEntities entities = new EmpDBEntities())
            {
                return entities.Employees.ToList();
            }
        }
        //public Employee Get(int id)
        public HttpResponseMessage Get(int id)
        {
            using (EmpDBEntities entities = new EmpDBEntities())
            {
                var entity =  entities.Employees.FirstOrDefault(s => s.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emp with Id: " + id.ToString() + " NOT FOUND!");
                }
            }
        }

        //public void Post([FromBody] Employee emp)
        public HttpResponseMessage Post([FromBody] Employee emp)
        {
            try
            {
                using (EmpDBEntities entities = new EmpDBEntities())
                {
                    entities.Employees.Add(emp);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    message.Headers.Location = new Uri(Request.RequestUri + emp.Id.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
