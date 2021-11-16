CREATE TABLE [dbo].[Users](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.Users', 'LoginName') IS NULL
	ALTER TABLE dbo.[Users] ADD LoginName nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Users', 'Pwd') IS NULL
	ALTER TABLE dbo.[Users] ADD Pwd nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Users', 'Status') IS NULL
	ALTER TABLE dbo.[Users] ADD Status nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Users', 'CompanyID') IS NULL
	ALTER TABLE dbo.[Users] ADD CompanyID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.Users', 'StoreID') IS NULL
	ALTER TABLE dbo.[Users] ADD StoreID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.Users', 'EmpplyeeName') IS NULL
	ALTER TABLE dbo.[Users] ADD EmpplyeeName nvarchar(100) NULL
GO
IF COL_LENGTH ('dbo.Users', 'Sex') IS NULL
	ALTER TABLE dbo.[Users] ADD Sex nvarchar(2) NULL
GO
IF COL_LENGTH ('dbo.Users', 'Picture') IS NULL
	ALTER TABLE dbo.[Users] ADD Picture nvarchar(200) NULL
GO
IF COL_LENGTH ('dbo.Users', 'OnBoardDate') IS NULL
	ALTER TABLE dbo.[Users] ADD OnBoardDate datetime NULL
GO
IF COL_LENGTH ('dbo.Users', 'CreateTime') IS NULL
	ALTER TABLE dbo.[Users] ADD CreateTime datetime NULL
GO
IF COL_LENGTH ('dbo.Users', 'Position') IS NULL
	ALTER TABLE dbo.[Users] ADD Position nvarchar(200) NULL
GO


CREATE TABLE [dbo].[Roles](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.Roles', 'Name') IS NULL
	ALTER TABLE dbo.[Roles] ADD Name nvarchar(50) NULL
GO


CREATE TABLE [dbo].[RoleAuthMap](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.RoleAuthMap', 'RoleID') IS NULL
	ALTER TABLE dbo.[RoleAuthMap] ADD RoleID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.RoleAuthMap', 'Auth') IS NULL
	ALTER TABLE dbo.[RoleAuthMap] ADD Auth nvarchar(50) NULL
GO


CREATE TABLE [dbo].[UsersRolesMap](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.UsersRolesMap', 'UserID') IS NULL
	ALTER TABLE dbo.[UsersRolesMap] ADD UserID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.UsersRolesMap', 'RoleName') IS NULL
	ALTER TABLE dbo.[UsersRolesMap] ADD RoleName nvarchar(50) NULL
GO


CREATE TABLE [dbo].[Members](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.Members', 'Phone') IS NULL
	ALTER TABLE dbo.[Members] ADD Phone nvarchar(11) NULL
GO
IF COL_LENGTH ('dbo.Members', 'NickName') IS NULL
	ALTER TABLE dbo.[Members] ADD NickName nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Members', 'Sex') IS NULL
	ALTER TABLE dbo.[Members] ADD Sex nvarchar(2) NULL
GO
IF COL_LENGTH ('dbo.Members', 'Picture') IS NULL
	ALTER TABLE dbo.[Members] ADD Picture nvarchar(200) NULL
GO
IF COL_LENGTH ('dbo.Members', 'CreateTime') IS NULL
	ALTER TABLE dbo.[Members] ADD CreateTime datetime NULL
GO


CREATE TABLE [dbo].[Companys](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.Companys', 'CompanyName') IS NULL
	ALTER TABLE dbo.[Companys] ADD CompanyName nvarchar(300) NULL
GO
IF COL_LENGTH ('dbo.Companys', 'Province') IS NULL
	ALTER TABLE dbo.[Companys] ADD Province nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Companys', 'City') IS NULL
	ALTER TABLE dbo.[Companys] ADD City nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Companys', 'District') IS NULL
	ALTER TABLE dbo.[Companys] ADD District nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Companys', 'Address') IS NULL
	ALTER TABLE dbo.[Companys] ADD Address nvarchar(200) NULL
GO
IF COL_LENGTH ('dbo.Companys', 'ContractName') IS NULL
	ALTER TABLE dbo.[Companys] ADD ContractName nvarchar(300) NULL
GO
IF COL_LENGTH ('dbo.Companys', 'ContractPhone') IS NULL
	ALTER TABLE dbo.[Companys] ADD ContractPhone nvarchar(20) NULL
GO


CREATE TABLE [dbo].[Stores](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.Stores', 'CompanyID') IS NULL
	ALTER TABLE dbo.[Stores] ADD CompanyID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.Stores', 'StoreName') IS NULL
	ALTER TABLE dbo.[Stores] ADD StoreName nvarchar(200) NULL
GO
IF COL_LENGTH ('dbo.Stores', 'Province') IS NULL
	ALTER TABLE dbo.[Stores] ADD Province nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Stores', 'City') IS NULL
	ALTER TABLE dbo.[Stores] ADD City nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Stores', 'District') IS NULL
	ALTER TABLE dbo.[Stores] ADD District nvarchar(50) NULL
GO
IF COL_LENGTH ('dbo.Stores', 'Address') IS NULL
	ALTER TABLE dbo.[Stores] ADD Address nvarchar(200) NULL
GO


CREATE TABLE [dbo].[Projects](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.Projects', 'StoreID') IS NULL
	ALTER TABLE dbo.[Projects] ADD StoreID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.Projects', 'ProjectName') IS NULL
	ALTER TABLE dbo.[Projects] ADD ProjectName nvarchar(300) NULL
GO
IF COL_LENGTH ('dbo.Projects', 'Picture') IS NULL
	ALTER TABLE dbo.[Projects] ADD Picture nvarchar(200) NULL
GO
IF COL_LENGTH ('dbo.Projects', 'Status') IS NULL
	ALTER TABLE dbo.[Projects] ADD Status nvarchar(2) NULL
GO
IF COL_LENGTH ('dbo.Projects', 'Price') IS NULL
	ALTER TABLE dbo.[Projects] ADD Price int NULL
GO
IF COL_LENGTH ('dbo.Projects', 'Count') IS NULL
	ALTER TABLE dbo.[Projects] ADD Count int NULL
GO
IF COL_LENGTH ('dbo.Projects', 'CreateTime') IS NULL
	ALTER TABLE dbo.[Projects] ADD CreateTime datetime NULL
GO


CREATE TABLE [dbo].[MemberOrders](
	[ID] uniqueidentifier NOT NULL PRIMARY KEY
)
IF COL_LENGTH ('dbo.MemberOrders', 'StoreID') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD StoreID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.MemberOrders', 'MemberID') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD MemberID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.MemberOrders', 'EmployeeID') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD EmployeeID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.MemberOrders', 'ProjectID') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD ProjectID uniqueidentifier NULL
GO
IF COL_LENGTH ('dbo.MemberOrders', 'OrderTime') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD OrderTime datetime NULL
GO
IF COL_LENGTH ('dbo.MemberOrders', 'Status') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD Status nvarchar(3) NULL
GO
IF COL_LENGTH ('dbo.MemberOrders', 'CreateTime') IS NULL
	ALTER TABLE dbo.[MemberOrders] ADD CreateTime datetime NULL
GO




IF COL_LENGTH ('dbo.Members', 'OpenID') IS NULL
	ALTER TABLE dbo.[Members] ADD OpenID  nvarchar(10) NULL
GO