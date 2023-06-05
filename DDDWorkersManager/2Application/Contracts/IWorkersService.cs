using DDDWorkersManager._3Domain.Entities;
using System;
using System.Collections.Generic;

namespace DDDWorkersManager._2Application
{
    public interface IWorkersService
    {
        bool AssignItWorkerAsTechnician(int idWorker, int idTeam);
        (bool status, string error) AssignTeamToItWorker(int idWorker, int idTeam);
        (bool status, string error) RegisterNewItWorker(string name, string surname, DateTime birthDate, int yearsOfExperience, List<string> techKnowledge, WorkerLevel level);
        bool UnassignItWorkerFromTeam(int idWorker, int idTeam);
        (bool status, string error) UnregisterItWorker(int idWorker);
        List<string> GetWorkersByTeamId(int idTeam);
        string GetWorkerById(int idWorker);
    }

}