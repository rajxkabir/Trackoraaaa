Use Time_Sheet_Management_db

-- Users Table 
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100),
    Email VARCHAR(150) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) CHECK (Role IN ('Admin','Leader','Employee')) NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);



--Teams Table

CREATE TABLE Teams (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    LeaderId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (LeaderId) REFERENCES Users(Id)
);


--Team Mebmers 

CREATE TABLE TeamMembers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TeamId INT NOT NULL,
    UserId INT NOT NULL,
    JoinedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TeamId) REFERENCES Teams(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (TeamId, UserId)
);


--Projects
CREATE TABLE Projects (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    Description TEXT,
    StartDate DATE,
    EndDate DATE,
    Status VARCHAR(20) DEFAULT 'Active',
    CreatedBy INT,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);

--Project = teams 
CREATE TABLE ProjectTeams (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId INT NOT NULL,
    TeamId INT NOT NULL,
    FOREIGN KEY (ProjectId) REFERENCES Projects(Id),
    FOREIGN KEY (TeamId) REFERENCES Teams(Id),
    UNIQUE (ProjectId, TeamId)
);


--Tasks Table 
CREATE TABLE Tasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Description TEXT,
    ProjectId INT NOT NULL,
    AssignedTo INT NOT NULL,
    AssignedBy INT NOT NULL,
    Status VARCHAR(20) DEFAULT 'Pending',
    Priority VARCHAR(20) DEFAULT 'Medium',
    DueDate DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (ProjectId) REFERENCES Projects(Id),
    FOREIGN KEY (AssignedTo) REFERENCES Users(Id),
    FOREIGN KEY (AssignedBy) REFERENCES Users(Id)
);


-- Timesheets Table 
CREATE TABLE Timesheets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    ProjectId INT NOT NULL,
    TaskId INT,
    WorkDate DATE NOT NULL,
    HoursWorked DECIMAL(5,2) NOT NULL,
    Description TEXT,
    Status VARCHAR(20) DEFAULT 'Pending',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,

    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ProjectId) REFERENCES Projects(Id),
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id)
);


--Daily Reports
CREATE TABLE DailyReports (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TeamId INT NOT NULL,
    LeaderId INT NOT NULL,
    ReportDate DATE NOT NULL,
    Summary TEXT,
    Status VARCHAR(20) DEFAULT 'Submitted',
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (TeamId) REFERENCES Teams(Id),
    FOREIGN KEY (LeaderId) REFERENCES Users(Id)
);


--reportsDetails 
CREATE TABLE ReportDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReportId INT NOT NULL,
    TaskId INT,
    Notes TEXT,

    FOREIGN KEY (ReportId) REFERENCES DailyReports(Id),
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id)
);

--Notifications Tble 
CREATE TABLE Notifications (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Message TEXT,
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(Id)
);


--Chat Table

CREATE TABLE Messages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SenderId INT NOT NULL,
    ReceiverId INT NULL,
    TeamId INT NULL,
    Message TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (SenderId) REFERENCES Users(Id),
    FOREIGN KEY (ReceiverId) REFERENCES Users(Id),
    FOREIGN KEY (TeamId) REFERENCES Teams(Id)
);

--Task Comments
CREATE TABLE TaskComments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    Comment TEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (TaskId) REFERENCES Tasks(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);


--attachments

CREATE TABLE Attachments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TaskId INT NOT NULL,
    FilePath VARCHAR(255),
    UploadedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (TaskId) REFERENCES Tasks(Id)
);


CREATE INDEX idx_user_email ON Users(Email);
CREATE INDEX idx_timesheet_user ON Timesheets(UserId);
CREATE INDEX idx_tasks_project ON Tasks(ProjectId);

--Exporting Tsble

CREATE TABLE ExportLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AdminId INT NOT NULL,
    ExportType VARCHAR(50), -- Team / Employee
    ReferenceId INT, -- TeamId or UserId
    FromDate DATE,
    ToDate DATE,
    FileType VARCHAR(10), -- Excel / PDF
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (AdminId) REFERENCES Users(Id)
);