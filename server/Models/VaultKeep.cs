namespace Keepr.Models;

public class VaultKeep
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}

public class VaultHasKeep : Keep
{
  public int VaultKeepId { get; set; }
}