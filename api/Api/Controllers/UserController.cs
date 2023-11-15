namespace MatMate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<UserController> _logger;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<UserController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationDto registrationDto)
    {
        var newUser = new IdentityUser { UserName = registrationDto.Username, Email = registrationDto.Email };

        var result = await _userManager.CreateAsync(newUser, registrationDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors); // TODO: don't return identity errors directly.
        }

        return Ok("Registration successful");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);

        if (!result.Succeeded)
        {
            return BadRequest(result.ToString());
        }

        return Ok("Login successful");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok("Logout successful");
    }
}