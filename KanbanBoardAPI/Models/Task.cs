using KanbanBoardAPI.Interfaces;
using System.Data.Common;

namespace KanbanBoardAPI.Models
{
    public class Task : IHaveId
    {
        public int Id { get; set; }

        public string ?Title { get; set; }

        public string ?Description { get; set; }

        public int ColumnId { get; set; }

        public Column ?Column { get; set; }

        public string ?Assignee { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
