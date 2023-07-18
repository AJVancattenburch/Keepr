CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL COMMENT 'User Name',
  email VARCHAR(255) NOT NULL COMMENT 'User Email',
  picture VARCHAR(500) NOT NULL COMMENT 'User Picture',
  coverImg VARCHAR(500) NOT NULL COMMENT 'User Cover Image',
  bio TEXT COMMENT 'User Bio'
) default charset utf8mb4 COMMENT '';

ALTER TABLE accounts ADD COLUMN coverImg VARCHAR(255) DEFAULT "";

CREATE TABLE IF NOT EXISTS keeps(
 id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
 createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
 updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
 name VARCHAR(255) NOT NULL COMMENT 'Name',
 description VARCHAR(500) NOT NULL COMMENT 'Description',
 img VARCHAR(500) NOT NULL COMMENT 'Image',
 creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
 views INT NOT NULL DEFAULT 0,

 FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
 id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
 createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
 updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
 name VARCHAR(255) NOT NULL COMMENT 'Name',
 description VARCHAR(500) NOT NULL COMMENT 'Description',
 img VARCHAR(500) NOT NULL COMMENT 'Image',
 isPrivate BOOLEAN NOT NULL DEFAULT false COMMENT 'Is Private',
 creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',

 FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  vaultId INT NOT NULL COMMENT 'Vault Id',
  keepId INT NOT NULL COMMENT 'Keep Id',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
  ) default charset utf8mb4 COMMENT '';


  DROP TABLE vaultKeeps,
  vaults,
  keeps;

  INSERT INTO keeps
  (name, description, img, creatorId)
  VALUES
  ('Sweet Png', 'Check out this sweet png!', 'https://th.bing.com/th/id/R.4199edcef653e72fa3dd9b9bb629f2f5?rik=XNA%2fwfqt8Bw%2bKA&riu=http%3a%2f%2fclipartix.com%2fwp-content%2fuploads%2f2016%2f12%2fAwesome-clip-art-download-image-2.png&ehk=DW9K7kzvxznRArz6RVfyeHrJ56Myr7fcnmsFvRiq71Y%3d&risl=&pid=ImgRaw&r=0', '647fe77f07eaa2e6662ac239');
  /* ('Cool Png', 'This png is so cool man!'. 'https://wallpapercave.com/wp/huUjeWj.jpg', '647fe77f07eaa2e6662ac239'),
  ('Awesome Png', 'This png is awesome!', 'https://th.bing.com/th/id/R.051a04cbabda2e976ccfdf5479eb0b97?rik=g%2bSUpTML10vakg&riu=http%3a%2f%2fclipartmag.com%2fimages%2fawesome-job-clipart-35.png&ehk=Zfn2oYc3vrI8VuqlJlWCDCo%2fHgqyVfWGSFSdiF0iUW8%3d&risl=&pid=ImgRaw&r=0', '647fe77f07eaa2e6662ac239'); */

  INSERT INTO vaults
  (name, description, img, creatorId)
  VALUES
  ('Pngs n Things', 'This is a vault for all my sweet pngs!', 'https://i.imgur.com/1QZ7wYV.png', '647fe77f07eaa2e6662ac239');
    
  INSERT INTO vaultKeeps
  (vaultId, keepId, creatorId)
  VALUES
  (1, 1, '647fe77f07eaa2e6662ac239'),
  (1, 2, '647fe77f07eaa2e6662ac239'),
  (1, 3, '647fe77f07eaa2e6662ac239');

  DELETE
  FROM vaultKeeps
  WHERE vaultId = LAST_INSERT_ID(); */