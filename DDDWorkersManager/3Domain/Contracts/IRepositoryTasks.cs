using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;

namespace DDDWorkersManager._3Domain.Contracts
{
    public interface IRepositoryTasks : IRepository<Tasks>
    {
        List<Tasks> GetTasksByAssignedWorker(List<int> idWorkers);
    }
}
