using Microsoft.AspNetCore.Mvc;

namespace RedisApi;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repo;

    public PlatformsController(IPlatformRepo repo)
    {
        _repo = repo;
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<Platform> GetPlatformById(string id)
    {
        var platform = _repo.GetPlatformById(id);
        if (platform != null)
            return Ok(platform);
        else
            return NotFound();
    }

    [HttpPost]
    public ActionResult<Platform> Create(Platform platform)
    {
        _repo.CreatePlatform(platform);

        return CreatedAtRoute(nameof(GetPlatformById), new { id = platform.Id }, platform);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> GetPlatforms()
    {
        var list = _repo.GetPlatforms();
        return Ok(list);
    }
}
