using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NETAPI2.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        static List<string> strs = new List<string>()
        {
            "value0",
            "value1",
            "value2"
        };
        // GET api/values
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return strs;
        }

        // GET api/values/5
        public string Get(int id)
        {
            //return "value";
            return strs[id];
        }

        // POST api/values - for Adding Item
        public void Post([FromBody]string value)
        {
            strs.Add(value);
        }

        // PUT api/values/5 - for updating Item
        public void Put(int id, [FromBody]string value)
        {
            strs[id] = value;
        }

        // DELETE api/values/5  -- for removing Item
        public void Delete(int id)
        {
            strs.RemoveAt(id);
        }
    }
}
