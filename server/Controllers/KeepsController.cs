namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
  private readonly KeepsService _ks;
  private readonly Auth0Provider _auth;

  public KeepsController(KeepsService ks, Auth0Provider auth)
  {
    _ks = ks;
    _auth = auth;
  }


  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
  {
    try
    {
      Account UserInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      newKeep.CreatorId = UserInfo.Id;
      Keep keep = _ks.CreateKeep(newKeep);
      return Ok(keep);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _ks.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _ks.GetKeepById(keepId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep updateKeep, int keepId)
  {
    try
    {
      Account UserInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = UserInfo.Id;
      Keep keep = _ks.EditKeep(userId, keepId, updateKeep);
      return Ok(keep);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> DeleteKeep(int keepId)
  {
    try
    {
      Account UserInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = UserInfo.Id;
      string deleted = _ks.DeleteKeep(userId, keepId);
      return Ok(deleted);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }
}