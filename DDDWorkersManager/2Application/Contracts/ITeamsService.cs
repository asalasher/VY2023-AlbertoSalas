using DDDWorkersManager._3Domain.Entities.Team;
using DDDWorkersManager._3Domain.Entities.Worker;
using System.Collections.Generic;

namespace DDDWorkersManager._2Application
{
    public interface ITeamsService
    {
        (bool status, List<string>) GetAllTeamNames();
        List<Tasks> GetTasksAssignedToTeam(int idTeam);
        Team GetTeamMembers(int idTeam);
        (bool status, string error) RegisterNewTeam(string teamName);
    }
}