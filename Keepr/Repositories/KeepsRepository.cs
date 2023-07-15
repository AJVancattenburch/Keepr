namespace Keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep newKeep)
  {
    string sql = @"
    INSERT INTO keeps
    (name, description, img, creatorId)
    VALUES
    (@Name, @Description, @Img, @CreatorId);
    SELECT
    keep.*,
    acct.*
    FROM keeps keep
    JOIN accounts acct ON acct.id = keep.creatorId
    WHERE keep.id = LAST_INSERT_ID()
    ;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, newKeep).FirstOrDefault();
    return keep; 
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id
    ;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, splitOn: "id").ToList();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = @keepId
    GROUP BY (keeps.id)
    ;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { keepId }).FirstOrDefault();
    return keep;
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id
    WHERE keeps.creatorId = @profileId
    GROUP BY (keeps.id)
    ;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { profileId }, splitOn: "id").ToList();
    return keeps;
  }

  internal void EditKeep(Keep original)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description = @Description,
    img = @Img,
    views = @Views
    WHERE id = @Id
    ;";

    _db.Execute(sql, original);
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE id = @keepId
    ;";

    _db.Execute(sql, 
    new { keepId });
  }
}