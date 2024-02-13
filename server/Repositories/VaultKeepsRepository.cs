namespace Keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateVaultKeep(VaultKeep newVaultKeep)
  {
    string sql = @"
    INSERT INTO vaultKeeps
    (creatorId, vaultId, keepId, createdAt, updatedAt)
    VALUES
    (@CreatorId, @VaultId, @KeepId, @CreatedAt, @UpdatedAt);
    SELECT 
    LAST_INSERT_ID()
    ;";

    return _db.ExecuteScalar<int>(sql, newVaultKeep);
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = @"
    DELETE 
    FROM vaultKeeps 
    WHERE id = @vaultKeepId 
    LIMIT 1
    ;";
    
    _db.Execute(sql, 
    new { vaultKeepId });
  }

  internal List<VaultKeep> GetMyVaultKeeps(string userId)
  {
    string sql = @"
    SELECT
    *
    FROM vaultKeeps
    WHERE 
    vaultKeeps.creatorId = @userId
    ;";

    List<VaultKeep> myVaultKeeps = _db.Query<VaultKeep>(sql, 
    new { userId }).ToList();
    return myVaultKeeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT 
    *
    FROM 
    vaultKeeps
    WHERE 
    vaultKeeps.id = @vaultKeepId
    ;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, 
    new { vaultKeepId }).FirstOrDefault();
    return vaultKeep;
  }

  internal List<VaultHasKeep> GetVaultKeepsByVaultId(int vaultId)
  {

    string sql = @"
    SELECT 
    k.*,
    vk.id AS vaultKeepId,
    acct.*
    FROM vaultKeeps vk
    JOIN keeps k ON k.id = vk.keepId
    JOIN accounts acct ON acct.id = k.creatorId
    WHERE vk.vaultId = @vaultId
    ;";

    List<VaultHasKeep> vaultKeeps = _db.Query<VaultHasKeep, Account, VaultHasKeep>(sql, 
    (vk, acct) =>
    {
      vk.Creator = acct;
      return vk;
    }, new { vaultId }).ToList();
    return vaultKeeps;
  }


}