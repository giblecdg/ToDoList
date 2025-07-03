namespace ToDoList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ToDoListApp ToDoListApp = new ToDoListApp();
            ToDoListApp.LoadTasks();
            ToDoListApp.ShowMenu();
        }
    }
}
