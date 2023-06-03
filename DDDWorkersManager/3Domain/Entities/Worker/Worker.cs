using DDDWorkersManager._3Domain.Entities;
using System;
using System.Collections.Generic;

namespace DDDWorkersManager._3Domain
{
    public class Worker
    {
        public static int TotalNumber { get; private set; }
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LeaveDate { get; set; }

        public Worker(string name, string surname, DateTime birthDate)
        {
            TotalNumber++;
            Id = TotalNumber;

            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
    }

    public class ItWorker : Worker
    {
        public int IdTeam { get; set; }
        public WorkerLevel Level { get; private set; }
        public int YearsOfExperience { get; private set; }
        public List<string> TechKnowleges { get; set; }

        public ItWorker(string name, string surname, DateTime birthDate, int yearsOfExperience, List<string> techKnowleges, WorkerLevel level) : base(name, surname, birthDate)
        {
            //// TODO
            //if ("es menor que 18")
            //{
            //    throw new ArgumentException("worker must be over 18 in order to be an ItWorker", "birthDate");
            //}

            //throw new InvalidOperationException("worker must be over 18 in order to be an ItWorker");

            YearsOfExperience = yearsOfExperience;
            TechKnowleges = techKnowleges;
            Level = level;
        }

        public (bool status, string error) UpdateLevel(WorkerLevel level)
        {
            if (level == WorkerLevel.Senior && YearsOfExperience < 5)
            {
                return (false, "The worker has not enough experience");
            }

            Level = level;
            return (true, string.Empty);
        }

    }

}
