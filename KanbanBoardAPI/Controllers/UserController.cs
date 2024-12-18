using KanbanBoardAPI.Controllers;
using KanbanBoardAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiHTTProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        public UserController(KanbanContext context) : base(context)
        {
            
        }

    }
}