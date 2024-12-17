namespace KanbanBoardAPI.Models
{
    public class Column
    {
        public int Id { get; set; }

        public string ?Name { get; set; }

        public int Order { get; set; }

        public List<Task> ?Tasks { get; set; }
    }
}
