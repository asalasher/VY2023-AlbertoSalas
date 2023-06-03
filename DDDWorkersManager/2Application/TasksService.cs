using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Worker;

namespace DDDWorkersManager._2Application
{
    public class TasksService
    {
        private readonly IRepositoryTeam _teamsRepository;
        private readonly IRepositoryItWorker _workersRepository;
        private readonly IRepositoryTasks _tasksRepository;

        public TasksService(IRepositoryTeam teamsRepository, IRepositoryItWorker workersRepository, IRepositoryTasks tasksRepository)
        {
            _teamsRepository = teamsRepository;
            _workersRepository = workersRepository;
            _tasksRepository = tasksRepository;
        }

        public (bool status, string error) AssignTaskToItWorker(int idWorker, int idTask)
        {
            Tasks task = _tasksRepository.GetById(idTask);

            if (task == null)
            {
                return (false, "task not found");
            }

            task.IdWorker = idWorker;
            _tasksRepository.Update(task);

            return (true, string.Empty);
        }

    }
}
