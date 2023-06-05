using DDDWorkersManager._2Application;
using DDDWorkersManager._3Domain.Entities;
using DDDWorkersManager._3Domain.Entities.Team;
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
        private readonly IOptionsService _optionsService;
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
            IOptionsService optionsService,
            int maxNumberAttempts)
        {
            _teamService = teamService;
            _workerService = workerService;
            _taskService = taskService;
            _authService = authService;
            _optionsService = optionsService;
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

            if (!IsUserLogged)
            {
                Console.WriteLine("Not authenticated");
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
                    Exit = true;
                    break;

                default:
                    Console.WriteLine("Option not available. Please introduce a number between 1 and 12");
                    break;
            }

        }

        public void PrintMenus()
        {
            Console.WriteLine("==========OPTIONS===========");
            foreach (string option in _optionsService.GetOptions())
            {
                Console.WriteLine(option);
            }
        }

        public void RegisterNewItWorker()
        {
            Console.WriteLine("Introduce the following data in order to create a new It worker:");
            string workerName = AskForString("Name:");
            if (workerName is null)
                return;

            string workerSurname = AskForString("Surname:");
            if (workerSurname is null)
                return;

            DateTime? workerDateOfBirth = AskForDate("Date of birth:");
            if (workerDateOfBirth is null)
                return;

            int? workerYearsOfExperience = AskForInteger("Years of experience:", 1);
            if (workerYearsOfExperience is null)
                return;

            string rawInput = AskForString("Technologies known (write them separated by a coma):");
            if (rawInput == null)
                return;
            List<string> workerTechnologies = rawInput.Split(',').Select(e => e.ToLower()).ToList();
            //var workerTechnologies = rawInput.Split(',').ToList();

            WorkerLevel? workerLevel = AskForWorkerLevel("Introduce the worker's level (Junior, Medium, Senior)");
            if (workerLevel is null)
                return;

            (bool status, string errorMsg) = _workerService.RegisterNewItWorker(
                workerName,
                workerSurname,
                (DateTime)workerDateOfBirth,
                (int)workerYearsOfExperience,
                workerTechnologies,
                (WorkerLevel)workerLevel);

            if (!status)
            {
                Console.WriteLine(errorMsg);
                return;
            }
            Console.WriteLine("User unsuccesfully registered");
        }

        public void RegisterNewTeam()
        {
            string newTeamName = AskForString("Name of the team:");
            if (newTeamName is null)
                return;

            try
            {
                Team newTeam = new Team(newTeamName);
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
            if (taskName is null)
                return;

            string taskDescription = AskForString("Description:");
            if (taskDescription is null)
                return;

            string taskTechnology = AskForString("Technology:");
            if (taskTechnology is null)
                return;

            (bool status, string errorMsg) = _taskService.RegisterNewTask(taskName, taskDescription, taskTechnology);

            if (!status)
            {
                Console.WriteLine(errorMsg);
            }
            Console.WriteLine("Task registered successfully!");
        }

        public void ListTeamNames()
        {
            Console.WriteLine("The team names are:");
            (List<string> teamNames, string error) = _teamService.GetAllTeamNames();

            if (!string.IsNullOrEmpty(error))
            {
                Console.Write(error);
                return;
            }

            if (teamNames.Count() == 0)
            {
                Console.Write("No team names to show");
                return;
            }

            foreach (var teamName in teamNames)
            {
                Console.Write(teamName);
            }
        }

        public void ListTeamMembersByTeamName()
        {
            string teamName = AskForString("Introduce the Team's name:");
            if (teamName is null)
                return;

            Team team = _teamService.GetTeamByName(teamName);
            if (team is null)
            {
                Console.WriteLine("No team found with such a name");
                return;
            }

            List<string> teamMembers = _workerService.GetWorkersByTeamId(team.Id);

            if (teamMembers.Count == 0)
            {
                Console.Write("No team members to show");
                return;
            }

            Console.WriteLine("List of technicians:");

            foreach (var worker in teamMembers)
            {
                Console.WriteLine(worker);
            }

            Console.WriteLine("Manager:");
            Console.WriteLine(_workerService.GetWorkerById((int)team.IdManager));
        }

        public void ListUnassignedTasks()
        {
            (List<string> unassignedTasks, string errorMsg) = _taskService.GetUnassignedTasks();

            if (errorMsg != string.Empty)
            {
                Console.WriteLine(errorMsg);
                return;
            }

            if (unassignedTasks.Count == 0)
            {
                Console.WriteLine("No tasks to show");
                return;
            }

            Console.WriteLine("Unassigned tasks:");
            foreach (string task in unassignedTasks)
            {
                Console.WriteLine(task);
            }
        }

        public void ListTasksAssignmentsByTeamName()
        {
            string teamName = AskForString("Introduce the team's name");
            if (teamName is null)
                return;

            (List<string> assignedTasks, string errorMsg) = _teamService.GetTasksAssignedToTeam(teamName);
            if (errorMsg != string.Empty)
            {
                Console.WriteLine(errorMsg);
                return;
            }

            Console.WriteLine("Tasks assigned to team:");
            foreach (var task in assignedTasks)
            {
                Console.WriteLine(task);
            }
        }

        public void AssignTeamManager()
        {
            int? idWorker = AskForInteger("Introduce the worker's id", 1);
            if (idWorker is null)
                return;

            int? idTeam = AskForInteger("Introduce the team's id", 1);
            if (idTeam is null)
                return;

            (bool status, string error) = _teamService.AssignManager((int)idWorker, (int)idTeam);
            if (!status)
            {
                Console.WriteLine(error);
                return;
            }
            Console.WriteLine("Manager assigned successfully");
        }

        public void AssignTeamTechnician()
        {
            int? idWorker = AskForInteger("Introduce the worker's id", 1);
            if (idWorker is null)
                return;

            int? idTeam = AskForInteger("Introduce the team's id", 1);
            if (idTeam is null)
                return;

            (bool status, string errorMsg) = _teamService.AssignTechnician((int)idWorker, (int)idTeam);
            if (!status)
            {
                Console.WriteLine(errorMsg);
                return;
            }
            Console.WriteLine("Technician added correctly");
        }

        public void AssignTaskToWorker()
        {
            int? idWorker = AskForInteger("Introduce the worker's id", 1);
            if (idWorker is null)
                return;

            int? idTask = AskForInteger("Introduce the task's id", 1);
            if (idTask is null)
                return;

            (bool status, string errorMsg) = _taskService.AssignTaskToItWorker((int)idWorker, (int)idTask);
            if (!status)
            {
                Console.WriteLine(errorMsg);
                return;
            }
            Console.WriteLine("Task assigned to worker correctly");
        }

        public void UnregisterWorker()
        {
            int? idWorker = AskForInteger("Introduce the id of the worker you want to unregister", 1);
            if (idWorker is null)
                return;

            (bool status, string errorMsg) = _workerService.UnregisterItWorker((int)idWorker);

            if (!status)
            {
                Console.WriteLine(errorMsg);
            }

            Console.WriteLine("Worker unregistered succesfully");
        }

        // TODO
        public DateTime? AskForDate(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be dd/MM/yyyy");
            NumberOfAttempts = 0;

            while (NumberOfAttempts < _maxNumberOfAttempts)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    NumberOfAttempts++;
                    Console.WriteLine("Invalid input. Please make sure your input follows the correct date format");
                    Console.WriteLine($"{MaxNumberOfAttempts - NumberOfAttempts} attempts left");
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
                    NumberOfAttempts++;
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                    Console.WriteLine($"{_maxNumberOfAttempts - NumberOfAttempts} attempts left");
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
                    Console.WriteLine($"{_maxNumberOfAttempts - NumberOfAttempts} attempts left");
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

            while (NumberOfAttempts < _maxNumberOfAttempts)
            {
                (decimal validatedInput, string error) = new InputValidator().ParseDecimal(Console.ReadLine(), minimumValue);
                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    NumberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is correct");
                    Console.WriteLine($"{_maxNumberOfAttempts - NumberOfAttempts} attempts left");
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

            while (NumberOfAttempts < _maxNumberOfAttempts)
            {
                (string validatedInput, string error) = new InputValidator().ParseString(Console.ReadLine());
                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    NumberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is a positive integer");
                    Console.WriteLine($"{_maxNumberOfAttempts - NumberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            Exit = true;
            return null;
        }

    }
}
