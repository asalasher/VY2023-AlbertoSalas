using PK.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PK.DataAccess
{
    public interface IMovesRepository
    {
        Task<List<Move>> GetMoveNames(int number);
    }
}