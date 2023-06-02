using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities.Team;

namespace DDDWorkersManager._2Application
{
    public class TeamsService
    {
        private readonly IRepositoryTeam _teamsRepository;
        private readonly IRepositoryItWorker _workersRepository;

        public TeamsService(IRepositoryTeam teamsRepository, IRepositoryItWorker workersRepository)
        {
            _teamsRepository = teamsRepository;
            _workersRepository = workersRepository;
        }

        // TODO - DTO
        public bool RegisterNewTeam(string teamName)
        {
            var newTeam = new Team(teamName);
            _teamsRepository.Insert(newTeam);
            return true;
        }

        public Team GetTeamByName(string teamName)
        {
            foreach (var team in Teams)
            {
                if (team.Name == name)
                {
                    return team;
                }
            }
            return null;
        }

        public Team GetTeamById(string id)
        {
            return _teamsRepository.GetById(id);
        }

        public Team GetTeamByWorkerId(int idWorker)
        {
            foreach (var team in Teams)
            {
                if (team.IsWorkerInTeam(idWorker))
                {
                    return team;
                }
            }
            return null;
        }

        public bool DeleteIdWorkerFromTeam(string idWorker, string idTeam)
        {

            var team = _teamsRepository.GetById(idTeam);


            foreach (var team in Teams)
            {
                if (team.Manager.Id == idWorker)
                {
                    team.Manager = null;
                    return true;
                }
                else
                {
                    foreach (var worker in team.Technicians)
                    {
                        if (worker.Id == idWorker)
                        {
                            team.Technicians.Remove(worker);
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
