import { Profile } from "./Account.js"
import { Keep } from "./Keep.js"

function formatTime(date) {
  let hours = date.getHours()
  let minutes = date.getMinutes()
  let ampm = hours >= 12 ? 'pm' : 'am'
  hours = hours % 12
  hours = hours ? hours : 12
  minutes = minutes < 10 ? '0' + minutes : minutes
  let strTime = hours + ':' + minutes + ' ' + ampm
  return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime
}

export class VaultKeep {
  constructor(data) {
    this.id = data.id
    this.vaultId = data.vaultId
    this.keepId = data.keepId
    this.creatorId = data.creatorId
    this.creator = data.creator ? new Profile(data.creator) : null
    this.keep = data.keep ? new Keep(data.keep) : null
    this.createdAt = formatTime(new Date(data.createdAt))
    this.updatedAt = formatTime(new Date(data.updatedAt))
  }
}

export class VaultHasKeep extends Keep {
  constructor(data) {
    super(data)
    this.vaultKeepId = data.vaultKeepId
  }
}











// public class VaultKeep
// {
//   public int Id { get; set; }
//   public DateTime CreatedAt { get; set; }
//   public DateTime UpdatedAt { get; set; }
//   public int VaultId { get; set; }
//   public int KeepId { get; set; }
//   public string CreatorId { get; set; }
//   public Profile Creator { get; set; }
// }

// public class VaultHasKeep : VaultKeep
// {
//   public int VaultKeepId { get; set; }
// }