using DDDWorkersManager._2Application;
using DDDWorkersManager._5XCutting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DDDWorkersManager._1Presentation
{
    public class Presentation
    {
        private readonly int _maxNumberOfAttempts;
        private readonly ITeamsService _teamService;
        private readonly ITasksService _taskService;
        private readonly IWorkersService _workerService;
        private readonly IAuthService _authService;
        private readonly Session _session;
        public bool Exit { get; private set; } = false;
        public int MaxNumberOfAttempts { get; private set; } = 0;
        public int NumberOfAttempts { get; private set; } = 0;
        public bool IsUserLogged { get; private set; }

        public Presentation(
            ITeamsService teamService,
            IWorkersService workerService,
            ITasksService taskService,
            IAuthService authService,
            int maxNumberAttempts)
        {
            _teamService = teamService;
            _workerService = workerService;
            _taskService = taskService;
            _authService = authService;
            _maxNumberOfAttempts = maxNumberAttempts;
        }

        public void Run()
        {

            LogInUser();
            do
            {
                PrintMenus();
                AskForOption();
            }
            while (!Exit);
        }

        public void LogInUser()
        {

            while (IsUserLogged == false)
            {
                Console.WriteLine("Wellcome to the worker management");
                int? idUser = AskForInteger("Introduce your user id:", 0);

                (ISession session, string error) = _authService.AuthenticateUser((int)idUser);

                if (session != null)
                {
                    // TODO - crear session
                    IsUserLogged = true;
                    break;
                }

            }
        }

        public void AskForOption()
        {
            string chosenOption = AskForString("Introduce an option");

            if (!CheckUserAuth(chosenOption))
            {
                Console.WriteLine("Not authorised");
                return;
            }

            switch (chosenOption)
            {
                case "1":
                    RegisterNewItWorker();
                    break;

                case "2":
                    RegisterNewTeam();
                    break;

                case "3":
                    RegisterNewTask();
                    break;

                case "4":
                    ListTeamNames();
                    break;

                case "5":
                    ListTeamMembersByTeamName();
                    break;

                case "6":
                    ListUnassignedTasks();
                    break;

                case "7":
                    ListTasksAssignmentsByTeamName();
                    break;

                case "8":
                    AssignTeamManager();
                    break;

                case "9":
                    AssignTeamTechnician();
                    break;

                case "10":
                    AssignTaskToWorker();
                    break;

                case "11":
                    UnregisterWorker();
                    break;

                case "12":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Option not available. Please introduce a number between 1 and 12");
                    break;
            }

        }

        public void PrintMenus()
        {
            Console.WriteLine("==========OPTIONS===========");

            var keys = authorizedOptions[(WorkerRoles)UserRole];
            foreach (string key in keys)
            {
                Console.WriteLine($"{key}. {optionNames[key]}");
            }
        }

        public void RegisterNewItWorker()
        {
            Console.WriteLine("Introduce the following data in order to create a new It worker:");
            string workerName = AskForString("Name:");
            if (workerName == null) { return; }

            string workerSurname = AskForString("Surname:");
            if (workerSurname == null) { return; }

            var workerDateOfBirth = AskForDate("Date of birth:");
            if (workerDateOfBirth == null) { return; }

            var workerYearsOfExperience = AskForInteger("Years of experience:", 1);
            if (workerYearsOfExperience == null) { return; }

            string rawInput = AskForString("Technologies known (write them separated by a coma):");
            if (rawInput == null) { return; }
            List<string> workerTechnologies = rawInput.Split(',').Select(e => e.ToLower()).ToList();
            //var workerTechnologies = rawInput.Split(',').ToList();

            var workerLevel = AskForWorkerLevel("Introduce the worker's level (Junior, Medium, Senior)");
            if (workerLevel == null) { return; }

            var newWorker = new ItWorker(workerName, workerSurname, (DateTime)workerDateOfBirth, (int)workerYearsOfExperience, workerTechnologies, (WorkerLevel)workerLevel);

            if (!workerManager.RegisterNewWorker(newWorker))
            {
                Console.WriteLine("User unsuccesfully registered");
            }
            Console.WriteLine("User registered successfully");
        }

        public void RegisterNewTeam()
        {
            Console.WriteLine("Introduce the following data in order to create a new team:");
            string newTeamName = AskForString("Name:");
            if (newTeamName == null) { return; }

            var newTeamManagerId = AskForInteger("Introduce the id of the worker to become manager", 1);
            if (newTeamManagerId == null) { return; }

            var newTeamManager = workerManager.GetWorkerById((int)newTeamManagerId);
            try
            {
                Team newTeam = new Team(newTeamManager, newTeamName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("The worker must be senior in order to be eligible for a manager position");
            }
        }

        public void RegisterNewTask()
        {
            Console.WriteLine("Introduce the following data in order to create a new task:");
            string taskName = AskForString("Name:");
            if (taskName == null) { return; }

            string taskDescription = AskForString("Description:");
            if (taskDescription == null) { return; }

            string taskTechnology = AskForString("Technology:");
            if (taskTechnology == null) { return; }

            var newTask = new Tasks(taskName, taskDescription, taskTechnology);
            if (!taskManager.RegisterNewTask(newTask))
            {
                Console.WriteLine("Task NOT registered successfully!");
            }
            Console.WriteLine("Task registered successfully!");
        }

        public void ListTeamNames()
        {
            Console.WriteLine("The team names are:");
            var allTeams = teamManager.Teams.ToList();
            foreach (var team in allTeams)
            {
                Console.Write(team.Name);
            }
        }

        public void ListTeamMembersByTeamName()
        {
            string teamName;

            if (UserRole == WorkerRoles.Manager)
            {
                teamName = UserTeam.Name;
            }
            else
            {
                teamName = AskForString("Introduce the Team's name:");
                if (teamName == null) { return; }
            }

            Team team = teamManager.GetTeamByName(teamName);
            if (team == null)
            {
                Console.WriteLine("No team found with such a name");
                return;
            }

            Console.WriteLine("List of technicians:");
            foreach (var worker in team.Technicians)
            {
                Console.WriteLine(worker.ToString());
            }

            Console.WriteLine("Manager:");
            Console.WriteLine(team.Manager.ToString());
        }

        public void ListUnassignedTasks()
        {
            Console.WriteLine("Unassigned tasks:");
            var unassignedTasks = taskManager.GetTasksByIdWorker(null);
            foreach (Tasks task in unassignedTasks)
            {
                Console.WriteLine(task.Name);
            }
        }

        public void ListTasksAssignmentsByTeamName()
        {
            Team team;

            if (UserRole == WorkerRoles.Manager || UserRole == WorkerRoles.Worker)
            {
                team = UserTeam;
            }
            else
            {
                string teamName = AskForString("Introduce the team's name");
                if (teamName == null) { return; }
                team = teamManager.GetTeamByName(teamName);
            }

            if (team == null)
            {
                Console.WriteLine("No team found with such a name");
                return;
            }

            var assignedTasks = new List<Tasks>();
            foreach (ItWorker worker in team.Technicians)
            {
                // TODO - check AddRange method
                assignedTasks.AddRange(taskManager.GetTasksByIdWorker(worker.Id));
            }
            assignedTasks.AddRange(taskManager.GetTasksByIdWorker(team.Manager.Id));

            Console.WriteLine("Tasks assigned to team:");
            foreach (Tasks task in assignedTasks)
            {
                Console.WriteLine(task.ToString());
            }
        }

        public void AssignTeamManager()
        {
            int? workerId = AskForInteger("Introduce the worker's id", 1);
            if (workerId == null) { return; }

            ItWorker worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            if (worker.Level != WorkerLevel.Senior)
            {
                Console.WriteLine("The worker must be senior to be a manager");
                return;
            }

            int? teamId = AskForInteger("Introduce the team's id", 1);
            if (teamId == null) { return; }

            Team team = teamManager.GetTeamById((int)teamId);
            if (team == null)
            {
                Console.WriteLine("No team found with such an ID");
                return;
            }
            if (team.Manager != null)
            {
                Console.WriteLine("The team already has a manager");
                return;
            }
        }

        public void AssignTeamTechnician()
        {
            int? workerId = AskForInteger("Introduce the worker's id", 1);
            if (workerId == null) { return; }

            ItWorker worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            int? teamId = AskForInteger("Introduce the team's id", 1);
            if (teamId == null) { return; }

            Team team = teamManager.GetTeamById((int)teamId);
            if (team == null)
            {
                Console.WriteLine("No team found with such an ID");
                return;
            }

            if (!team.AddTechnician(worker))
            {
                Console.WriteLine("Technician was NOT added because already exists");
            }

            Console.WriteLine("Technician added correctly");
        }

        public void AssignTaskToWorker()
        {
            Console.WriteLine("Introduce the following data in order to assign a task");
            int? idWorker;

            if (UserRole != WorkerRoles.Worker)
            {
                idWorker = AskForInteger("Introduce the worker's id", 1);
                if (idWorker == null) { return; }
            }
            else
            {
                idWorker = ActiveUser.Id;
            }

            ItWorker worker = workerManager.GetWorkerById((int)idWorker);
            if (worker == null)
            {
                Console.WriteLine("No worker found with such an id");
                return;
            }

            string taskName = AskForString("Task's name:");
            if (taskName == null) { return; }

            var task = taskManager.GetTaskByName(taskName);
            if (task == null)
            {
                Console.WriteLine("No task found with such a name");
                return;
            }

            if (worker.TechKnowleges.Contains(task.Technology)
                && taskManager.AssignTaskToWorker((int)idWorker, task.Id))
            {
                Console.WriteLine("Task assigned correctly");
                return;
            }

            Console.WriteLine("Worker does not know the technologies required or task is already done");
            Console.WriteLine("Task was not assigned");
        }

        public void UnregisterWorker()
        {
            Console.WriteLine("Introduce the following data in order to unregister a worker");
            var idWorker = AskForInteger("Introduce the worker's id", 1);
            if (idWorker == null) { return; }

            ItWorker worker = workerManager.GetWorkerById((int)idWorker);
            if (worker == null)
            {
                Console.WriteLine("No worker found with such an id");
                return;
            }

            if (!workerManager.UnregisterWorkerById((int)idWorker)
                || !taskManager.DeleteIdWorkerFromTasks((int)idWorker)
                || !teamManager.DeleteIdWorkerFromTeam((int)idWorker))
            {
                Console.WriteLine("Worker NOT unregistered correctly");
            }
            Console.WriteLine("Worker unregistered succesfully");
        }

        // TODO
        public DateTime? AskForDate(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be dd/MM/yyyy");
            numberOfAttempts = 0;

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine("Invalid input. Please make sure your input follows the correct date format");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            Exit = true;
            return null;
        }

        // TODO
        public WorkerLevel? AskForWorkerLevel(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be Senior, Medium or Junior");
            NumberOfAttempts = 0;

            while (NumberOfAttempts < _maxNumberOfAttempts)
            {
                if (WorkerLevel.TryParse(Console.ReadLine(), out WorkerLevel validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            Exit = true;
            return null;
        }


        public int AskForInteger(string consoleText, int minimumValue)
        {
            NumberOfAttempts = 0;
            Console.WriteLine($"{consoleText}. It must be an integer greater or equal to {minimumValue}.");

            while (NumberOfAttempts < _maxNumberOfAttempts)
            {
                (int validatedInput, string error) = new InputValidator().ParseInteger(Console.ReadLine(), minimumValue);
                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    NumberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is correct");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            Exit = true;
            return 0;
        }

        public decimal AskForDecimal(string consoleText, int minimumValue)
        {
            NumberOfAttempts = 0;
            Console.WriteLine($"{consoleText}. It must be a decimal number greater or equal to {minimumValue}.");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                (decimal validatedInput, string error) = new InputValidator().ParseDecimal(Console.ReadLine(), minimumValue);

                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is correct");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            Exit = true;
            return 0;
        }

        public string AskForString(string consoleText)
        {
            NumberOfAttempts = 0;
            Console.WriteLine($"{consoleText}. It must be a valid string");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                (string validatedInput, string error) = new InputValidator().ParseString(Console.ReadLine());

                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is a positive integer");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            Exit = true;
            return null;
        }

    }
}
