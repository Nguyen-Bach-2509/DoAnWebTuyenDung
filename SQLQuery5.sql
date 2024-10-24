--New Database: MyStore
-- Bang User: quan ly ca tai khoan cua Khach hang va Admin
CREATE TABLE [dbo].[User] (
    [UserID] INT IDENTITY(1, 1) NOT NULL,
    [Username] NVARCHAR(255) NOT NULL,
    [Password] NCHAR(50) NOT NULL,
    [UserRole] NCHAR(1) NOT NULL, -- 'A' = Admin, 'C' = Candidate, 'R' = Recruiter
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

--Bang Company: luu thong tin cong ty tuyen dung
CREATE TABLE [dbo].[Company] (
    [CompanyID] INT IDENTITY(1, 1) NOT NULL,
    [CompanyName] NVARCHAR(MAX) NOT NULL,
    [CompanyAddress] NVARCHAR(MAX) NULL,
    [CompanyPhone] NVARCHAR(15) NOT NULL,
    [CompanyEmail] NVARCHAR(MAX) NOT NULL,
    [UserID] INT NOT NULL, -- Li�n k?t v?i User (Recruiter)
    PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

--Bang Job: L�u tr? th�ng tin c�c b�i ��ng tuy?n d?ng.
CREATE TABLE [dbo].[Job] (
    [JobID] INT IDENTITY(1, 1) NOT NULL,
    [CompanyID] INT NOT NULL,
    [JobTitle] NVARCHAR(MAX) NOT NULL,
    [JobDescription] NVARCHAR(MAX) NOT NULL,
    [JobRequirements] NVARCHAR(MAX) NULL,
    [SalaryRange] NVARCHAR(MAX) NULL,
    [JobLocation] NVARCHAR(MAX) NOT NULL,
    [JobType] NVARCHAR(50) NULL, -- V� d?: Full-time, Part-time, Contract
    [PostDate] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([JobID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

--Bang Candidate: luu thong tin ung vien
CREATE TABLE [dbo].[Candidate] (
    [CandidateID] INT IDENTITY(1, 1) NOT NULL,
    [FullName] NVARCHAR(MAX) NOT NULL,
    [Phone] NVARCHAR(15) NOT NULL,
    [Email] NVARCHAR(MAX) NOT NULL,
    [Address] NVARCHAR(MAX) NULL,
    [CV] NVARCHAR(MAX) NULL, -- ��?ng d?n t?i CV c?a ?ng vi�n
    [UserID] INT NOT NULL, -- Li�n k?t v?i User (Candidate)
    PRIMARY KEY CLUSTERED ([CandidateID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

--Bang Application: luu thong tin ung tuyen
CREATE TABLE [dbo].[Application] (
    [ApplicationID] INT IDENTITY(1, 1) NOT NULL,
    [JobID] INT NOT NULL,
    [CandidateID] INT NOT NULL,
    [ApplicationDate] DATE NOT NULL,
    [Status] NVARCHAR(50) NOT NULL, -- V� d?: 'Pending', 'Reviewed', 'Accepted', 'Rejected'
    PRIMARY KEY CLUSTERED ([ApplicationID] ASC),
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID]),
    FOREIGN KEY ([CandidateID]) REFERENCES [dbo].[Candidate] ([CandidateID])
);

--Bang JobCategory: luu thong tin danh muc cong viec
CREATE TABLE [dbo].[JobCategory] (
    [CategoryID] INT IDENTITY(1, 1) NOT NULL,
    [CategoryName] NVARCHAR(MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

--Bang JobCategoryMapping: D�ng �? li�n k?t c�ng vi?c v?i c�c danh m?c ng�nh ngh?.
CREATE TABLE [dbo].[JobCategoryMapping] (
    [JobID] INT NOT NULL,
    [CategoryID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([JobID], [CategoryID]), -- S? d?ng c? 2 c?t l�m kh�a ch�nh
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID]), -- Li�n k?t v?i Job
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[JobCategory] ([CategoryID]) -- Li�n k?t v?i JobCategory
);

-- Thay �?i quy?n s? h?u database
ALTER AUTHORIZATION ON DATABASE::DoAnWeb TO sa;