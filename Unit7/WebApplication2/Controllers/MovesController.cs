using PK.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class MovesController : ApiController
    {
        private readonly IServicesMoves _servicesMoves;

        public MovesController(IServicesMoves servicesMoves)
        {
            _servicesMoves = servicesMoves;
        }

        // GET: api/Moves
        public async Task<IHttpActionResult> Get()
        {
            var names = await _servicesMoves.GetMoves();
            return Ok(names);
        }

    }
}
