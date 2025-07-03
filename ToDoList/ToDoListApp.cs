using ToDoList.Core;

namespace ToDoList
{
    public class ToDoListApp
    {
        private TaskManager TaskManager = new TaskManager();
        public void ShowMenu()
        {
            bool isRunning = true;

            while(isRunning)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("TO-DO LIST");
                Console.WriteLine("----------------------");

                Console.WriteLine();

                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Remove task");
                Console.WriteLine("3. Mark task as done");
                Console.WriteLine("4. Edit task");
                Console.WriteLine("5. Show tasks");
                Console.WriteLine("6. Show stats");
                Console.WriteLine("7. Save and exit");

                Console.WriteLine();

                Console.Write("Enter an option: ");
                string userChoice = Console.ReadLine();

                switch(userChoice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        RemoveTask();
                        break;
                    case "3":
                        MarkTaskAsDone();
                        break;
                    case "4":
                        EditTask();
                        break;
                    case "5":
                        ShowTasks();
                        break;
                    case "6":
                        ShowStats();
                        break;
                    case "7":
                        isRunning = SaveAndExit();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please enter an correct option.");
                        Console.WriteLine();
                        break;
                }

            }
        }
        
        public void AddTask()
        {
            string taskName = "";

            while (taskName.Length == 0)
            {
                Console.Clear();
                Console.Write("Enter Task: ");
                taskName = Console.ReadLine();
            }

            string taskPriority = SetPriority();

            Console.Clear();
            DateTime taskDueDate = SetDueDate();

            bool isDone = false;

            Console.Clear();

            TaskManager.AddTask(taskName, isDone, taskDueDate, taskPriority);

            taskPriority = char.ToUpper(taskPriority[0]) + taskPriority.Substring(1);
            Console.WriteLine($"Added: '{taskName}' \nPriority - {taskPriority} \nDue date: {taskDueDate.ToString("dd/MM/yyyy")}");

            Console.WriteLine();
        }

        public void RemoveTask()
        {
            int taskToRemove;

            Console.Clear();

            if (TaskManager.taskList.Count == 0)
            {
                Console.WriteLine("You don't have any tasks to delete.");
                Console.WriteLine();
                return;
            }

            int taskIndex = 1;

            foreach (TaskToDo task in TaskManager.taskList)
            {
                Console.WriteLine($"{taskIndex}. {task}");
                taskIndex++;
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.Write("Enter number of task you want to remove: ");

            try
            {
                taskToRemove = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine($"Task Num. {taskToRemove} removed.");
                Console.WriteLine();

                TaskManager.RemoveTask(taskToRemove);
            }
            catch {
                Console.Clear();
                RemoveTask();
            }
        }

        public void MarkTaskAsDone()
        {
            int taskToMarkAsDone;

            Console.Clear();

            if (TaskManager.taskList.Count == 0)
            {
                Console.WriteLine("You don't have any tasks to mark as done.");
                Console.WriteLine();
                return;
            }

            int taskIndex = 1;

            foreach (TaskToDo task in TaskManager.taskList)
            {
                Console.WriteLine($"{taskIndex}. {task}");
                taskIndex++;
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.Write("Enter number of task you want to mark as done: ");

            try
            {
                taskToMarkAsDone = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine($"Task Num. {taskToMarkAsDone} marked as done.");
                Console.WriteLine();

                TaskManager.MarkTaskAsDone(taskToMarkAsDone);
            }
            catch
            {
                Console.Clear();
                MarkTaskAsDone();
            }
        }

        public void EditTask()
        {
            if (TaskManager.taskList.Count > 0)
            {
                Console.Clear();
                int taskIndex = 1;

                foreach (TaskToDo task in TaskManager.taskList)
                {
                    Console.WriteLine($"{taskIndex}. {task}");
                    taskIndex++;
                }
                Console.WriteLine();
                Console.Write("Which task you want to edit?: ");

                try
                {
                    int taskToEdit = int.Parse(Console.ReadLine());
                    Console.Clear();

                    if (taskToEdit > 0 && taskToEdit <= TaskManager.taskList.Count)
                    {
                        Console.Write("Enter new task: ");
                        string taskNewName = Console.ReadLine();

                        TaskManager.EditTask(taskToEdit, taskNewName);

                        Console.Clear();
                        Console.WriteLine($"Task Num. {taskToEdit} changed to '{taskNewName}'");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Clear();
                        EditTask();
                    }
                }
                catch
                {
                    Console.Clear();
                    EditTask();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You don't have any tasks to edit.");
                Console.WriteLine();
                return;
            }
        }

        public void ShowTasks()
        {
            if(TaskManager.taskList.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Tasks:");
                Console.WriteLine();

                int taskIndex = 1;

                foreach (TaskToDo task in TaskManager.taskList)
                {
                    Console.WriteLine($"{taskIndex}. {task}");
                    taskIndex++;
                }
                Console.WriteLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You don't have any tasks left.");
                Console.WriteLine();
                return;
            }
        }
        
        public void ShowStats()
        {
            TaskToDo closestTask = null;
            DateTime now = DateTime.Now;
            int tasksTotal = 0;
            int tasksDone = 0;
            int tasksNotDone = 0;

            Console.Clear();
            Console.WriteLine("Stats:");
            Console.WriteLine();

            foreach (TaskToDo task in TaskManager.taskList)
            {
                if(task.isDone)
                {
                    tasksDone++;
                }
                else
                {
                    tasksNotDone++;
                }
                tasksTotal++;
            }

            foreach (TaskToDo task in TaskManager.taskList)
            {
                if(task.dueDate >= now)
                {
                    if (closestTask == null || task.dueDate < closestTask.dueDate)
                    {
                        closestTask = task;
                    }
                }
            }

            Console.WriteLine($"Total tasks: {tasksTotal}");
            Console.WriteLine($"Tasks Done: {tasksDone}");
            Console.WriteLine($"Tasks to do: {tasksNotDone}");

            if (closestTask != null)
            {
                Console.WriteLine($"Upcoming task: '{closestTask.taskName}' until: ({closestTask.dueDate.ToString("dd/MM/yyyy")})");
            }
            else
            {
                Console.WriteLine("Upcoming task: none");
            }
            Console.WriteLine();
        }

        public bool SaveAndExit()
        {
            TaskManager.SaveTasksToFile();
            Console.Clear();
            return false;
        }

        public string SetPriority()
        {
            Console.Clear();

            Console.Write("Set priority (low/medium/high): ");
            string priorityName = Console.ReadLine().ToLower();

            switch (priorityName)
            {
                case "low":
                    return "low";
                case "medium":
                    return "medium";
                case "high":
                    return "high";
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter a correct option.");
                    return SetPriority();
            }
        }


        public DateTime SetDueDate()
        {
            DateTime convertedDate;
            DateTime now = DateTime.Now;

            while (true)
            {
                Console.WriteLine("Enter the date by which the task should be completed (DD/MM/YYYY)");
                Console.Write("Enter date: ");
                string userDate = Console.ReadLine();

                if (DateTime.TryParse(userDate, out convertedDate))
                {
                    if (convertedDate < now)
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a date later than the current one.");
                        continue;
                    }

                    break;
                }

                Console.Clear();
                Console.WriteLine("Please enter a correct date.");
            }
            return convertedDate;
        }

        public void LoadTasks()
        {
            TaskManager.taskList = TaskManager.LoadTasksFromFile();
        }
    }
}
