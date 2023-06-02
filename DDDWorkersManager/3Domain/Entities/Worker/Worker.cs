using DDDWorkersManager._3Domain.Entities;
using System;
using System.Collections.Generic;

namespace DDDWorkersManager._3Domain
{
    public class Worker
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LeaveDate { get; set; }

        public Worker(string name, string surname, DateTime birthDate)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
    }

    public class ItWorker : Worker
    {
        public WorkerLevel Level { get; private set; }
        public int YearsOfExperience { get; private set; }
        public List<string> TechKnowleges { get; set; }

        public ItWorker(string name, string surname, DateTime birthDate, int yearsOfExperience, List<string> techKnowleges, WorkerLevel level) : base(name, surname, birthDate)
        {
            YearsOfExperience = yearsOfExperience;
            TechKnowleges = techKnowleges;
            Level = level;
        }
    }

}
