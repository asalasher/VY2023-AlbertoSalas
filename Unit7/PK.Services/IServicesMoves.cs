using System.Collections.Generic;
using System.Threading.Tasks;

namespace PK.Services
{
    public interface IServicesMoves
    {
        Task<List<string>> GetMoves();
    }
}