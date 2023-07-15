namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly Auth0Provider _auth;

  public VaultsController(VaultsService vs, Auth0Provider auth)
  {
    _vs = vs;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      newVault.CreatorId = userInfo.Id;
      Vault vault = _vs.CreateVault(newVault);
      vault.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetVaultById(int vaultId)
  {
    try
    {
      Vault vault = _vs.GetVaultById(vaultId);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> EditVault([FromBody] Vault updatedVault, int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      Vault vault = _vs.EditVault(userId, vaultId, updatedVault);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}