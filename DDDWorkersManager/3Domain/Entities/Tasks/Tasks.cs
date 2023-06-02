using System;

namespace DDDWorkersManager._3Domain.Entities.Worker
{
    public class Tasks
    {
        public string Technology { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; } = TaskStatus.ToDo;
        public string IdWorker { get; private set; } = string.Empty;

        public Tasks(string name, string description, string technology)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            Technology = technology.ToLower();
        }

        public (bool status, string error) AssignTaskToWorker(string idWorker)
        {
            if (IdWorker == string.Empty)
            {
                IdWorker = idWorker;
                return (true, string.Empty);
            }
            return (false, "Task already is assigned to a worker");
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Status: {Status} | IdWorker: {IdWorker} |";
        }

    }
}
