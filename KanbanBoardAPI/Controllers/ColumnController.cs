using KanbanBoardAPI.Controllers;
using KanbanBoardAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiHTTProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : BaseController<Column>
    {
        public ColumnController(KanbanContext context) : base(context)
        {
            
        }

    }
}