using DDDWorkersManager._3Domain.Contracts;

namespace DDDWorkersManager._2Application
{
    internal class AuthService
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
                Session.IsActiveUserManager = true;
                Session.ActiveUserId = idWorker;
                return (Session, string.Empty);
            }
            else
            {
                Session.IsActiveUserManager = false;
                Session.ActiveUserId = idWorker;
                Session.IsActiveUserManager = _teamsRepository.GetByManagerId(idWorker) is null ? true : false;

                return (Session, string.Empty);
            }
        }

    }
}
