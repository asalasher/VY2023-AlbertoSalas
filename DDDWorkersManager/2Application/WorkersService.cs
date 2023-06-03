using DDDWorkersManager._3Domain;
using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities;
using System;
using System.Collections.Generic;

namespace DDDWorkersManager._2Application
{
    internal class WorkersService : IWorkersService
    {
        private readonly IRepositoryTeam _teamsRepository;
        private readonly IRepositoryItWorker _workersRepository;
        private readonly ISession _session;

        public WorkersService(IRepositoryTeam teamsRepository, IRepositoryItWorker workersRepository, ISession session)
        {
            _teamsRepository = teamsRepository;
            _workersRepository = workersRepository;
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

        public bool UnregisterItWorker(int idWorker)
        {
            return _workersRepository.Delete(idWorker);
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
