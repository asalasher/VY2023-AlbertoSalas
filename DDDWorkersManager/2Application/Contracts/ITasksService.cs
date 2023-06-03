namespace DDDWorkersManager._2Application
{
    public interface ITasksService
    {
        (bool status, string error) AssignTaskToItWorker(int idWorker, int idTask);
        (bool status, string error) RegisterNewTask(string name, string description, string technology);
    }
}