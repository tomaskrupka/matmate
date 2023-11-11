    namespace MatMate.Api.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class BonjourController : ControllerBase
    {
        [HttpGet("bonjour")]
        public IActionResult GetBonjour()
        {
            return Ok("bonjour");
        }
    }