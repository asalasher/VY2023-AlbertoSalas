using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Team;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DDDWorkersManager._2Application
{
    public class TeamsService
    {
        private readonly IRepositoryTeam _teamsRepository;
        private readonly IRepositoryItWorker _workersRepository;
        private readonly IRepositoryTasks _tasksRepository;

        public TeamsService(IRepositoryTeam teamsRepository, IRepositoryItWorker workersRepository, IRepositoryTasks tasksRepository)
        {
            _teamsRepository = teamsRepository;
            _workersRepository = workersRepository;
            _tasksRepository = tasksRepository;
        }

        // TODO - DTO
        public bool RegisterNewTeam(string teamName)
        {
            var newTeam = new Team(teamName);
            _teamsRepository.Insert(newTeam);
            return true;
        }

        public Team GetTeamMembers(int idTeam)
        {
            return null;
        }

        public List<string> GetAllTeamNames()
        {
            // TODO - cambiar esto para que me venga de la capa de data
            return _teamsRepository.GetAll().Select(x => x.Name).ToList();
        }

        public bool DeleteWorkerFromTeam(int idWorker, int idTeam)
        {
            var worker = _workersRepository.GetById(idWorker);
            if (worker.IdTeam == idTeam)
            {
                return _workersRepository.Delete(idWorker);
            }
            return true;
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
