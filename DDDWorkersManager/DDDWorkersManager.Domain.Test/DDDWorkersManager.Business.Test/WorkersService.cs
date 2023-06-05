using DDDWorkersManager._2Application;
using DDDWorkersManager._3Domain;
using DDDWorkersManager._3Domain.Contracts;
using DDDWorkersManager._3Domain.Entities;
using DDDWorkersManager._4InfrastructureData;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DDDWorkersManager.Business.Test
{
    public class WorkersServiceTestSuite
    {
        [Fact]
        public void GetWorkersByTeamId_positiveInput_returnItWorker()
        {
            // Arrange

            //  Realizamos el mock del objecto en cuestion
            Mock<IRepositoryItWorker> workerMock = new Mock<IRepositoryItWorker>();
            ItWorker simulatedWorker = new ItWorker("alberto", "salas", new System.DateTime(1989, 9, 16), 1, new List<string> { "java" }, WorkerLevel.Junior);
            workerMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(simulatedWorker);

            IRepositoryItWorker worker = workerMock.Object;

            var session = new Session();
            session.WorkerRole = WorkerRoles.Admin;
            session.ActiveUserId = 0;

            WorkersService workersService = new WorkersService(new TeamsRepository(), new ItWorkersRepository(), session);

            // Act
            List<string> workers = workersService.GetWorkersByTeamId(1);

            // Assert
            Assert.Equal(1, workers.Count);

        }
    }
}
