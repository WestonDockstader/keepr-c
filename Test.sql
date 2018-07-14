DROP TABLE IF EXISTS users;
CREATE TABLE users (
    id VARCHAR(255) NOT NULL,
    username VARCHAR(20) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    PRIMARY KEY (id),
    UNIQUE KEY email (email)
);
DROP TABLE IF EXISTS vaults;
CREATE TABLE vaults (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(20) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    INDEX userId (userId),
    FOREIGN KEY (userId)
        REFERENCES users(id)
        ON DELETE CASCADE,  
    PRIMARY KEY (id)
);
DROP TABLE IF EXISTS keeps;
CREATE TABLE keeps (
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(20) NOT NULL,
    description VARCHAR(255) NOT NULL,
    address VARCHAR(255),
    userId VARCHAR(255),
    privatepublic TINYINT(1),
    views INT,
    keeps INT,
    INDEX userId (userId),
    FOREIGN KEY (userId)
        REFERENCES users(id)
        ON DELETE CASCADE,  
    PRIMARY KEY (id)
);


-- SELECT * FROM users;

-- USE THIS LINE FOR GET KEEPS BY VAULTID
-- SELECT * FROM vaultkeeps vk
-- INNER JOIN keeps k ON k.id = vk.keepId 
-- WHERE (vaultId = 2)
