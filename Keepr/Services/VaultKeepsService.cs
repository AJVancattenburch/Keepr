namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vs;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vs)
  {
    _repo = repo;
    _vs = vs;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, string userId, Vault vault)
  {
    if (vault.CreatorId == userId)
    {
      newVaultKeep.CreatorId = userId;
      int vaultKeepId = _repo.CreateVaultKeep(newVaultKeep);
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
      return vaultKeep;
    }
    else
    {
      throw new Exception("[UNAUTHORIZED] You are not the creator of this vault!");
    }
  }

  internal string DeleteVaultKeep(string userId, int vaultKeepId)
  {
    VaultKeep original = GetVaultKeepById(vaultKeepId);
    if (original.CreatorId == userId)
    {
      _repo.DeleteVaultKeep(vaultKeepId);
      return $"Successfully Deleted VaultKeep Id #{vaultKeepId}";
    }
    else
    {
      throw new Exception("[UNAUTHORIZED] You are not the creator of this vault!");
    }
  }

  internal List<VaultKeep> GetMyVaultKeeps(string userId)
  {
    List<VaultKeep> myVaultKeeps = _repo.GetMyVaultKeeps(userId);
    return myVaultKeeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception($"[INVALID] VaultKeep Id #{vaultKeepId}");
    }
    return vaultKeep;
  }

  internal List<VaultHasKeep> GetVaultKeepsByVaultId(int vaultId, string userId)
  {
    _vs.GetVaultById(vaultId, userId);
    List<VaultHasKeep> vaultHasKeeps = _repo.GetVaultKeepsByVaultId(vaultId);
    return vaultHasKeeps;
  }
}