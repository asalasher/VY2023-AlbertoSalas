namespace DDDWorkersManager._3Domain.Entities.Worker
{
    public class Tasks
    {
        public static int TotalNumber { get; private set; } = 1;
        public int Id { get; private set; }
        public string Technology { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; } = TaskStatus.ToDo;
        public int? IdWorker { get; set; }

        public Tasks(string name, string description, string technology)
        {
            TotalNumber++;
            Id = TotalNumber;

            Name = name;
            Description = description;
            Technology = technology.ToLower();
        }

        public (bool status, string error) AssignTaskToItWorker(ItWorker itWorker)
        {
            if (!itWorker.TechKnowleges.Contains(Technology))
            {
                return (false, "itWorker does not have the tech knowledge");
            }

            IdWorker = itWorker.Id;
            return (true, string.Empty);
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Status: {Status} | IdWorker: {IdWorker} |";
        }

    }
}
