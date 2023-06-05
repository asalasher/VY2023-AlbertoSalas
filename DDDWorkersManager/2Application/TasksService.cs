using DDDWorkersManager._3Domain;
using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._2Application
{
    public class TasksService : ITasksService
    {
        private readonly IRepositoryTasks _tasksRepository;
        private readonly IRepositoryItWorker _workersRepository;
        private readonly ISession _session;

        public TasksService(IRepositoryTasks tasksRepository, ISession session)
        {
            _tasksRepository = tasksRepository;
            _session = session;
        }

        public (bool status, string error) RegisterNewTask(string name, string description, string technology)
        {
            if (_session.WorkerRole != WorkerRoles.Admin)
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

            if (task is null)
            {
                return (false, "task not found");
            }

            ItWorker worker = _workersRepository.GetById(idWorker);
            if (worker is null)
            {
                return (false, "user not found");
            }

            (bool status, string errorMsg) = task.AssignTaskToItWorker(worker);
            return (status, errorMsg);
        }

        public (List<string> unassignedTasks, string error) GetUnassignedTasks()
        {
            List<Tasks> task = _tasksRepository.GetTasksByAssignedWorker(null);
            return (task.Select(x => x.ToString()).ToList(), string.Empty);
        }

    }
}
