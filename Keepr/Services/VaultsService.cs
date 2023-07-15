namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;
  private readonly Auth0Provider _auth;
  
  public VaultsService(VaultsRepository repo, Auth0Provider auth)
  {
    _repo = repo;
    _auth = auth;
  }


  internal Vault CreateVault(Vault newVault)
  {
    Vault vault = _repo.CreateVault(newVault);
    return vault;
  }

  internal string DeleteVault(string userId, int vaultId)
  {
    Vault original = GetVaultById(vaultId, userId);
    if (original.CreatorId == userId)
    {
      _repo.DeleteVault(vaultId);
      return "Successfully Deleted Vault";
    }
    else
    {
      throw new Exception("[UNAUTHORIZED] You are not the creator of this vault!");
    }
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

  internal List<Vault> GetVaultsByProfileId(string userId, string profileId)
  {
    List<Vault> vaults = _repo.GetVaultsByProfileId(profileId);
    List<Vault> filteredVaults = vaults.FindAll(v => 
      v.CreatorId != userId && v.IsPrivate == false
      );

    return filteredVaults;
  }

  internal Vault GetVaultById(int vaultId, string UserId)
  {
    Vault vault = _repo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Invalid Id #{vaultId}");
    }
    if (vault.IsPrivate == true && vault.CreatorId != UserId)
    {
      throw new Exception("[RESTRICTED] This is another user's private vault!");
    }
    return vault; // Mick if you see this in my project please know I did not copy and paste this from you but am writing this comment as a joke ;)
  }
  
}