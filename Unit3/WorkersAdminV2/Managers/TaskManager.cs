using System.Collections.Generic;
using WorkersAdminV2.Entities;

namespace WorkersAdminV2.Managers
{
    public class TaskManager: ITaskManager
    {

        private List<Tasks> tasks;
        public TaskManager()
        {
            tasks = new List<Tasks>();
        }

        public bool RegisterNewTask(Tasks newTask)
        {
            tasks.Add(newTask);
            return true;
        }

        public List<Tasks> GetTasksByIdWorker(int? idWorker)
        {
            var tasks = new List<Tasks>();

            foreach (var task in tasks)
            {
                if (task.IdWorker == idWorker)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }
        public bool AssignTaskToWorker(int idWorker, int idTask)
        {
            foreach (var task in tasks)
            {
                if (task.Id == idTask && task.Status != TaskStatus.Done)
                {
                    task.IdWorker = idWorker;
                    return true;
                }
            }

            return false;
        }

        public Tasks GetTaskByName(string taskName)
        {
            foreach (var task in tasks)
            {
                if (task.Name == taskName)
                {
                    return task;
                }
            }
            return null;
        }

        public bool DeleteIdWorkerFromTasks(int idWorker)
        {
            var tasks = GetTasksByIdWorker(idWorker);
            foreach (var task in tasks)
            {
                if (task.IdWorker == idWorker)
                {
                    task.IdWorker = null;
                    return true;
                }
            }
            return false;
        }

    }
}
