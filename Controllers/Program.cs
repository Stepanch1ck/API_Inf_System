
using Microsoft.AspNetCore.Mvc;

public class AController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Test([FromQuery]string id)

}