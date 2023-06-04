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

        public Team GetTeamById(int idTeam)
        {
            return _teamsRepository.GetById(idTeam);
        }

        public (bool status, string error) AssignManager(int idWorker, int idTeam)
        {
            var worker = _workersRepository.GetById(idWorker);
            if (worker is null)
            {
                return (false, "no worker found with such an id");
            }

            Team team = GetTeamById(idTeam);
            if (team == null)
            {
                return (false, "no team found with such an ID");
            }

            (bool status, string error) = team.AssignManegerToTeam(worker);
            return (status, error);
        }

        public (bool status, string error) AssignTechnician(int idWorker, int idTeam)
        {
            var worker = _workersRepository.GetById(idWorker);
            if (worker is null)
            {
                return (false, "no worker found with such an id");
            }

            Team team = GetTeamById(idTeam);
            if (team == null)
            {
                return (false, "no team found with such an ID");
            }

            worker.IdTeam = idTeam;
            bool status = _workersRepository.Update(worker);
            string errorMsg = status ? string.Empty : "error to save the changes into de DB"
            return (status, errorMsg);
        }

        public Team GetTeamByName(string teamName)
        {
            return _teamsRepository.GetByName(teamName);
        }

        public (List<string> teamNames, string error) GetAllTeamNames()
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
