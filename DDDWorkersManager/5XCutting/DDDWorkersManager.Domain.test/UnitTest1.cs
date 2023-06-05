using DDDWorkersManager._3Domain;
using DDDWorkersManager._3Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace DDDWorkersManager.Domain.test
{
    public class NameOfClassTestSuite // nombre de la clase que se va a testear
    {
        [Fact]
        public void UpdateWorkerLevel_InputSenior_ReturnNegative() // un metodo por cada invocacion
        {
            // Arrange
            ItWorker worker = new ItWorker("alberto", "salas", new DateTime(1989, 9, 16), 1, new List<string> { "java" }, WorkerLevel.Junior);

            // Act
            (bool status, string errorMsg) = worker.UpdateLevel(WorkerLevel.Senior);

            // Assert
            Assert.True(status);
        }
    }
}
