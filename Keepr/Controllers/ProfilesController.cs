namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _ps;
  private readonly KeepsService _ks;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService ps, KeepsService ks, Auth0Provider auth)
  {
    _ps = ps;
    _ks = ks;
    _auth = auth;
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
  {
    try
    {
      List<Keep> keeps = _ks.GetKeepsByProfileId(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try
    {
      Profile profile = _ps.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }
}