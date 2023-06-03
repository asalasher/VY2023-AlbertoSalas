namespace DDDWorkersManager._2Application
{
    public class Session : ISession
    {
        public bool IsActiveUserAdmin { get; set; } = false;
        public int ActiveUserId { get; set; }
        public bool IsActiveUserManager { get; set; } = false;
    }
}
