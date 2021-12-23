create table AdminCurrentStatuses
(
	ID bigint identity
		constraint AdminCurrentStatuses_pk
			primary key nonclustered,
	AdminID bigint,
	StatusTypeID bigint,
	IsCurrent bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AdminTypes
(
	ID bigint identity
		constraint PK__AdminTyp__3214EC27274ECD65
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AdminUserMessageHubs
(
	ID bigint identity
		constraint PK_AdminUserMessageHubs
			primary key,
	AdminUserID bigint,
	ConnectionID nvarchar(max),
	CreatedBy bigint,
	modifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AgentBranches
(
	ID bigint identity
		constraint AgentBranches_pk
			primary key nonclustered,
	AgentID bigint,
	AgentBranchID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AgentCurrentStatuses
(
	ID bigint identity
		constraint AgentCurrentStatuses_pk
			primary key nonclustered,
	AgentID bigint,
	StatusID bigint,
	IsCurrent bit default 1,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AgentDeliveryPrices
(
	ID bigint identity
		constraint AgentDeliveryPrices_pk
			primary key nonclustered,
	AgentID bigint,
	Kilometers int,
	Price decimal,
	ExtraKilometers int,
	ExtraKiloPrice decimal,
	IsCurrent bit default 1,
	IsDeleted bit default 0,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AgentMessageHubs
(
	ID bigint identity
		constraint PK_AgentMessageHubs
			primary key,
	AgentID bigint,
	ConnectionID nvarchar(max),
	CreatedBy bigint,
	modifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AgentOrderDeliveryPrices
(
	ID bigint identity
		constraint AgentOrderDeliveryPrices_pk
			primary key nonclustered,
	OrderID bigint,
	AgentDeliveryPriceID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table AgentTypes
(
	ID bigint identity
		constraint PK__AgentTyp__3214EC2764029FC7
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table AndroidVersions
(
	ID bigint identity
		constraint AndroidVersions_pk
			primary key nonclustered,
	Name nvarchar(400),
	Description nvarchar(max),
	Version nvarchar(400),
	FileName nvarchar(max),
	FilePath nvarchar(max),
	FileExtension nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	IsCurrent bit default 1
)
go

create table BonusTypes
(
	Id bigint not null
		constraint BonusTypes_PK
			primary key,
	Type varchar(max)
)
go

create table Bookkeeping
(
	ID bigint identity
		constraint Bookkeeping_pk
			primary key nonclustered,
	UserID bigint,
	OrderID bigint,
	DepositTypeID bigint,
	Value decimal,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table BoxTypes
(
	ID bigint identity
		constraint PK__BoxTypes__3214EC278451DD65
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table Cities
(
	ID bigint identity
		constraint Cities_pk
			primary key nonclustered,
	CountryID bigint,
	Name nvarchar(400),
	ArabicName nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table CityOrderPrices
(
	ID bigint identity
		constraint CityOrderPrices_pk
			primary key nonclustered,
	OrderID bigint,
	CityPriceID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table CityPrices
(
	ID bigint identity
		constraint CityPrices_pk
			primary key nonclustered,
	CityID bigint,
	Kilometers int,
	Price decimal,
	ExtraKilometers int,
	ExtraKiloPrice decimal,
	IsCurrent bit default 1,
	IsDeleted bit default 0,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table ContactMessages
(
	ID bigint identity
		constraint ContactMessages_pk
			primary key nonclustered,
	Name nvarchar(400),
	Email nvarchar(400),
	Subject nvarchar(max),
	Message nvarchar(max),
	IsDeleted bit default 0,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	Status bigint
)
go

create table Countries
(
	ID bigint identity
		constraint PK__Countrie__3214EC270DB6767C
			primary key,
	Name nvarchar(400),
	ISO nvarchar(400),
	Code bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicName nvarchar(400),
	CurrencyName nvarchar(200),
	CurrencyArabicName nvarchar(200),
	CurrencyISO nvarchar(200)
)
go

create table AdminUsers
(
	ID bigint identity
		constraint PK__AdminUse__3214EC27AD601441
			primary key,
	Fullname nvarchar(400),
	NationalNumber nvarchar(400),
	CountryID bigint
		references Countries,
	CityID bigint
		references Cities,
	Address nvarchar(400),
	Gender nvarchar(200),
	BirthDay int,
	BirthMonth int,
	BirthYear int,
	Mobile nvarchar(200),
	Email nvarchar(200),
	ResidenceExpireDay int,
	ResidenceExpireMonth int,
	ResidenceExpireYear int,
	Image nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ResidenceCountryID bigint,
	ResidenceCityID bigint
)
go

create table AdminUserAccounts
(
	ID bigint identity
		constraint PK__AdminUse__3214EC27EE9444E6
			primary key,
	AdminUserID bigint
		constraint FK_AdminUserAccounts_AdminUsers
			references AdminUsers,
	AdminTypeID bigint
		constraint FK_AdminUserAccounts_AdminTypes
			references AdminTypes,
	StatusTypeID bigint,
	Email nvarchar(200),
	PasswordHash varbinary(max),
	PasswordSalt varbinary(max),
	Token nvarchar(max),
	IsDeleted bit constraint DF__AdminUser__IsDel__4F7CD00D default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Agents
(
	ID bigint identity
		constraint PK__Agents__3214EC27A5F535F2
			primary key,
	Fullname nvarchar(max),
	Email nvarchar(400),
	CountryID bigint
		references Countries,
	CityID bigint
		references Cities,
	Address nvarchar(max),
	Mobile nvarchar(200),
	AgentTypeID bigint
		constraint FK_Agents_AgentTypes
			references AgentTypes,
	IsBranch bit,
	PasswordHash varbinary(max),
	PasswordSalt varbinary(max),
	Token nvarchar(max),
	LocationLat nvarchar(max),
	LocationLong nvarchar(max),
	IsDeleted bit constraint DF__Agents__IsDelete__440B1D61 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	StatusTypeID bigint,
	Image nvarchar(max),
	CommercialRegistrationNumber nvarchar(200),
	Website nvarchar(200)
)
go

create table AgentLocationHistories
(
	ID bigint identity
		constraint PK__AgentLoc__3214EC27C4C63A75
			primary key,
	AgentID bigint
		constraint FK_AgentLocationHistories_Agents
			references Agents,
	Lat nvarchar(max),
	Long nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Bonuses
(
	BonusPerMonth decimal,
	OrdersPerMonth bigint,
	BonusPerYear decimal,
	OrdersPerYear bigint,
	CountryId bigint
		constraint Bonuses_Country_FK
			references Countries,
	CreatedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	Id bigint identity
		constraint Bounus_PK
			primary key,
	BonusPerDay decimal,
	OrdersPerDay bigint
)
go

create table CountryOrderPrices
(
	ID bigint identity
		constraint CountryOrderPrices_pk
			primary key nonclustered,
	OrderID bigint,
	CountryPriceID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table CountryPrices
(
	ID bigint identity
		constraint PK__CountryP__3214EC2780DE13DD
			primary key,
	CountryID bigint
		constraint FK_CountryPrices_Countries
			references Countries,
	Kilometers int,
	Price decimal,
	ExtraKilometers int,
	ExtraKiloPrice decimal,
	IsDeleted bit constraint DF__CountryPr__IsDel__5AEE82B9 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	IsCurrent bit default 1
)
go

create table CountryPriceHistories
(
	ID bigint identity
		constraint PK__CountryP__3214EC2732AC7ED2
			primary key,
	CountryPriceID bigint
		constraint FK_CountryPriceHistories_CountryPrices
			references CountryPrices,
	Kilometers int,
	Price decimal,
	ExtraKilometers int,
	ExtraKiloPrice decimal,
	IsDeleted bit constraint DF_CountryPriceHistories_IsDeleted default 0,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table CouponType
(
	Id bigint not null
		constraint CouponType_PK
			primary key,
	Type nvarchar(max)
)
go

create table Coupons
(
	Id bigint identity
		constraint Coupons_pk
			primary key,
	Coupon nvarchar(max),
	DiscountPercent float,
	NumberOfUse int,
	ExpirationDate datetime,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	CouponType bigint
		constraint Coupon_CouponType_FK
			references CouponType
)
go

create table CouponAssign
(
	Id bigint identity
		constraint AgentCoupons_pk
			primary key,
	CouponId bigint
		constraint Fk_AgentCoupons_Coupons
			references Coupons,
	AgentId bigint
		constraint Fk_AgentCoupons_Agents
			references Agents,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	CountryId bigint
		constraint Coupouns_Countries_Fk
			references Countries
)
go

create table DepositTypes
(
	ID bigint identity
		constraint DepositTypes_pk
			primary key nonclustered,
	Type nvarchar(400),
	ArabicType nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table IgnorPerTypes
(
	ID bigint identity
		constraint PK__IgnorPer__3214EC272480895B
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table MaounAgents
(
	ID bigint identity
		constraint MaounAgents_pk
			primary key nonclustered,
	MaounBusinessID bigint,
	SenderAgentID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table MaounOrders
(
	ID bigint identity
		constraint MaounOrders_pk
			primary key nonclustered,
	MaounOrderID bigint,
	SenderOrderID bigint,
	SenderAgentID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table MessageStatusTypes
(
	ID bigint identity
		constraint MessageStatusTypes_pk
			primary key nonclustered,
	Type nvarchar(400),
	ArabicType nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table OrderInvoices
(
	ID bigint identity
		constraint OrderInvoices_pk
			primary key nonclustered,
	UserID bigint,
	OrderID bigint,
	OrderAssignID bigint,
	FileName nvarchar(400),
	FileType nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	PaidOrderID bigint
)
go

create table OrderStatusTypes
(
	ID bigint identity
		constraint PK__OrderSta__3214EC27207C5076
			primary key,
	Status nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicStatus nvarchar(400)
)
go

create table PaidOrders
(
	ID bigint identity
		constraint PaidOrders_pk
			primary key nonclustered,
	UserID bigint,
	OrderID bigint,
	OrderAssignID bigint,
	Type bigint,
	Value decimal,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table PaymentStatusTypes
(
	ID bigint identity
		constraint PK__PaymentS__3214EC278290D71B
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table PaymentTypes
(
	ID bigint identity
		constraint PK__PaymentT__3214EC27AA061B7F
			primary key,
	Type nvarchar(400),
	Allowed bit constraint DF__PaymentTy__Allow__6EF57B66 default 1 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table PenaltyPerTypes
(
	ID bigint identity
		constraint PK__PenaltyP__3214EC27C4F8345F
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table PenaltyStatusTypes
(
	ID bigint identity
		constraint PK__PenaltyS__3214EC27CDE66982
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table ProductTypes
(
	ID bigint identity
		constraint PK__ProductT__3214EC274DB01D96
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table CountryProductPrices
(
	ID bigint identity
		constraint PK__CountryP__3214EC27BD07334B
			primary key,
	CountryID bigint
		constraint FK_CountryProductPrices_Countries
			references Countries,
	ProductID bigint
		constraint FK_CountryProductPrices_ProductTypes
			references ProductTypes,
	Kilometers int,
	Price decimal,
	ExtraKilometers int,
	ExtraKiloPrice decimal,
	IsDeleted bit constraint DF__CountryPr__IsDel__52593CB8 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table CountryProductPriceHistories
(
	ID bigint identity
		constraint PK__CountryP__3214EC27144468F4
			primary key,
	CountryProductPriceID bigint
		constraint FK_CountryProductPriceHistories_CountryProductPrices
			references CountryProductPrices,
	Kilometers int,
	Price decimal,
	ExtraKilometers int,
	ExtraKiloPrice decimal,
	IsDeleted bit constraint DF_CountryProductPriceHistories_IsDeleted default 0,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Orders
(
	ID bigint identity
		constraint PK__Orders__3214EC27831F2285
			primary key,
	AgentID bigint
		constraint FK_Orders_Agents
			references Agents,
	ProductTypeID bigint
		constraint FK_Orders_ProductTypes
			references ProductTypes,
	ProductOtherType nvarchar(400),
	CustomerName nvarchar(400),
	CustomerPhone nvarchar(400),
	PaymentTypeID bigint
		constraint FK_Orders_PaymentTypes
			references PaymentTypes,
	Description nvarchar(max),
	Details nvarchar(max),
	PickupLocationLat nvarchar(max),
	PickupLocationLong nvarchar(max),
	DropLocationLat nvarchar(max),
	DropLocationLong nvarchar(max),
	CurrentStatus bigint,
	IsDeleted bit constraint DF__Orders__IsDelete__160F4887 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	CustomerAddress nvarchar(max)
)
go

create table CouponUsage
(
	Id bigint identity
		constraint CouponUsage_pk
			primary key,
	CouponId bigint
		constraint Fk_CouponUsage_Coupons
			references Coupons,
	UsageDate datetime,
	CreatedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	OrderId bigint
		constraint CouponUsage_Order_FK
			references Orders,
	AgentId bigint
		constraint CouponUsage_Agent_FK
			references Agents
)
go

create table OrderCurrentStatuses
(
	ID bigint identity
		constraint PK__OrderCur__3214EC27BC4A2FAE
			primary key,
	OrderID bigint
		constraint FK_OrderCurrentStatuses_Orders
			references Orders,
	StatusTypeID bigint,
	IsCurrent bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table OrderItems
(
	ID bigint identity
		constraint PK__OrderIte__3214EC277FE45D1E
			primary key,
	OrderID bigint
		constraint FK_OrderItems_Orders
			references Orders,
	Item nvarchar(400),
	Description nvarchar(max),
	Quantity int,
	Price decimal,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table OrderStatusHistories
(
	ID bigint identity
		constraint PK__OrderSta__3214EC27E2061757
			primary key,
	OrderID bigint
		constraint FK_OrderStatusHistories_Orders
			references Orders,
	OrderStatusID bigint
		constraint FK_OrderStatusHistories_OrderStatusTypes
			references OrderStatusTypes,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table PromotionTypes
(
	ID bigint identity
		constraint PromotionTypes_pk
			primary key nonclustered,
	Type nvarchar(400),
	ArabicType nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Promotions
(
	ID bigint identity
		constraint Promotions_pk
			primary key nonclustered,
	Name nvarchar(400),
	Descriptions nvarchar(max),
	Image nvarchar(max),
	TypeID bigint,
	Value nvarchar(max),
	IsDeleted bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ExpireDate datetime
)
go

create table RejectPerTypes
(
	ID bigint identity
		constraint PK__RejectPe__3214EC27B9D708D7
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table RunningOrders
(
	ID bigint identity
		constraint RunningOrders_pk
			primary key nonclustered,
	OrderID bigint,
	UserID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Shifts
(
	ID bigint identity
		constraint Shifts_pk
			primary key nonclustered,
	StartHour nvarchar(400),
	StartMinutes nvarchar(400),
	EndHour nvarchar(400),
	EndMinutes nvarchar(400),
	Day int,
	Month int,
	Year int,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	IsDeleted bit default 0,
	CountryID bigint,
	CityID bigint
)
go

create table StatusTypes
(
	ID bigint identity
		constraint PK__UserStat__3214EC27BF31C000
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table SupportStatusTypes
(
	ID bigint identity
		constraint PK__SupportS__3214EC27C99AEF84
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table SupportTypes
(
	ID bigint identity
		constraint PK__SupportT__3214EC27E6AC3074
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table SupportUserCurrentStatuses
(
	ID bigint identity
		constraint PK_SupportUserCurrentStatuses
			primary key,
	SupportUserID bigint,
	StatusTypeID bigint,
	IsCurrent bit constraint DF_SupportUserCurrentStatuses_IsCurrent default 1,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table SupportUserMessageHubs
(
	ID bigint identity
		constraint PK_SupportUserMessageHubs
			primary key,
	SupportUserID bigint,
	ConnectionID nvarchar(max),
	CreatedBy bigint,
	modifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table SupportUserWorkingState
(
	ID bigint identity
		constraint SupportUserWorkingState_pk
			primary key nonclustered,
	SupportUserID bigint,
	SupportStatusTypeID bigint,
	IsCurrent bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table SupportUsers
(
	ID bigint identity
		constraint PK__SupportU__3214EC2765AF91C3
			primary key,
	Fullname nvarchar(400),
	NationalNumber nvarchar(400),
	CountryID bigint
		references Countries,
	CityID bigint
		references Cities,
	Address nvarchar(400),
	Gender nvarchar(200),
	BirthDay int,
	BirthMonth int,
	BirthYear int,
	Mobile nvarchar(200),
	Email nvarchar(200),
	ResidenceExpireDay int,
	ResidenceExpireMonth int,
	ResidenceExpireYear int,
	Image nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ResidenceCountryID bigint,
	ResidenceCityID bigint
)
go

create table SupportUserAccounts
(
	ID bigint identity
		constraint PK__SupportU__3214EC27D25C09B5
			primary key,
	SupportUserID bigint
		constraint FK_SupportUserAccounts_SupportUsers
			references SupportUsers,
	StatusTypeID bigint,
	Email nvarchar(200),
	PasswordHash varbinary(max),
	PasswordSalt varbinary(max),
	Token nvarchar(max),
	IsDeleted bit constraint DF__SupportUs__IsDel__797309D9 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	Fullname nvarchar(400),
	SupportTypeID bigint
)
go

create table Supports
(
	ID bigint identity
		constraint PK__Supports__3214EC275A19F762
			primary key,
	SupportTypeID bigint
		constraint FK_Supports_SupportTypes
			references SupportTypes,
	UserID bigint,
	StatusTypeID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	Description nvarchar(max)
)
go

create table SystemSettings
(
	ID bigint identity
		constraint PK__SystemSe__3214EC272969EA5A
			primary key,
	AllowUserToReject bit,
	AllowUserToWorkOutShift bit,
	AllowUserPayment bit,
	AllowFixedPricePerCountry bit,
	AllowPricePerProductCountry bit,
	RejectPerTypeID bigint
		constraint FK_SystemSettings_RejectPerTypes
			references RejectPerTypes,
	RejectRequestsNumbers int,
	RejectPenaltyPerTypeID bigint,
	RejectPenaltyPeriodNumber int,
	IgnorPerTypeID bigint
		constraint FK_SystemSettings_IgnorPerTypes
			references IgnorPerTypes,
	IgnorRequestsNumbers int,
	IgnorPenaltyPerTypeID bigint,
	IgnorPenaltyPeriodNumber int,
	IsCurrent bit,
	IsDeleted bit constraint DF__SystemSet__IsDel__4AB81AF0 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Transfers
(
	ID bigint identity
		constraint Transfers_pk
			primary key nonclustered,
	BookkeepingID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserAccountHistories
(
	ID bigint identity
		constraint PK__UserAcco__3214EC2708C67508
			primary key,
	AccountID bigint,
	StatusTypeID bigint
		constraint FK_UserAccountHistories_StatusTypes
			references StatusTypes,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserActivities
(
	ID bigint identity
		constraint UserActivities_pk
			primary key nonclustered,
	StatusTypeID bigint,
	IsCurrent bit default 0,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	UserID bigint
)
go

create table UserMessageHubs
(
	ID bigint identity
		constraint PK_UserMessageHubs
			primary key,
	UserID bigint,
	ConnectionID nvarchar(max),
	CreatedBy bigint,
	modifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserPaymentHistories
(
	ID bigint identity
		constraint UserPaymentHistories_pk
			primary key nonclustered,
	UserID bigint,
	OrderID bigint,
	PaymentTypeID bigint,
	SystemSettingID bigint,
	StatusID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	Value decimal
)
go

create table UserPromotions
(
	ID bigint identity
		constraint UserPromotions_pk
			primary key nonclustered,
	UserID bigint,
	PromotionID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Users
(
	ID bigint identity
		constraint PK__Users__3214EC279E2FF09F
			primary key,
	NationalNumber nvarchar(400),
	CountryID bigint
		references Countries,
	CityID bigint
		references Cities,
	BirthDay int,
	BirthMonth int,
	BirthYear int,
	Mobile nvarchar(200),
	ResidenceExpireDay int,
	ResidenceExpireMonth int,
	ResidenceExpireYear int,
	PersonalImageName nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	PersonalImageAndroidPath nvarchar(max),
	NationalNumberImageName nvarchar(max),
	NationalNumberImageAndroidPath nvarchar(max),
	ResidenceCountryID bigint,
	ResidenceCityID bigint,
	NationalNoExpireDay int,
	NationalNoExpireMonth int,
	NationalNoExpireYear int,
	RecidenceImg varchar(max),
	FirstName nvarchar(max),
	FamilyName nvarchar(max),
	BirthDate datetime,
	NationalNumberExpDate datetime,
	StcPay nvarchar(max),
	Gender bit,
	PersonalImage nvarchar(max),
	NbsherNationalNumberImage nvarchar(max),
	NationalNumberFrontImage nvarchar(max),
	VehicleRegistrationImage nvarchar(max),
	DrivingLicenseImage nvarchar(max),
	VehiclePlateNumber nvarchar(max)
)
go

create table OrderAssignments
(
	ID bigint identity
		constraint PK__OrderAss__3214EC274ACE4655
			primary key,
	OrderID bigint
		constraint FK_OrderAssignments_Orders
			references Orders,
	UserID bigint
		constraint FK_OrderAssignments_Users
			references Users,
	ToAgentKilometer nvarchar(400),
	ToAgentTime nvarchar(400),
	ToCustomerKilometer nvarchar(400),
	ToCustomerTime nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table OrderEndLocations
(
	ID bigint identity
		constraint PK__OrderEnd__3214EC270A96D6C2
			primary key,
	OrderID bigint
		constraint FK_OrderEndLocations_Orders
			references Orders,
	OrderAssignID bigint
		constraint FK_OrderEndLocations_OrderAssignments
			references OrderAssignments,
	DroppedLat nvarchar(max),
	DroppedLong nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table OrderStartLocations
(
	ID bigint identity
		constraint PK__OrderSta__3214EC273AA96935
			primary key,
	OrderID bigint
		constraint FK_OrderStartLocations_Orders
			references Orders,
	OrderAssignID bigint
		constraint FK_OrderStartLocations_OrderAssignments
			references OrderAssignments,
	PickedupLat nvarchar(max),
	PickedupLong nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table QRCodes
(
	ID bigint identity
		constraint QRCodes_pk
			primary key,
	Code varbinary(max),
	OrderId bigint
		constraint FK_QRCodes_Orders
			references Orders,
	UserId bigint
		constraint FK_QRCodes_users
			references Users,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	IsDeleted bit,
	QrCodeUrl nvarchar(max)
)
go

create table SupportAssignments
(
	ID bigint identity
		constraint PK__SupportA__3214EC277F14EF0A
			primary key,
	SupportID bigint
		constraint FK_SupportAssignments_Supports
			references Supports,
	SupportUserID bigint
		constraint FK_SupportAssignments_SupportUsers
			references SupportUsers,
	UserID bigint
		constraint FK_SupportAssignments_Users
			references Users,
	CurrentStatusID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table SupportMessages
(
	ID bigint identity
		constraint PK__SupportM__3214EC27F440ACF5
			primary key,
	SupportAssignID bigint
		constraint FK_SupportMessages_SupportAssignments
			references SupportAssignments,
	IsUser bit,
	Message nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserAcceptedRequests
(
	ID bigint identity
		constraint PK__UserAcce__3214EC2770B7639D
			primary key,
	UserID bigint
		constraint FK_UserAcceptedRequests_Users
			references Users,
	OrderID bigint
		constraint FK_UserAcceptedRequests_Orders
			references Orders,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserAccounts
(
	ID bigint identity
		constraint PK__UserAcco__3214EC27BE466461
			primary key,
	UserID bigint
		constraint FK_UserAccounts_Users
			references Users,
	StatusTypeID bigint
		constraint FK_UserAccounts_StatusTypes
			references StatusTypes,
	Mobile nvarchar(200),
	PasswordHash varbinary(max),
	PasswordSalt varbinary(max),
	Token nvarchar(max),
	IsDeleted bit constraint DF__UserAccou__IsDel__2C3393D0 default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	Fullname nvarchar(400),
	PersonalImage nvarchar(max),
	Password nvarchar(400)
)
go

create table UserActiveHistories
(
	ID bigint identity
		constraint PK__UserActi__3214EC27E1D9109B
			primary key,
	UserID bigint
		constraint FK_UserActiveHistories_Users
			references Users,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserBonuses
(
	Id bigint identity
		constraint UserBonus_PK
			primary key,
	UserId bigint
		constraint UserBonuses_User_FK
			references Users,
	IsWithdrawed bit default 0 not null,
	WithdrawDate datetime,
	Amount decimal,
	CreatedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	BonusTypeId bigint
		constraint UserBonus_BonusType_FK
			references BonusTypes
)
go

create table UserCurrentActivities
(
	ID bigint identity
		constraint PK__UserCurr__3214EC270C847E8F
			primary key,
	UserID bigint
		constraint FK_UserCurrentActivities_Users
			references Users,
	IsActive bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserCurrentLocation
(
	ID bigint identity
		constraint PK__UserCurr__3214EC27C240B0FB
			primary key,
	UserID bigint
		constraint FK_UserCurrentLocation_Users
			references Users,
	Lat nvarchar(max),
	Long nvarchar(max),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserCurrentStatuses
(
	ID bigint identity
		constraint PK__UserCurr__3214EC27613ADB89
			primary key,
	UserID bigint
		constraint FK_UserCurrentStatuses_Users
			references Users,
	StatusTypeID bigint
		constraint FK_UserCurrentStatuses_StatusTypes
			references StatusTypes,
	IsCurrent bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserIgnoredPenalties
(
	ID bigint identity
		constraint PK__UserIgno__3214EC2711E008D7
			primary key,
	UserID bigint
		constraint FK_UserIgnoredPenalties_Users
			references Users,
	SystemSettingID bigint
		constraint FK_UserIgnoredPenalties_SystemSettings
			references SystemSettings,
	PenaltyStatusTypeID bigint
		constraint FK_UserIgnoredPenalties_PenaltyStatusTypes
			references PenaltyStatusTypes,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserIgnoredRequests
(
	ID bigint identity
		constraint PK__UserIgno__3214EC27A9F54468
			primary key,
	UserID bigint
		constraint FK_UserIgnoredRequests_Users
			references Users,
	OrderID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	AgentID bigint
)
go

create table UserInactiveHistories
(
	ID bigint identity
		constraint PK__UserInac__3214EC278668E20D
			primary key,
	UserID bigint
		constraint FK_UserInactiveHistories_Users
			references Users,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserNewRequests
(
	ID bigint identity
		constraint PK__UserNewR__3214EC274C8D3ACC
			primary key,
	UserID bigint
		constraint FK_UserNewRequests_Users
			references Users,
	OrderID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	AgentID bigint
)
go

create table UserPayments
(
	ID bigint identity
		constraint PK__UserPaym__3214EC27B3D74C78
			primary key,
	UserID bigint
		constraint FK_UserPayments_Users
			references Users,
	OrderID bigint,
	PaymentTypeID bigint
		constraint FK_UserPayments_PaymentTypes
			references PaymentTypes,
	SystemSettingID bigint
		constraint FK_UserPayments_SystemSettings
			references SystemSettings,
	Value decimal,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	StatusID bigint
)
go

create table UserCurrentBalance
(
	ID bigint identity
		constraint PK__UserCurr__3214EC2729A1EED6
			primary key,
	UserPaymentID bigint
		constraint FK_UserCurrentBalance_UserPayments
			references UserPayments,
	PaymentStatusTypeID bigint
		constraint FK_UserCurrentBalance_PaymentStatusTypes
			references PaymentStatusTypes,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserRejectPenalties
(
	ID bigint identity
		constraint PK__UserReje__3214EC27D795E4A1
			primary key,
	UserID bigint
		constraint FK_UserRejectPenalties_Users
			references Users,
	SystemSettingID bigint
		constraint FK_UserRejectPenalties_SystemSettings
			references SystemSettings,
	PenaltyStatusTypeID bigint
		constraint FK_UserRejectPenalties_PenaltyStatusTypes
			references PenaltyStatusTypes,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table UserRejectedRequests
(
	ID bigint identity
		constraint PK__UserReje__3214EC27568DA2C9
			primary key,
	UserID bigint
		constraint FK_UserRejectedRequests_Users
			references Users,
	OrderID bigint,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	AgentID bigint
)
go

create table UserShifts
(
	ID bigint identity
		constraint PK__UserShif__3214EC27E297148A
			primary key,
	UserID bigint
		constraint FK_UserShifts_Users
			references Users,
	StartHour nvarchar(400),
	StartMinutes nvarchar(400),
	EndHour nvarchar(400),
	EndMinutes nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ShiftID bigint,
	IsDeleted bit default 0
)
go

create table UserStatusHistories
(
	ID bigint identity
		constraint PK__UserStat__3214EC27D01DCB91
			primary key,
	UserID bigint
		constraint FK_UserStatusHistories_Users
			references Users,
	StatusTypeID bigint
		constraint FK_UserStatusHistories_StatusTypes
			references StatusTypes,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table Vehicles
(
	ID bigint identity
		constraint PK__Vehicles__3214EC27F175FA9A
			primary key,
	Type nvarchar(400),
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	ArabicType nvarchar(400)
)
go

create table UserVehicles
(
	ID bigint identity
		constraint PK__UserVehi__3214EC27AAC4762F
			primary key,
	UserID bigint
		constraint FK_UserVehicles_Users
			references Users,
	VehicleID bigint
		constraint FK_UserVehicles_Vehicles
			references Vehicles,
	PlateNumber nvarchar(400),
	Model nvarchar(400),
	VehicleImageName nvarchar(max),
	IsActive bit,
	IsDeleted bit,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime,
	VehicleImageAndroidPath nvarchar(max),
	LicenseImageName nvarchar(max),
	LicenseImageAndroidPath nvarchar(max),
	LicenseNumber nvarchar(400)
)
go

create table UserBoxs
(
	ID bigint identity
		constraint PK__UserBoxs__3214EC2782249154
			primary key,
	UserVehicleID bigint
		constraint FK_UserBoxs_UserVehicles
			references UserVehicles,
	BoxTypeID bigint
		constraint FK_UserBoxs_BoxTypes
			references BoxTypes,
	IsDeleted bit constraint DF__UserBoxs__IsDele__03F0984C default 0 not null,
	CreatedBy bigint,
	ModifiedBy bigint,
	CreationDate datetime,
	ModificationDate datetime
)
go

create table WebHookTypes
(
	Id bigint identity
		constraint webhookType_pk
			primary key,
	HookType nvarchar(max),
	ModifiedBy bigint,
	CreatedBy bigint,
	CreationDate datetime,
	ExpireationDate datetime,
	ModificationDate datetime
)
go

create table WebHooks
(
	Id bigint identity
		constraint webhooks_pk
			primary key,
	Url nvarchar(max),
	AgentId bigint
		constraint FK_WebHooks_Agent
			references Agents,
	ModifiedBy bigint,
	CreatedBy bigint,
	CreationDate datetime,
	ExpireationDate datetime,
	ModificationDate datetime,
	TypeId bigint
		constraint Fk_WebHooksTypes_WebHooks
			references WebHookTypes
)
go

