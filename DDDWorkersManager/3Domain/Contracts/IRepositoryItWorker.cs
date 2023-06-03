using System.Collections.Generic;

namespace DDDWorkersManager._3Domain.Contracts
{
    public interface IRepositoryItWorker : IRepository<ItWorker>
    {
        List<ItWorker> GetByTeamId(int idTeam);
    }
}
