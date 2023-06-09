﻿using DDDWorkersManager._3Domain;
using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities;
using DDDWorkersManager._3Domain.Entities.Worker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDWorkersManager._2Application
{
    public class WorkersService : IWorkersService
    {
        private readonly IRepositoryItWorker _workersRepository;
        private readonly IRepositoryTasks _tasksRepository;
        private readonly ISession _session;

        public WorkersService(IRepositoryItWorker workersRepository, IRepositoryTasks tasksRepository, ISession session)
        {
            _workersRepository = workersRepository;
            _tasksRepository = tasksRepository;
            _session = session;
        }

        public (bool status, string error) RegisterNewItWorker(
            string name,
            string surname,
            DateTime birthDate,
            int yearsOfExperience,
            List<string> techKnowledge,
            WorkerLevel level
            )
        {

            if (_session.WorkerRole != WorkerRoles.Admin)
            {
                return (false, "not allowed");
            }

            try
            {
                ItWorker newItWorker = new ItWorker(
                    name,
                    surname,
                    birthDate,
                    yearsOfExperience,
                    techKnowledge,
                    level
                    );

                bool status = _workersRepository.Insert(newItWorker);
                string errorMsg = status ? string.Empty : "error when saving to database";
                return (true, string.Empty);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ToString());
                return (false, ex.ToString());
            }

        }

        public bool AssignItWorkerAsTechnician(int idWorker, int idTeam)
        {
            ItWorker worker = _workersRepository.GetById(idWorker);
            if (worker == null)
            {
                return false;
            }
            worker.IdTeam = idTeam;

            return _workersRepository.Update(worker);
        }

        public (bool status, string error) UnregisterItWorker(int idWorker)
        {
            ItWorker worker = _workersRepository.GetById(idWorker);
            if (worker is null)
            {
                return (false, "worker not found");
            }

            Tasks task = _tasksRepository.GetTasksByAssignedWorker(new List<int>() { worker.Id }).FirstOrDefault();
            if (task != null)
            {
                task.IdWorker = null;
                _tasksRepository.Update(task);
                return (true, string.Empty);
            }

            return (true, string.Empty);
        }

        public List<string> GetWorkersByTeamId(int idTeam)
        {
            return _workersRepository.GetByTeamId(idTeam).Select(x => x.ToString()).ToList();
        }

        public string GetWorkerById(int idWorker)
        {
            return _workersRepository.GetById(idWorker).ToString();
        }

        public (bool status, string error) AssignTeamToItWorker(int idWorker, int idTeam)
        {
            var worker = _workersRepository.GetById(idWorker);
            if (worker == null)
            {
                return (false, "worker not found");
            }

            worker.IdTeam = idTeam;
            bool status = _workersRepository.Update(worker);
            string errorMsg = status ? string.Empty : "error when updating in database";

            return (status, errorMsg);
        }

        public bool UnassignItWorkerFromTeam(int idWorker, int idTeam)
        {
            var worker = _workersRepository.GetById(idWorker);
            if (worker.IdTeam == idTeam)
            {
                return _workersRepository.Delete(idWorker);
            }
            return true;
        }

    }
}
