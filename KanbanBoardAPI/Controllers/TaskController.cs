using KanbanBoardAPI.Controllers;
using KanbanBoardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Task = KanbanBoardAPI.Models.Task;



namespace ApiHTTProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController<Task>
    {
        public TaskController(KanbanContext context) : base(context)
        {
            
        }

    }
}