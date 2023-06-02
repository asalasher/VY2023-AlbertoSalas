using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Team;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._4InfrastructureData
{
    public class TeamsRepository : IRepositoryTeam
    {
        private static readonly List<Team> _teams;

        public bool IsEntityOnDDBB(string id) => _teams.FirstOrDefault(x => x.Id == id) != null;

        public bool Delete(string id)
        {
            for (var i = 0; i < _teams.Count; i++)
            {
                if (_teams[i].Id == id)
                {
                    _teams.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<Team> GetAll() => _teams;

        public Team GetById(string id) => _teams.FirstOrDefault(x => x.Id == id);

        public bool Insert(Team entity)
        {
            if (IsEntityOnDDBB(entity.Id))
            {
                return false;
            }
            _teams.Add(entity);
            return true;
        }

        public bool Update(Team entity)
        {
            for (var i = 0; i < _teams.Count; i++)
            {
                if (_teams[i].Id == entity.Id)
                {
                    _teams[i] = entity;
                    return true;
                }
            }
            return false;
        }

    }
}
