select *
from [dbo].[User]
ALTER TABLE [dbo].[User]
ALTER COLUMN CreatedAt DATETIME;
ALTER TABLE [dbo].[User]
ADD ConfirmPassword NVARCHAR(255) NULL;
ALTER TABLE [dbo].[User]
ALTER COLUMN Password NVARCHAR(255) NOT NULL;