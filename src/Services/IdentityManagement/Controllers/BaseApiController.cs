using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}
