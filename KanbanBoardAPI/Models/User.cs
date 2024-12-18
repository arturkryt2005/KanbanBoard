using KanbanBoardAPI.Interfaces;

namespace KanbanBoardAPI.Models
{
    public class User : IHaveId
    {
        public int Id { get; set; }

        public string ?Name { get; set; }

        public string ?Email { get; set; }
    }
}
