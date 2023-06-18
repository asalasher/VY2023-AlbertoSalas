using PK.DataAccess;
using PK.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PK.Services
{
    public class ServicesMoves : IServicesMoves
    {
        private readonly IMovesRepository _movesRepository;

        public ServicesMoves(IMovesRepository movesRepository)
        {
            _movesRepository = movesRepository;
        }

        public async Task<List<string>> GetMoves()
        {
            List<Move> moves = await _movesRepository.GetMoveNames(10);
            return moves.Select(move => move.SpanishName).ToList();
        }
    }
}
