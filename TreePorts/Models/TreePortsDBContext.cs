using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TreePorts.Models
{
    public partial class TreePortsDBContext : DbContext
    {
        public TreePortsDBContext()
        {
        }

        public TreePortsDBContext(DbContextOptions<TreePortsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminCurrentStatus> AdminCurrentStatuses { get; set; }
        public virtual DbSet<AdminType> AdminTypes { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<AdminUserAccount> AdminUserAccounts { get; set; }
        public virtual DbSet<AdminUserMessageHub> AdminUserMessageHubs { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentBranch> AgentBranches { get; set; }
        public virtual DbSet<AgentCurrentStatus> AgentCurrentStatuses { get; set; }
        public virtual DbSet<AgentDeliveryPrice> AgentDeliveryPrices { get; set; }
        public virtual DbSet<AgentLocationHistory> AgentLocationHistories { get; set; }
        public virtual DbSet<AgentMessageHub> AgentMessageHubs { get; set; }
        public virtual DbSet<AgentOrderDeliveryPrice> AgentOrderDeliveryPrices { get; set; }
        public virtual DbSet<AgentType> AgentTypes { get; set; }
        public virtual DbSet<AndroidVersion> AndroidVersions { get; set; }
        public virtual DbSet<Bonus> Bonuses { get; set; }
        public virtual DbSet<BonusType> BonusTypes { get; set; }
        public virtual DbSet<Bookkeeping> Bookkeepings { get; set; }
        public virtual DbSet<BoxType> BoxTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityOrderPrice> CityOrderPrices { get; set; }
        public virtual DbSet<CityPrice> CityPrices { get; set; }
        public virtual DbSet<ContactMessage> ContactMessages { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryOrderPrice> CountryOrderPrices { get; set; }
        public virtual DbSet<CountryPrice> CountryPrices { get; set; }
        public virtual DbSet<CountryPriceHistory> CountryPriceHistories { get; set; }
        public virtual DbSet<CountryProductPrice> CountryProductPrices { get; set; }
        public virtual DbSet<CountryProductPriceHistory> CountryProductPriceHistories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponAssign> CouponAssigns { get; set; }
        public virtual DbSet<CouponType> CouponTypes { get; set; }
        public virtual DbSet<CouponUsage> CouponUsages { get; set; }
        public virtual DbSet<DepositType> DepositTypes { get; set; }
        public virtual DbSet<IgnorPerType> IgnorPerTypes { get; set; }
        public virtual DbSet<MaounAgent> MaounAgents { get; set; }
        public virtual DbSet<MaounOrder> MaounOrders { get; set; }
        public virtual DbSet<MessageStatusType> MessageStatusTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderAssignment> OrderAssignments { get; set; }
        public virtual DbSet<OrderCurrentStatus> OrderCurrentStatuses { get; set; }
        public virtual DbSet<OrderEndLocation> OrderEndLocations { get; set; }
        public virtual DbSet<OrderInvoice> OrderInvoices { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderStartLocation> OrderStartLocations { get; set; }
        public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual DbSet<OrderStatusType> OrderStatusTypes { get; set; }
        public virtual DbSet<PaidOrder> PaidOrders { get; set; }
        public virtual DbSet<PaymentStatusType> PaymentStatusTypes { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<PenaltyPerType> PenaltyPerTypes { get; set; }
        public virtual DbSet<PenaltyStatusType> PenaltyStatusTypes { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionType> PromotionTypes { get; set; }
        public virtual DbSet<Qrcode> Qrcodes { get; set; }
        public virtual DbSet<RejectPerType> RejectPerTypes { get; set; }
        public virtual DbSet<RunningOrder> RunningOrders { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<StatusType> StatusTypes { get; set; }
        public virtual DbSet<Support> Supports { get; set; }
        public virtual DbSet<SupportAssignment> SupportAssignments { get; set; }
        public virtual DbSet<SupportMessage> SupportMessages { get; set; }
        public virtual DbSet<SupportStatusType> SupportStatusTypes { get; set; }
        public virtual DbSet<SupportType> SupportTypes { get; set; }
        public virtual DbSet<SupportUser> SupportUsers { get; set; }
        public virtual DbSet<SupportUserAccount> SupportUserAccounts { get; set; }
        public virtual DbSet<SupportUserCurrentStatus> SupportUserCurrentStatuses { get; set; }
        public virtual DbSet<SupportUserMessageHub> SupportUserMessageHubs { get; set; }
        public virtual DbSet<SupportUserWorkingState> SupportUserWorkingStates { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<CaptainUser> Users { get; set; }
        public virtual DbSet<CaptainUserAcceptedRequest> UserAcceptedRequests { get; set; }
        public virtual DbSet<CaptainUserAccount> UserAccounts { get; set; }
        public virtual DbSet<CaptainUserAccountHistory> UserAccountHistories { get; set; }
        public virtual DbSet<CaptainUserActiveHistory> UserActiveHistories { get; set; }
        public virtual DbSet<CaptainUserActivity> UserActivities { get; set; }
        public virtual DbSet<CaptainUserBonus> UserBonuses { get; set; }
        public virtual DbSet<CaptainUserBox> UserBoxs { get; set; }
        public virtual DbSet<CaptainUserCurrentActivity> UserCurrentActivities { get; set; }
        public virtual DbSet<CaptainUserCurrentBalance> UserCurrentBalances { get; set; }
        public virtual DbSet<CaptainUserCurrentLocation> UserCurrentLocations { get; set; }
        public virtual DbSet<CaptainUserCurrentStatus> UserCurrentStatuses { get; set; }
        public virtual DbSet<CaptainUserIgnoredPenalty> UserIgnoredPenalties { get; set; }
        public virtual DbSet<CaptainUserIgnoredRequest> UserIgnoredRequests { get; set; }
        public virtual DbSet<CaptainUserInactiveHistory> UserInactiveHistories { get; set; }
        public virtual DbSet<CaptainUserMessageHub> UserMessageHubs { get; set; }
        public virtual DbSet<CaptainUserNewRequest> UserNewRequests { get; set; }
        public virtual DbSet<CaptainUserPayment> UserPayments { get; set; }
        public virtual DbSet<CaptainUserPaymentHistory> UserPaymentHistories { get; set; }
        public virtual DbSet<CaptainUserPromotion> UserPromotions { get; set; }
        public virtual DbSet<CaptainUserRejectPenalty> UserRejectPenalties { get; set; }
        public virtual DbSet<CaptainUserRejectedRequest> UserRejectedRequests { get; set; }
        public virtual DbSet<CaptainUserShift> UserShifts { get; set; }
        public virtual DbSet<CaptainUserStatusHistory> UserStatusHistories { get; set; }
        public virtual DbSet<CaptainUserVehicle> UserVehicles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<WebHook> WebHooks { get; set; }
        public virtual DbSet<WebHookType> WebHookTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=SenderDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminCurrentStatus>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("AdminCurrentStatuses_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");
            });

            modelBuilder.Entity<AdminType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fullname).HasMaxLength(400);

                entity.Property(e => e.Gender).HasMaxLength(200);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.NationalNumber).HasMaxLength(400);

                entity.Property(e => e.ResidenceCityId).HasColumnName("ResidenceCityID");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.AdminUsers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__AdminUser__CityI__4A4E069C");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AdminUsers)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__AdminUser__Count__4959E263");
            });

            modelBuilder.Entity<AdminUserAccount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdminTypeId).HasColumnName("AdminTypeID");

                entity.Property(e => e.AdminUserId).HasColumnName("AdminUserID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.AdminType)
                    .WithMany(p => p.AdminUserAccounts)
                    .HasForeignKey(d => d.AdminTypeId)
                    .HasConstraintName("FK_AdminUserAccounts_AdminTypes");

                entity.HasOne(d => d.AdminUser)
                    .WithMany(p => p.AdminUserAccounts)
                    .HasForeignKey(d => d.AdminUserId)
                    .HasConstraintName("FK_AdminUserAccounts_AdminUsers");
            });

            modelBuilder.Entity<AdminUserMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdminUserId).HasColumnName("AdminUserID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentTypeId).HasColumnName("AgentTypeID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CommercialRegistrationNumber).HasMaxLength(200);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(400);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.Website).HasMaxLength(200);

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgentTypeId)
                    .HasConstraintName("FK_Agents_AgentTypes");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Agents__CityID__4C364F0E");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Agents__CountryI__4B422AD5");
            });

            modelBuilder.Entity<AgentBranch>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("AgentBranches_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentBranchId).HasColumnName("AgentBranchID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AgentCurrentStatus>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("AgentCurrentStatuses_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<AgentDeliveryPrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("AgentDeliveryPrices_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<AgentLocationHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentLocationHistories)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentLocationHistories_Agents");
            });

            modelBuilder.Entity<AgentMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");
            });

            modelBuilder.Entity<AgentOrderDeliveryPrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("AgentOrderDeliveryPrices_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentDeliveryPriceId).HasColumnName("AgentDeliveryPriceID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
            });

            modelBuilder.Entity<AgentType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<AndroidVersion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("AndroidVersions_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FileExtension).HasMaxLength(400);

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.Property(e => e.Version).HasMaxLength(400);
            });

            modelBuilder.Entity<Bonus>(entity =>
            {
                entity.Property(e => e.BonusPerDay).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BonusPerMonth).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BonusPerYear).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Bonus)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("Bonuses_Country_FK");
            });

            modelBuilder.Entity<BonusType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<Bookkeeping>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Bookkeeping_pk")
                    .IsClustered(false);

                entity.ToTable("Bookkeeping");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DepositTypeId).HasColumnName("DepositTypeID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<BoxType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Cities_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicName).HasMaxLength(400);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(400);
            });

            modelBuilder.Entity<CityOrderPrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CityOrderPrices_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityPriceId).HasColumnName("CityPriceID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
            });

            modelBuilder.Entity<CityPrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CityPrices_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ContactMessages_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(400);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(400);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicName).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyArabicName).HasMaxLength(200);

                entity.Property(e => e.CurrencyIso)
                    .HasColumnName("CurrencyISO")
                    .HasMaxLength(200);

                entity.Property(e => e.CurrencyName).HasMaxLength(200);

                entity.Property(e => e.Iso)
                    .HasColumnName("ISO")
                    .HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(400);
            });

            modelBuilder.Entity<CountryOrderPrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CountryOrderPrices_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryPriceId).HasColumnName("CountryPriceID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
            });

            modelBuilder.Entity<CountryPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryPrices)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CountryPrices_Countries");
            });

            modelBuilder.Entity<CountryPriceHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryPriceId).HasColumnName("CountryPriceID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CountryPrice)
                    .WithMany(p => p.CountryPriceHistories)
                    .HasForeignKey(d => d.CountryPriceId)
                    .HasConstraintName("FK_CountryPriceHistories_CountryPrices");
            });

            modelBuilder.Entity<CountryProductPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryProductPrices)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CountryProductPrices_Countries");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CountryProductPrices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_CountryProductPrices_ProductTypes");
            });

            modelBuilder.Entity<CountryProductPriceHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryProductPriceId).HasColumnName("CountryProductPriceID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CountryProductPrice)
                    .WithMany(p => p.CountryProductPriceHistories)
                    .HasForeignKey(d => d.CountryProductPriceId)
                    .HasConstraintName("FK_CountryProductPriceHistories_CountryProductPrices");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.Coupon1).HasColumnName("Coupon");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.CouponTypeNavigation)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.CouponType)
                    .HasConstraintName("Coupon_CouponType_FK");
            });

            modelBuilder.Entity<CouponAssign>(entity =>
            {
                entity.ToTable("CouponAssign");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CouponAssigns)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("Fk_AgentCoupons_Agents");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CouponAssigns)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("Coupouns_Countries_Fk");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponAssigns)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("Fk_AgentCoupons_Coupons");
            });

            modelBuilder.Entity<CouponType>(entity =>
            {
                entity.ToTable("CouponType");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<CouponUsage>(entity =>
            {
                entity.ToTable("CouponUsage");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.UsageDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CouponUsages)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("CouponUsage_Agent_FK");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponUsages)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("Fk_CouponUsage_Coupons");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CouponUsages)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("CouponUsage_Order_FK");
            });

            modelBuilder.Entity<DepositType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("DepositTypes_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<IgnorPerType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<MaounAgent>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("MaounAgents_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.MaounBusinessId).HasColumnName("MaounBusinessID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.SenderAgentId).HasColumnName("SenderAgentID");
            });

            modelBuilder.Entity<MaounOrder>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("MaounOrders_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.MaounOrderId).HasColumnName("MaounOrderID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.SenderAgentId).HasColumnName("SenderAgentID");

                entity.Property(e => e.SenderOrderId).HasColumnName("SenderOrderID");
            });

            modelBuilder.Entity<MessageStatusType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("MessageStatusTypes_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(400);

                entity.Property(e => e.CustomerPhone).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.ProductOtherType).HasMaxLength(400);

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Orders_Agents");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_Orders_PaymentTypes");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_Orders_ProductTypes");
            });

            modelBuilder.Entity<OrderAssignment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ToAgentKilometer).HasMaxLength(400);

                entity.Property(e => e.ToAgentTime).HasMaxLength(400);

                entity.Property(e => e.ToCustomerKilometer).HasMaxLength(400);

                entity.Property(e => e.ToCustomerTime).HasMaxLength(400);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderAssignments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderAssignments_Orders");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderAssignments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OrderAssignments_Users");
            });

            modelBuilder.Entity<OrderCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCurrentStatus)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderCurrentStatuses_Orders");
            });

            modelBuilder.Entity<OrderEndLocation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderAssignId).HasColumnName("OrderAssignID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.OrderAssign)
                    .WithMany(p => p.OrderEndLocations)
                    .HasForeignKey(d => d.OrderAssignId)
                    .HasConstraintName("FK_OrderEndLocations_OrderAssignments");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderEndLocations)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderEndLocations_Orders");
            });

            modelBuilder.Entity<OrderInvoice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("OrderInvoices_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(400);

                entity.Property(e => e.FileType).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderAssignId).HasColumnName("OrderAssignID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaidOrderId).HasColumnName("PaidOrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Item).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItems_Orders");
            });

            modelBuilder.Entity<OrderStartLocation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderAssignId).HasColumnName("OrderAssignID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.OrderAssign)
                    .WithMany(p => p.OrderStartLocations)
                    .HasForeignKey(d => d.OrderAssignId)
                    .HasConstraintName("FK_OrderStartLocations_OrderAssignments");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStartLocations)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderStartLocations_Orders");
            });

            modelBuilder.Entity<OrderStatusHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatusHistories)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderStatusHistories_Orders");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.OrderStatusHistories)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK_OrderStatusHistories_OrderStatusTypes");
            });

            modelBuilder.Entity<OrderStatusType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicStatus).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(400);
            });

            modelBuilder.Entity<PaidOrder>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PaidOrders_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderAssignId).HasColumnName("OrderAssignID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PaymentStatusType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Allowed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PenaltyPerType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PenaltyStatusType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Promotions_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<PromotionType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PromotionTypes_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Qrcode>(entity =>
            {
                entity.ToTable("QRCodes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Qrcodes)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_QRCodes_Orders");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Qrcodes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_QRCodes_users");
            });

            modelBuilder.Entity<RejectPerType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<RunningOrder>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("RunningOrders_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Shifts_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.EndHour).HasMaxLength(400);

                entity.Property(e => e.EndMinutes).HasMaxLength(400);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StartHour).HasMaxLength(400);

                entity.Property(e => e.StartMinutes).HasMaxLength(400);
            });

            modelBuilder.Entity<StatusType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Support>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.SupportTypeId).HasColumnName("SupportTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.SupportType)
                    .WithMany(p => p.Supports)
                    .HasForeignKey(d => d.SupportTypeId)
                    .HasConstraintName("FK_Supports_SupportTypes");
            });

            modelBuilder.Entity<SupportAssignment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentStatusId).HasColumnName("CurrentStatusID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.SupportId).HasColumnName("SupportID");

                entity.Property(e => e.SupportUserId).HasColumnName("SupportUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Support)
                    .WithMany(p => p.SupportAssignments)
                    .HasForeignKey(d => d.SupportId)
                    .HasConstraintName("FK_SupportAssignments_Supports");

                entity.HasOne(d => d.SupportUser)
                    .WithMany(p => p.SupportAssignments)
                    .HasForeignKey(d => d.SupportUserId)
                    .HasConstraintName("FK_SupportAssignments_SupportUsers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SupportAssignments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SupportAssignments_Users");
            });

            modelBuilder.Entity<SupportMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.SupportAssignId).HasColumnName("SupportAssignID");

                entity.HasOne(d => d.SupportAssign)
                    .WithMany(p => p.SupportMessages)
                    .HasForeignKey(d => d.SupportAssignId)
                    .HasConstraintName("FK_SupportMessages_SupportAssignments");
            });

            modelBuilder.Entity<SupportStatusType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<SupportType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<SupportUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fullname).HasMaxLength(400);

                entity.Property(e => e.Gender).HasMaxLength(200);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.NationalNumber).HasMaxLength(400);

                entity.Property(e => e.ResidenceCityId).HasColumnName("ResidenceCityID");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SupportUsers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__SupportUs__CityI__4865BE2A");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SupportUsers)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__SupportUs__Count__477199F1");
            });

            modelBuilder.Entity<SupportUserAccount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fullname).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.SupportTypeId).HasColumnName("SupportTypeID");

                entity.Property(e => e.SupportUserId).HasColumnName("SupportUserID");

                entity.HasOne(d => d.SupportUser)
                    .WithMany(p => p.SupportUserAccounts)
                    .HasForeignKey(d => d.SupportUserId)
                    .HasConstraintName("FK_SupportUserAccounts_SupportUsers");
            });

            modelBuilder.Entity<SupportUserCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.SupportUserId).HasColumnName("SupportUserID");
            });

            modelBuilder.Entity<SupportUserMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.SupportUserId).HasColumnName("SupportUserID");
            });

            modelBuilder.Entity<SupportUserWorkingState>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SupportUserWorkingState_pk")
                    .IsClustered(false);

                entity.ToTable("SupportUserWorkingState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.SupportStatusTypeId).HasColumnName("SupportStatusTypeID");

                entity.Property(e => e.SupportUserId).HasColumnName("SupportUserID");
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IgnorPenaltyPerTypeId).HasColumnName("IgnorPenaltyPerTypeID");

                entity.Property(e => e.IgnorPerTypeId).HasColumnName("IgnorPerTypeID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.RejectPenaltyPerTypeId).HasColumnName("RejectPenaltyPerTypeID");

                entity.Property(e => e.RejectPerTypeId).HasColumnName("RejectPerTypeID");

                entity.HasOne(d => d.IgnorPerType)
                    .WithMany(p => p.SystemSettings)
                    .HasForeignKey(d => d.IgnorPerTypeId)
                    .HasConstraintName("FK_SystemSettings_IgnorPerTypes");

                entity.HasOne(d => d.RejectPerType)
                    .WithMany(p => p.SystemSettings)
                    .HasForeignKey(d => d.RejectPerTypeId)
                    .HasConstraintName("FK_SystemSettings_RejectPerTypes");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Transfers_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BookkeepingId).HasColumnName("BookkeepingID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CaptainUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.NationalNumber).HasMaxLength(400);

                entity.Property(e => e.NationalNumberExpDate).HasColumnType("datetime");

                entity.Property(e => e.RecidenceImg).IsUnicode(false);

                entity.Property(e => e.ResidenceCityId).HasColumnName("ResidenceCityID");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Users__CityID__467D75B8");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Users__CountryID__4589517F");
            });

            modelBuilder.Entity<CaptainUserAcceptedRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.UserAcceptedRequests)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_UserAcceptedRequests_Orders");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAcceptedRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserAcceptedRequests_Users");
            });

            modelBuilder.Entity<CaptainUserAccount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Fullname).HasMaxLength(400);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_UserAccounts_StatusTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserAccounts_Users");
            });

            modelBuilder.Entity<CaptainUserAccountHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.UserAccountHistories)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_UserAccountHistories_StatusTypes");
            });

            modelBuilder.Entity<CaptainUserActiveHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserActiveHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserActiveHistories_Users");
            });

            modelBuilder.Entity<CaptainUserActivity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("UserActivities_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CaptainUserBonus>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.WithdrawDate).HasColumnType("datetime");

                entity.HasOne(d => d.BonusType)
                    .WithMany(p => p.UserBonus)
                    .HasForeignKey(d => d.BonusTypeId)
                    .HasConstraintName("UserBonus_BonusType_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBonus)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserBonuses_User_FK");
            });

            modelBuilder.Entity<CaptainUserBox>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BoxTypeId).HasColumnName("BoxTypeID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.UserVehicleId).HasColumnName("UserVehicleID");

                entity.HasOne(d => d.BoxType)
                    .WithMany(p => p.UserBoxes)
                    .HasForeignKey(d => d.BoxTypeId)
                    .HasConstraintName("FK_UserBoxs_BoxTypes");

                entity.HasOne(d => d.UserVehicle)
                    .WithMany(p => p.UserBoxes)
                    .HasForeignKey(d => d.UserVehicleId)
                    .HasConstraintName("FK_UserBoxs_UserVehicles");
            });

            modelBuilder.Entity<CaptainUserCurrentActivity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCurrentActivities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserCurrentActivities_Users");
            });

            modelBuilder.Entity<CaptainUserCurrentBalance>(entity =>
            {
                entity.ToTable("UserCurrentBalance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentStatusTypeId).HasColumnName("PaymentStatusTypeID");

                entity.Property(e => e.UserPaymentId).HasColumnName("UserPaymentID");

                entity.HasOne(d => d.PaymentStatusType)
                    .WithMany(p => p.UserCurrentBalances)
                    .HasForeignKey(d => d.PaymentStatusTypeId)
                    .HasConstraintName("FK_UserCurrentBalance_PaymentStatusTypes");

                entity.HasOne(d => d.UserPayment)
                    .WithMany(p => p.UserCurrentBalances)
                    .HasForeignKey(d => d.UserPaymentId)
                    .HasConstraintName("FK_UserCurrentBalance_UserPayments");
            });

            modelBuilder.Entity<CaptainUserCurrentLocation>(entity =>
            {
                entity.ToTable("UserCurrentLocation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCurrentLocations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserCurrentLocation_Users");
            });

            modelBuilder.Entity<CaptainUserCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.UserCurrentStatus)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_UserCurrentStatuses_StatusTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCurrentStatus)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserCurrentStatuses_Users");
            });

            modelBuilder.Entity<CaptainUserIgnoredPenalty>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PenaltyStatusTypeId).HasColumnName("PenaltyStatusTypeID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.PenaltyStatusType)
                    .WithMany(p => p.UserIgnoredPenalties)
                    .HasForeignKey(d => d.PenaltyStatusTypeId)
                    .HasConstraintName("FK_UserIgnoredPenalties_PenaltyStatusTypes");

                entity.HasOne(d => d.SystemSetting)
                    .WithMany(p => p.UserIgnoredPenalties)
                    .HasForeignKey(d => d.SystemSettingId)
                    .HasConstraintName("FK_UserIgnoredPenalties_SystemSettings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserIgnoredPenalties)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserIgnoredPenalties_Users");
            });

            modelBuilder.Entity<CaptainUserIgnoredRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserIgnoredRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserIgnoredRequests_Users");
            });

            modelBuilder.Entity<CaptainUserInactiveHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInactiveHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserInactiveHistories_Users");
            });

            modelBuilder.Entity<CaptainUserMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CaptainUserNewRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNewRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserNewRequests_Users");
            });

            modelBuilder.Entity<CaptainUserPayment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.UserPayments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_UserPayments_PaymentTypes");

                entity.HasOne(d => d.SystemSetting)
                    .WithMany(p => p.UserPayments)
                    .HasForeignKey(d => d.SystemSettingId)
                    .HasConstraintName("FK_UserPayments_SystemSettings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPayments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserPayments_Users");
            });

            modelBuilder.Entity<CaptainUserPaymentHistory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("UserPaymentHistories_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<CaptainUserPromotion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("UserPromotions_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CaptainUserRejectPenalty>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PenaltyStatusTypeId).HasColumnName("PenaltyStatusTypeID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.PenaltyStatusType)
                    .WithMany(p => p.UserRejectPenalties)
                    .HasForeignKey(d => d.PenaltyStatusTypeId)
                    .HasConstraintName("FK_UserRejectPenalties_PenaltyStatusTypes");

                entity.HasOne(d => d.SystemSetting)
                    .WithMany(p => p.UserRejectPenalties)
                    .HasForeignKey(d => d.SystemSettingId)
                    .HasConstraintName("FK_UserRejectPenalties_SystemSettings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRejectPenalties)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRejectPenalties_Users");
            });

            modelBuilder.Entity<CaptainUserRejectedRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRejectedRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRejectedRequests_Users");
            });

            modelBuilder.Entity<CaptainUserShift>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.EndHour).HasMaxLength(400);

                entity.Property(e => e.EndMinutes).HasMaxLength(400);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.StartHour).HasMaxLength(400);

                entity.Property(e => e.StartMinutes).HasMaxLength(400);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserShifts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserShifts_Users");
            });

            modelBuilder.Entity<CaptainUserStatusHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.UserStatusHistories)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_UserStatusHistories_StatusTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserStatusHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserStatusHistories_Users");
            });

            modelBuilder.Entity<CaptainUserVehicle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseNumber).HasMaxLength(400);

                entity.Property(e => e.Model).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PlateNumber).HasMaxLength(400);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserVehicles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserVehicles_Users");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.UserVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_UserVehicles_Vehicles");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<WebHook>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.WebHooks)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_WebHooks_Agent");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.WebHooks)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("Fk_WebHooksTypes_WebHooks");
            });

            modelBuilder.Entity<WebHookType>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
