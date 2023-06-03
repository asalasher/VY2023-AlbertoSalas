using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities;
using DDDWorkersManager._3Domain.Entities.Team;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DDDWorkersManager._2Application
{
    public class TeamsService : ITeamsService
    {
        private readonly IRepositoryTeam _teamsRepository;
        private readonly IRepositoryItWorker _workersRepository;
        private readonly IRepositoryTasks _tasksRepository;
        private readonly ISession _session;

        public TeamsService(IRepositoryTeam teamsRepository, IRepositoryItWorker workersRepository, IRepositoryTasks tasksRepository, ISession session)
        {
            _teamsRepository = teamsRepository;
            _workersRepository = workersRepository;
            _tasksRepository = tasksRepository;
            _session = session;
        }

        // TODO - DTO
        public (bool status, string error) RegisterNewTeam(string teamName)
        {
            if (_session.WorkerRole != WorkerRoles.Admin)
            {
                return (false, "not allowed");
            }

            var newTeam = new Team(teamName);
            bool status = _teamsRepository.Insert(newTeam);

            return (status, string.Empty);
        }

        public Team GetTeamMembers(int idTeam)
        {
            return null;
        }

        public (List<string>, string error) GetAllTeamNames()
        {
            if (_session.WorkerRole != WorkerRoles.Admin)
            {
                return (null, "not allowed");
            }
            return (_teamsRepository.GetAll().Select(x => x.Name).ToList(), string.Empty);
        }

        public List<Tasks> GetTasksAssignedToTeam(int idTeam)
        {
            var workersId = _workersRepository.GetByTeamId(idTeam).Select(x => x.Id).ToList();
            if (workersId.Count == 0)
            {
                return new List<Tasks>();
            }
            return _tasksRepository.GetTasksByAssignedWorker(workersId);
        }

    }
}
