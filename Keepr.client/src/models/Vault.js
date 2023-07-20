import { Profile } from "./Account.js"

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

export class Vault {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creatorId = data.creatorId
    this.creator = data.creator ? new Profile(data.creator) : null
    this.createdAt = data.createdAt ? formatTime(new Date(data.createdAt)) : null
    this.updatedAt = data.updatedAt ? formatTime(new Date(data.updatedAt)) : null
  }
}