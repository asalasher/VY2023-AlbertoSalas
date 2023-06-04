using DDDWorkersManager._3Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._2Application
{
    public class OptionsService : IOptionsService
    {
        private readonly Session _session;

        private readonly Dictionary<WorkerRoles, string[]> authorizedOptions = new Dictionary<WorkerRoles, string[]>(){
                { WorkerRoles.Admin, new string[]{"1","2","3","4","5","6","7","8","9","10","11","12"} },
                { WorkerRoles.Manager, new string[] { "5", "6", "7", "9", "10", "12" } },
                { WorkerRoles.Worker, new string[] { "6", "7", "10", "12" } },
        };

        private readonly Dictionary<string, string> _optionNumbers = new Dictionary<string, string>(){
                {"1", "Register new IT worker"},
                {"2", "Register new team"},
                {"3", "Register new task (unassigned to anyone)"},
                {"4", "List all team names"},
                {"5", "List team members by team name"},
                {"6", "List unassigned tasks"},
                {"7", "List tasks assignments by team name"},
                {"8", "Assign IT worker to a team as manager"},
                {"9", "Assign IT worker to a team as technician"},
                {"10", "Assign task to IT worker"},
                {"11", "Unregister worker"},
                {"12", "Exit"},
        };

        public OptionsService(Session session)
        {
            _session = session;
        }

        public List<string> GetOptions()
        {
            WorkerRoles userRole = _session.WorkerRole;
            var allowedOptionNumbers = authorizedOptions[userRole];
            return allowedOptionNumbers.Select(x => ($"{x} - {_optionNumbers[x]}")).ToList();
        }

    }
}
