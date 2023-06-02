using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._4InfrastructureData
{
    public class TasksRepository : IRepositoryTasks
    {
        private static readonly List<Tasks> _tasks;

        public bool IsEntityOnDDBB(string id) => _tasks.FirstOrDefault(x => x.Id == id) != null;

        public bool Delete(string id)
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

        public Tasks GetById(string id) => _tasks.FirstOrDefault(x => x.Id == id);

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
