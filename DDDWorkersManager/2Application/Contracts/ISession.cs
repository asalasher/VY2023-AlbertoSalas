using DDDWorkersManager._3Domain.Entities;

namespace DDDWorkersManager._2Application
{
    public interface ISession
    {
        int ActiveUserId { get; set; }
        WorkerRoles WorkerRole { get; set; }
    }
}