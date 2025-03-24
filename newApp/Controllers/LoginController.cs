using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using newApp.Models;

namespace newApp.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }


    public IActionResult RecevoirJSessionId([FromBody] SessionRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.JSessionId))
        {
            return BadRequest(new { message = "JSESSIONID est requis" });
        }

        HttpContext.Session.SetString("JSESSIONID", request.JSessionId);
        _logger.LogInformation($"JSESSIONID reçu et stocké en session : {request.JSessionId}");

        return Ok(new { message = "JSESSIONID stocké en session", jsessionid = request.JSessionId });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}












public class SessionRequest
{
    public string JSessionId { get; set; }
}
