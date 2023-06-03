namespace DDDWorkersManager._3Domain.Entities.Team
{
    public class Team
    {
        public static int TotalNumber { get; private set; } = 0;
        public int Id { get; private set; }
        public int? IdManager { get; private set; }
        public string Name { get; private set; }

        public Team(string name)
        {
            TotalNumber++;
            Id = TotalNumber;

            Name = name;
        }

        public (bool status, string error) AssignManegerToTeam(ItWorker itWorker)
        {
            if (IdManager is null)
            {
                return (false, "the team already has a manager assigned");
            }

            if (itWorker.Level != WorkerLevel.Senior)
            {
                return (false, "itWorker does not have the level required");
            }

            IdManager = itWorker.Id;
            return (true, string.Empty);
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Manager id: {IdManager}";
        }

    }
}
