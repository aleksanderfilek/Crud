CREATE DATABASE crud;

USE crud;

CREATE TABLE users(
	ID INT AUTO_INCREMENT,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Age TINYINT,
    PRIMARY KEY (ID)
);

INSERT INTO users (FirstName, LastName, Age) VALUES
	('Aleksander', 'Filek', 21),
    ('Barbara', 'Nowak', 42),
    ('Kazimierz', 'Jonek', 27);
        