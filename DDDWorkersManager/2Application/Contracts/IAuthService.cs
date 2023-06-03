namespace DDDWorkersManager._2Application
{
    public interface IAuthService
    {
        ISession Session { get; set; }

        (ISession session, string error) AuthenticateUser(int idWorker);
    }
}