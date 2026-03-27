Use Time_Sheet_Management_db

--Users Demo Entriies
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role)
    VALUES (
        CONCAT('User', @i),
        'Test',
        CONCAT('user', @i, '@gmail.com'),
        'hashedpassword',
        CASE 
            WHEN @i <= 5 THEN 'Admin'
            WHEN @i <= 10 THEN 'Leader'
            ELSE 'Employee'
        END
    );

    SET @i = @i + 1;
END;
GO

--Teams Demo Entries
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Teams (Name, LeaderId)
    VALUES (
        CONCAT('Team ', @i),
        ((@i - 1) % 10) + 1  -- Leader from first 10 users
    );

    SET @i = @i + 1;
END;
GO

--Team Memmber DEMO Entries
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO TeamMembers (TeamId, UserId)
    VALUES (
        ((@i - 1) % 10) + 1,
        @i
    );

    SET @i = @i + 1;
END;
GO

--Projects  Demo Entriies
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Projects (Name, Description, StartDate, EndDate, CreatedBy)
    VALUES (
        CONCAT('Project ', @i),
        'Sample project description',
        GETDATE(),
        DATEADD(DAY, 30, GETDATE()),
        1
    );

    SET @i = @i + 1;
END;
GO

--PROJECT TEAMS Demo Entriies
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO ProjectTeams (ProjectId, TeamId)
    VALUES (
        @i,
        ((@i - 1) % 10) + 1
    );

    SET @i = @i + 1;
END;
GO

--TASKS Demo Entries
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Tasks (Title, Description, ProjectId, AssignedTo, AssignedBy, DueDate)
    VALUES (
        CONCAT('Task ', @i),
        'Task description',
        ((@i - 1) % 10) + 1,
        ((@i - 1) % 20) + 11, -- employees
        ((@i - 1) % 5) + 6,  -- leaders
        DATEADD(DAY, 7, GETDATE())
    );

    SET @i = @i + 1;
END;
GO

--TIMESHEETS Demo Entries
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Timesheets (UserId, ProjectId, TaskId, WorkDate, HoursWorked, Description)
    VALUES (
        ((@i - 1) % 20) + 11,
        ((@i - 1) % 10) + 1,
        ((@i - 1) % 30) + 1,
        DATEADD(DAY, -@i, GETDATE()),
        8,
        'Worked on task'
    );

    SET @i = @i + 1;
END;
GO

--DAILY REPORTS Demo Entries
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO DailyReports (TeamId, LeaderId, ReportDate, Summary)
    VALUES (
        ((@i - 1) % 10) + 1,
        ((@i - 1) % 5) + 6,
        GETDATE(),
        'Daily progress summary'
    );

    SET @i = @i + 1;
END;
GO

--REPORT DETAILS Demo Entriies
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO ReportDetails (ReportId, TaskId, Notes)
    VALUES (
        ((@i - 1) % 30) + 1,
        ((@i - 1) % 30) + 1,
        'Task progress noted'
    );

    SET @i = @i + 1;
END;
GO

--NOTIFICATIONS Demo Entries
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Notifications (UserId, Message)
    VALUES (
        ((@i - 1) % 30) + 1,
        'Notification message'
    );

    SET @i = @i + 1;
END;
GO

--MESSAGES Demo Entriies
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO Messages (SenderId, ReceiverId, Message)
    VALUES (
        ((@i - 1) % 30) + 1,
        ((@i) % 30) + 1,
        'Hello message'
    );

    SET @i = @i + 1;
END;
GO



