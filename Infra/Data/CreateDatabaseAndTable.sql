create database DriveWatch

use DriveWatch

CREATE TABLE VehicleAccesses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Plate NVARCHAR(20) NOT NULL,
    DriverName NVARCHAR(100) NOT NULL,
    VehicleType NVARCHAR(50) NOT NULL,
    PeopleCount INT NOT NULL,
    Observations NVARCHAR(500) NULL,
    EntryTime DATETIME NOT NULL,
    ExitTime DATETIME NULL
);