namespace Keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;
  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault newVault)
  {
    string sql = @"
    INSERT INTO vaults
    (name, description, img, isPrivate, creatorId)
    VALUES
    (@Name, @Description, @Img, @IsPrivate, @CreatorId);
    SELECT 
    v.*,
    creator.*
    FROM vaults v
    JOIN accounts creator ON v.creatorId = creator.id
    WHERE v.id = LAST_INSERT_ID();";
    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, creator) => 
    {
      v.Creator = creator;
      return v;
    }, newVault).FirstOrDefault();
    return vault;
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = @"
    DELETE 
    FROM vaults 
    WHERE id = @vaultId 
    LIMIT 1
    ;";
    _db.Execute(sql, new { vaultId });
  }

  internal Vault EditVault(Vault original)
  {
    string sql = @"
    UPDATE
    vaults
    SET
    name = @Name,
    description = @Description,
    img = @Img
    WHERE id = @Id;
    ";
    _db.Execute(sql, original);
    return original;
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    string sql = @"
    SELECT
    *
    FROM vaults
    WHERE vaults.creatorId = @userId;
    ";

    List<Vault> myVaults = _db.Query<Vault>(sql, new { userId }).ToList();
    return myVaults;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
    SELECT
    v.*,
    acct.*
    FROM vaults v
    JOIN accounts acct ON v.creatorId = acct.id
    WHERE v.id = @vaultId
    ;";
    
    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, account) => 
    {
      v.Creator = account;
      return v;
    }, new { vaultId }).FirstOrDefault();
    return vault;
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    *
    FROM vaults
    WHERE 
    vaults.creatorId = @profileId
    ;";
    
    List<Vault> vaults = _db.Query<Vault>(sql, 
    new { profileId }).ToList();
    return vaults;
  }
}