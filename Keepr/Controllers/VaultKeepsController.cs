namespace Keepr.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vks;
  private readonly VaultsService _vs;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vks, VaultsService vs, Auth0Provider auth)
  {
    _vks = vks;
    _vs = vs;
    _auth = auth;
  }

  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      Vault vault = _vs.GetVaultById(newVaultKeep.VaultId, userId);
      VaultKeep vaultKeep = _vks.CreateVaultKeep(newVaultKeep, userId, vault);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string deleted = _vks.DeleteVaultKeep(userId, vaultKeepId);
      return Ok(deleted);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}