using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
