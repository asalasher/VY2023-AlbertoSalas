using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkersAdminV1.Entities;

namespace WorkersAdminV1.Managers
{
    public class TaskManager
    {

        private List<Tasks> Tasks { get; set; }
        public TaskManager()
        {
            Tasks = new List<Tasks>();
        }

        public bool RegisterNewTask(Tasks newTask)
        {
            Tasks.Add(newTask);
            return true;
        }

        public List<Tasks> GetTasksByIdWorker(int? idWorker)
        {
            var tasks = new List<Tasks>();

            foreach (var task in Tasks)
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
            foreach (var task in Tasks)
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
            foreach (var task in Tasks)
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
