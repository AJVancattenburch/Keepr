namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth;
  private readonly VaultsService _vs;
  private readonly VaultKeepsService _vks;

  public AccountController(AccountService accountService, Auth0Provider auth, VaultsService vs, VaultKeepsService vks)
  {
    _accountService = accountService;
    _auth = auth;
    _vs = vs;
    _vks = vks;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> myVaults = _vs.GetMyVaults(userInfo.Id);
      return Ok(myVaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultKeeps}")]
  [Authorize]
  public async Task<ActionResult<List<VaultKeep>>> GetMyVaultKeeps()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<VaultKeep> myVaultKeeps = _vks.GetMyVaultKeeps(userId);
      return Ok(myVaultKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userEmail = userInfo.Email;
      Account edited = _accountService.Edit(editData, userEmail);
      return Ok(edited);
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

}
