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

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmpDBEntities entities = new EmpDBEntities())
                {
                    var emp = entities.Employees.FirstOrDefault(e => e.Id == id);
                    if (emp != null)
                    {
                        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.Id == id));
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emp with Id: " + id.ToString() + " NOT FOUND!");
                    }
                }

            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Put(  int id, [FromBody] Employee emp)
        {
            using (EmpDBEntities entities = new EmpDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.Id == id);
                if (entity == null) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emp with Id: " + id.ToString() + " NOT FOUND!");
                }
                try
                {
                    
                    entity.Name = emp.Name;
                    entity.Email = emp.Email;
                    entity.PhotoPath = emp.PhotoPath;
                    entity.Department = emp.Department;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
