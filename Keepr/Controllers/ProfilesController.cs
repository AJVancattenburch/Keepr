namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _ps;
  private readonly KeepsService _ks;
  private readonly VaultsService _vs;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs, Auth0Provider auth)
  {
    _ps = ps;
    _ks = ks;
    _vs = vs;
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

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<Vault> vaults = _vs.GetVaultsByProfileId(profileId, userId);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}