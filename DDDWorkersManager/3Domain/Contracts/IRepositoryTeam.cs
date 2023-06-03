using DDDWorkersManager._3Domain.Entities.Team;

namespace DDDWorkersManager._3Domain.Contracts
{
    public interface IRepositoryTeam : IRepository<Team>
    {
        Team GetByManagerId(int idManager);
    }
}
