using System.Collections.Generic;
using WorkersAdminV2.Entities;

namespace WorkersAdminV2
{

    public interface ITaskManager
    {
        bool RegisterNewTask(Tasks newtask);
        List<Tasks> GetTasksByIdWorker(int? idWorker);
        bool AssignTaskToWorker(int idWorker, int idTask);
        Tasks GetTaskByName(string taskName);
        bool DeleteIdWorkerFromTasks(int idWorker);
    }

    public interface IWorkerManager
    {
        ItWorker GetWorkerById(int id);
        bool RegisterNewWorker(ItWorker worker);
        bool UnregisterWorkerById(int idWorker);
    }

    public interface ITeamManager
    {
        List<Team> Teams { get; set; }
        bool RegisterNewTeam(Team team);
        Team GetTeamByName(string name);
        Team GetTeamById(int id);
        Team GetTeamByWorkerId(int idWorker);
        bool DeleteIdWorkerFromTeam(int idWorker);
    }

}
