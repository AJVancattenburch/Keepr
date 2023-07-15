namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;
  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  
  internal Keep CreateKeep(Keep newKeep)
  {
    Keep keep =_repo.CreateKeep(newKeep);
    return keep;
  }

  internal Keep EditKeep(string userId1, int keepId, string userId2)
  {
    throw new NotImplementedException();
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repo.GetAllKeeps();
    return keeps;
  }

  internal Keep EditKeep(string userId, int keepId, Keep updateKeep)
  {
    Keep original = GetKeepById(keepId);
    if (original.CreatorId == userId)
    {
    original.Name = updateKeep.Name ?? original.Name;
    original.Description = updateKeep.Description ?? original.Description;
    original.Img = updateKeep.Img ?? original.Img;
    _repo.EditKeep(original);
    return original;
    }
      else
    {
      throw new Exception("You are not the creator of this Keep");
    }
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    List<Keep> keeps = _repo.GetKeepsByProfileId(profileId);
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repo.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception("Invalid Keep Id");
    }
    keep.Views++;
    _repo.EditKeep(keep);
    return keep;
  }

  internal string DeleteKeep(string userId, int keepId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId == userId)
    {
      _repo.DeleteKeep(keepId);
      return $"Successfully Deleted Keep #{keepId}";
    }
    else
    {
      throw new Exception("[UNAUTHORIZED] You are not the creator of this Keep!");
    }
  }
}