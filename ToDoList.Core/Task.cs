namespace ToDoList.Core
{
    public class TaskToDo
    {
        public string taskName {  get; set; }
        public bool isDone { get; set; }
        public DateTime dueDate {  get; set; }
        public string priority { get; set; }

        public TaskToDo(string taskName, bool isDone, DateTime dueDate, string priority)
        {
            this.taskName = taskName;
            this.isDone = isDone;
            this.dueDate = dueDate;
            this.priority = priority;
        }

        public override string ToString()
        {
            priority = char.ToUpper(priority[0]) + priority.Substring(1);

            if (isDone == true) 
            {
                return $"[Done] '{taskName}' (until: {dueDate.ToString("dd/MM/yyyy")}) (Priority: {priority})";
            }

            return $"[Not Done] '{taskName}' (until: {dueDate.ToString("dd/MM/yyyy")}) (Priority: {priority})";
        }
    }
}
