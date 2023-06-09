﻿namespace WorkersAdminV2.Entities
{
    public class Tasks
    {

        private string _technology;

        public static int TotalCount;
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public string Description { get; set; }
        public string Technology
        {
            get
            {
                return _technology;
            }

            set
            {
                _technology = value.ToLower();
            }
        }
        public int? IdWorker { get; set; }

        public Tasks(string name, string description, string technology)
        {
            TotalCount++;
            Id = TotalCount;
            Name = name;
            Status = TaskStatus.ToDo;
            Description = description;
            Technology = technology;
            IdWorker = null;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Status: {Status} | IdWorker: {IdWorker} |";
        }

    }
}
