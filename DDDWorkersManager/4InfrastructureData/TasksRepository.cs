using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._4InfrastructureData
{
    public class TasksRepository : IRepositoryTasks
    {
        private static readonly List<Tasks> _tasks = new List<Tasks>();

        public bool IsEntityOnDDBB(int id) => _tasks.FirstOrDefault(x => x.Id == id) != null;

        public bool Delete(int id)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Id == id)
                {
                    _tasks.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<Tasks> GetAll() => _tasks;

        public Tasks GetById(int id) => _tasks.FirstOrDefault(x => x.Id == id);

        public List<Tasks> GetTasksByAssignedWorker(List<int> idWorkers)
        {
            if (_tasks.Count == 0)
            {
                return _tasks;
            }

            return _tasks.Where(x => idWorkers.Contains((int)x.IdWorker)).ToList();
        }

        public bool Insert(Tasks entity)
        {
            if (IsEntityOnDDBB(entity.Id))
            {
                return false;
            }
            _tasks.Add(entity);
            return true;
        }

        public bool Update(Tasks entity)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Id == entity.Id)
                {
                    _tasks[i] = entity;
                    return true;
                }
            }
            return false;
        }

    }
}
