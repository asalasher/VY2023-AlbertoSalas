using DDDWorkersManager._3Domain.Entities.Team;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;

namespace DDDWorkersManager._2Application
{
    public interface ITeamsService
    {
        (List<string> teamNames, string error) GetAllTeamNames();
        List<Tasks> GetTasksAssignedToTeam(int idTeam);
        Team GetTeamMembers(int idTeam);
        (bool status, string error) RegisterNewTeam(string teamName);
        Team GetTeamByName(string teamName);
        Team GetTeamById(int idTeam);
        (bool status, string error) AssignManager(int idWorker, int idTeam);
        (bool status, string error) AssignTechnician(int idWorker, int idTeam);
    }
}