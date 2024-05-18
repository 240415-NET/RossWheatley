CREATE TABLE Saves (
    SaveId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER,
    SaveDate DATETIMEOFFSET,
    Turns INT,
    Units INT,
    GridConstraintX INT,
    GridConstraintY INT
);

CREATE TABLE PlayerObject (
    SaveId UNIQUEIDENTIFIER PRIMARY KEY,
    IsPlayer BIT,
    Level INT,
    Experience INT,
    Item UNIQUEIDENTIFIER,
	SkillIndex INT,
    Modifier INT,
    SkillPoints INT,
    AttributePoints INT,
    CoordinateX INT,
    CoordinateY INT,
    CharacterClass INT,
    FOREIGN KEY (SaveId) REFERENCES Saves(SaveId)
);

CREATE TABLE Skills (
    SaveId UNIQUEIDENTIFIER,
    SkillIndex INT,
    SkillValue INT,
    PRIMARY KEY (SaveId, SkillIndex),
    FOREIGN KEY (SaveId) REFERENCES Saves(SaveId)
);

CREATE TABLE Attributes (
    SaveId UNIQUEIDENTIFIER,
    AttributeIndex INT,
    AttributeValue INT,
    PRIMARY KEY (SaveId, AttributeIndex),
    FOREIGN KEY (SaveId) REFERENCES Saves(SaveId)
);

CREATE TABLE Tasks (
    TaskId UNIQUEIDENTIFIER PRIMARY KEY,
    SaveId UNIQUEIDENTIFIER,
    UnitCost INT,
    Reward INT,
    Probability INT,
    CoordinateX INT,
    CoordinateY INT,
    FOREIGN KEY (SaveId) REFERENCES Saves(SaveId)
);

CREATE TABLE Encounters (
    EncounterId UNIQUEIDENTIFIER PRIMARY KEY,
    SaveId UNIQUEIDENTIFIER,
    CoordinateX INT,
    CoordinateY INT,
    FOREIGN KEY (SaveId) REFERENCES Saves(SaveId)
);

CREATE TABLE Items (
    ItemId UNIQUEIDENTIFIER PRIMARY KEY,
    SaveId UNIQUEIDENTIFIER,
    SkillIndex INT,
    Modifier INT,
    CoordinateX INT,
    CoordinateY INT,
    FOREIGN KEY (SaveId) REFERENCES Saves(SaveId)
);