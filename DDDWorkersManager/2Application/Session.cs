using DDDWorkersManager._3Domain.Entities;

namespace DDDWorkersManager._2Application
{
    public class Session : ISession
    {
        public int ActiveUserId { get; set; }
        public WorkerRoles WorkerRole { get; set; }
    }
}
