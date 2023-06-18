using System.Collections.Generic;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// It receives an array of coordinates
    /// </summary>
    [RoutePrefix("api/coordinates")]
    public class CoordinatesController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Register([FromBody] List<decimal> coordinates)
        {
            return Ok<string>("value");
        }

    }
}
