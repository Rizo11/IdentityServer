using Microsoft.AspNetCore.Mvc;
namespace identity.Controllers;

public class AccountController: Controller
{
    public IActionResult Login() => View();
    
}