using DDDWorkersManager._3Domain;
using DDDWorkersManager._3Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._4InfrastructureData
{
    public class ItWorkersRepository : IRepositoryItWorker
    {
        private static readonly List<ItWorker> _workers;

        public bool IsEntityOnDDBB(int id) => _workers.FirstOrDefault(x => x.Id == id) != null;

        public bool Delete(int id)
        {
            for (var i = 0; i < _workers.Count; i++)
            {
                if (_workers[i].Id == id)
                {
                    _workers.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<ItWorker> GetAll() => _workers;

        public ItWorker GetById(int id) => _workers.FirstOrDefault(x => x.Id == id);
        public List<ItWorker> GetByTeamId(int idTeam) => _workers.Where(x => x.IdTeam.Equals(idTeam)).ToList();

        //public List<ItWorker> GetByTeamId(string idTeam)
        //{
        //    var workers = from worker in _workers
        //                  where worker.IdTeam == idTeam
        //                  select worker;

        //    return workers.ToList();
        //}

        public bool Insert(ItWorker entity)
        {
            if (IsEntityOnDDBB(entity.Id))
            {
                return false;
            }
            _workers.Add(entity);
            return true;
        }

        public bool Update(ItWorker entity)
        {
            for (var i = 0; i < _workers.Count; i++)
            {
                if (_workers[i].Id == entity.Id)
                {
                    _workers[i] = entity;
                    return true;
                }
            }
            return false;
        }

    }
}
