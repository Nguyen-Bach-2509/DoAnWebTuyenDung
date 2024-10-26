-- New Database: My Job

-- Bảng User: quản lý tài khoản của Khách hàng và Admin
CREATE TABLE [dbo].[User] (
    [UserID] INT IDENTITY(1, 1) NOT NULL,
    [Username] NVARCHAR(255) NOT NULL,
    [Password] NCHAR(50) NOT NULL,
    [UserRole] NCHAR(1) NOT NULL, -- 'A' = Admin, 'C' = Candidate, 'R' = Recruiter
    [Email] NVARCHAR(255) NULL, -- Thêm cột Email
    [Status] NCHAR(1) NOT NULL DEFAULT 'A', -- Trạng thái, 'A' = Active, 'I' = Inactive
    [LastLoginDate] DATETIME NULL, -- Thời gian đăng nhập lần cuối
    [CreateDate] DATETIME NOT NULL DEFAULT GETDATE(), -- Ngày tạo tài khoản
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

-- Bảng Company: lưu thông tin công ty tuyển dụng
CREATE TABLE [dbo].[Company] (
    [CompanyID] INT IDENTITY(1, 1) NOT NULL,
    [CompanyName] NVARCHAR(MAX) NOT NULL,
    [CompanyAddress] NVARCHAR(MAX) NULL,
    [CompanyPhone] NVARCHAR(15) NOT NULL,
    [CompanyEmail] NVARCHAR(MAX) NOT NULL,
    [UserID] INT NOT NULL, -- Liên kết với User (Recruiter)
    [CompanyLogo] NVARCHAR(MAX) NULL, -- Thêm cột CompanyLogo để lưu đường dẫn hình ảnh công ty
    PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

-- Bảng Job: Lưu trữ thông tin các bài đăng tuyển dụng
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

-- Bảng Candidate: lưu thông tin ứng viên
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

-- Bảng Application: lưu thông tin ứng tuyển
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

-- Bảng JobCategory: lưu thông tin danh mục công việc
CREATE TABLE [dbo].[JobCategory] (
    [CategoryID] INT IDENTITY(1, 1) NOT NULL,
    [CategoryName] NVARCHAR(MAX) NOT NULL,
    [JobCount] INT NOT NULL DEFAULT 0, -- Số lượng công việc trong danh mục
    [Description] NVARCHAR(MAX) NULL, -- Mô tả danh mục
    [Status] NCHAR(1) NOT NULL DEFAULT 'A', -- Trạng thái, 'A' = Active, 'I' = Inactive
    [CreateDate] DATETIME NOT NULL DEFAULT GETDATE(), -- Ngày tạo danh mục
    PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

-- Bảng JobCategoryMapping: dùng để liên kết công việc với các danh mục ngành nghề
CREATE TABLE [dbo].[JobCategoryMapping] (
    [JobID] INT NOT NULL,
    [CategoryID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([JobID], [CategoryID]), -- Sử dụng cả 2 cột làm khóa chính
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID]), -- Liên kết với Job
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[JobCategory] ([CategoryID]) -- Liên kết với JobCategory
);

-- Thay đổi quyền sở hữu database
ALTER AUTHORIZATION ON DATABASE::DoAnWeb TO sa;
