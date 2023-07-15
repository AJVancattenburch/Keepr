namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;
  
  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }


  internal Vault CreateVault(Vault newVault)
  {
    Vault vault = _repo.CreateVault(newVault);
    return vault;
  }

  internal Vault EditVault(string userId, int vaultId, Vault newVault)
  {
    Vault original = GetVaultById(vaultId, userId);
    if (original.CreatorId == userId)
    {
      original.Name = newVault.Name ?? original.Name;
      original.Description = newVault.Description ?? original.Description;
      original.Img = newVault.Img ?? original.Img;

      return _repo.EditVault(original);
      
    }
    else
    {
      throw new Exception("[UNAUTHORIZED] You are not the creator of this vault!");
    }
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    List<Vault> myVaults = _repo.GetMyVaults(userId);
    return myVaults;
  }

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Invalid Id #{vaultId}");
    }
    return vault; // Mick if you see this in my project please know I did not copy and paste this from you but am writing this comment as a joke ;)
  }
}