using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities;

namespace DDDWorkersManager._2Application
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryItWorker _workersRepository;
        private readonly IRepositoryTeam _teamsRepository;
        public ISession Session { get; set; }

        public AuthService(IRepositoryItWorker workersRepository, IRepositoryTeam teamsRepository, ISession session)
        {
            _workersRepository = workersRepository;
            _teamsRepository = teamsRepository;
            Session = session;
        }

        public (ISession session, string error) AuthenticateUser(int idWorker)
        {
            var worker = _workersRepository.GetById(idWorker);

            if (idWorker != 0 || worker == null)
            {
                return (null, "worker not found");
            }
            else if (idWorker == 0)
            {
                Session.WorkerRole = WorkerRoles.Admin;
                Session.ActiveUserId = idWorker;
                return (Session, string.Empty);
            }
            else
            {
                Session.ActiveUserId = idWorker;
                Session.WorkerRole = _teamsRepository.GetByManagerId(idWorker) is null ? WorkerRoles.Worker : WorkerRoles.Manager;
                return (Session, string.Empty);
            }
        }

        public bool CheckUserAuthorization(WorkerRoles workerRole, string option)
        {
            return false;
        }

    }
}
