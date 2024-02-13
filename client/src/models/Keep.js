import { Profile } from "./Account.js";

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

export class Keep {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.kept = data.kept
    this.creatorId = data.creatorId
    this.creator = data.creator || new Profile(data.creator)
    this.createdAt = formatTime(new Date(data.createdAt))
    this.updatedAt = formatTime(new Date(data.updatedAt))
  }
}



// id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
// createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
// updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
// name VARCHAR(255) NOT NULL COMMENT 'Name',
// description VARCHAR(500) NOT NULL COMMENT 'Description',
// img VARCHAR(500) NOT NULL COMMENT 'Image',
// creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
// views INT NOT NULL DEFAULT 0,