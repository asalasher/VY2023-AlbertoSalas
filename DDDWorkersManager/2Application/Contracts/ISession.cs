namespace DDDWorkersManager._2Application
{
    public interface ISession
    {
        int ActiveUserId { get; set; }
        bool IsActiveUserAdmin { get; set; }
        bool IsActiveUserManager { get; set; }
    }
}