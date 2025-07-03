using System.Text.Json;

namespace ToDoList.Core
{
    public class TaskManager
    {
        public List<TaskToDo> taskList = new List<TaskToDo>();
        private string filePath = "tasks.json";
        public void AddTask(string taskName, bool isDone, DateTime taskDueDate, string taskPriority)
        {
            taskList.Add(new TaskToDo(taskName, isDone, taskDueDate, taskPriority));
        }
        public void RemoveTask(int taskToRemove)
        {
            taskList.RemoveAt(taskToRemove - 1);
        }
        public void MarkTaskAsDone(int taskToMarkAsDone)
        {
            taskList[taskToMarkAsDone - 1].isDone = true;
        }
        public void EditTask(int taskToEdit, string taskNewName)
        {
            taskList[taskToEdit - 1].taskName = taskNewName;
            taskList[taskToEdit - 1].isDone = false;
        }
        public void SaveTasksToFile()
        {
            List<TaskToDo> tasksToSave = new List<TaskToDo>();

            foreach (TaskToDo task in taskList)
            {
                tasksToSave.Add(task);
            }

            string json = JsonSerializer.Serialize(tasksToSave, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        public List<TaskToDo> LoadTasksFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskToDo>();
            }
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskToDo>>(json);
        }
    }
}
