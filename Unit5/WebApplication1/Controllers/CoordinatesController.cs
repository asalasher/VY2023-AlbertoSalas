using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class CoordinatesController : ApiController
    {
        // GET: api/Coordinates
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Coordinates/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Coordinates
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Coordinates/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Coordinates/5
        public void Delete(int id)
        {
        }
    }
}
