
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
    [UserID] INT NOT NULL, -- Liên kết với User (Recruiter)
    PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

--Bang Job: Lưu trữ thông tin các bài đăng tuyển dụng.
CREATE TABLE [dbo].[Job] (
    [JobID] INT IDENTITY(1, 1) NOT NULL,
    [CompanyID] INT NOT NULL,
    [JobTitle] NVARCHAR(MAX) NOT NULL,
    [JobDescription] NVARCHAR(MAX) NOT NULL,
    [JobRequirements] NVARCHAR(MAX) NULL,
    [SalaryRange] NVARCHAR(MAX) NULL,
    [JobLocation] NVARCHAR(MAX) NOT NULL,
    [JobType] NVARCHAR(50) NULL, -- Ví dụ: Full-time, Part-time, Contract
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
    [CV] NVARCHAR(MAX) NULL, -- Đường dẫn tới CV của ứng viên
    [UserID] INT NOT NULL, -- Liên kết với User (Candidate)
    PRIMARY KEY CLUSTERED ([CandidateID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

--Bang Application: luu thong tin ung tuyen
CREATE TABLE [dbo].[Application] (
    [ApplicationID] INT IDENTITY(1, 1) NOT NULL,
    [JobID] INT NOT NULL,
    [CandidateID] INT NOT NULL,
    [ApplicationDate] DATE NOT NULL,
    [Status] NVARCHAR(50) NOT NULL, -- Ví dụ: 'Pending', 'Reviewed', 'Accepted', 'Rejected'
    PRIMARY KEY CLUSTERED ([ApplicationID] ASC),
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID]),
    FOREIGN KEY ([CandidateID]) REFERENCES [dbo].[Candidate] ([CandidateID])
);

--Bang JobCategory: luu thong tin danh muc cong viec
CREATE TABLE [dbo].[JobCategory] (
    [CategoryID] INT IDENTITY(1, 1) NOT NULL,
    [CategoryName] NVARCHAR(MAX) NOT NULL,
    [Description] NVARCHAR(MAX) NULL, -- Mô tả danh mục
    [Status] BIT NULL DEFAULT 1, -- Trạng thái hoạt động
    [CreatedDate] DATETIME NULL DEFAULT GETDATE(), -- Ngày tạo
    [JobCount] INT NULL, -- Số lượng công việc
    PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

--Bang JobCategoryMapping: Dùng để liên kết công việc với các danh mục ngành nghề.
CREATE TABLE [dbo].[JobCategoryMapping] (
    [JobID] INT NOT NULL,
    [CategoryID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([JobID], [CategoryID]), -- Sử dụng cả 2 cột làm khóa chính
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID]), -- Liên kết với Job
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[JobCategory] ([CategoryID]) -- Liên kết với JobCategory
);

-- Thay đổi quyền sở hữu database
ALTER AUTHORIZATION ON DATABASE::DoAnWeb TO sa;