using System;
using System.Collections.Generic;

namespace DDDWorkersManager._3Domain.Entities.Team
{
    public class Team
    {
        public string Id;
        public string IdManager { get; private set; } = string.Empty;
        public List<string> Technicians { get; private set; }
        public string Name { get; private set; }

        public Team(string name)
        {
            Id = Guid.NewGuid().ToString();
            Technicians = new List<string>();
            Name = name;
        }

        public (bool status, string error) AddTechnician(string idWorker)
        {
            if (IsWorkerInTechnicians(idWorker))
            {
                return (false, "Worker is already in team");
            }
            Technicians.Add(idWorker);
            return (true, null);
        }

        public bool IsWorkerInTechnicians(string idWorker)
        {
            return Technicians.Contains(idWorker);
        }

        public bool IsWorkerInTeam(string idWorker)
        {
            return Technicians.Contains(idWorker) || IdManager == idWorker;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Manager id: {IdManager}";
        }

    }
}
