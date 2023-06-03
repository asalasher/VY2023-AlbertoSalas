using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Worker;

namespace DDDWorkersManager._2Application
{
    public class TasksService : ITasksService
    {
        private readonly IRepositoryTasks _tasksRepository;
        private readonly ISession _session;

        public TasksService(IRepositoryTasks tasksRepository, ISession session)
        {
            _tasksRepository = tasksRepository;
            _session = session;
        }

        public (bool status, string error) RegisterNewTask(string name, string description, string technology)
        {
            if (!_session.IsActiveUserManager)
            {
                return (false, "not allowed");
            }

            bool status = _tasksRepository.Insert(new Tasks(name, description, technology));
            string errorMsg = status ? string.Empty : "error when saving in database";
            return (status, errorMsg);
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
