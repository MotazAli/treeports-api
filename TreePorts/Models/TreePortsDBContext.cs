using System;
using System.Collections.Generic;
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

        public virtual DbSet<AdminCurrentStatus> AdminCurrentStatuses { get; set; } = null!;
        public virtual DbSet<AdminType> AdminTypes { get; set; } = null!;
        public virtual DbSet<AdminUser> AdminUsers { get; set; } = null!;
        public virtual DbSet<AdminUserAccount> AdminUserAccounts { get; set; } = null!;
        public virtual DbSet<AdminUserMessageHub> AdminUserMessageHubs { get; set; } = null!;
        public virtual DbSet<Agent> Agents { get; set; } = null!;
        public virtual DbSet<AgentBranch> AgentBranches { get; set; } = null!;
        public virtual DbSet<AgentCurrentStatus> AgentCurrentStatuses { get; set; } = null!;
        public virtual DbSet<AgentDeliveryPrice> AgentDeliveryPrices { get; set; } = null!;
        public virtual DbSet<AgentLocationHistory> AgentLocationHistories { get; set; } = null!;
        public virtual DbSet<AgentMessageHub> AgentMessageHubs { get; set; } = null!;
        public virtual DbSet<AgentOrderDeliveryPrice> AgentOrderDeliveryPrices { get; set; } = null!;
        public virtual DbSet<AgentType> AgentTypes { get; set; } = null!;
        public virtual DbSet<AndroidVersion> AndroidVersions { get; set; } = null!;
        public virtual DbSet<Bonus> Bonuses { get; set; } = null!;
        public virtual DbSet<BonusType> BonusTypes { get; set; } = null!;
        public virtual DbSet<Bookkeeping> Bookkeepings { get; set; } = null!;
        public virtual DbSet<BoxType> BoxTypes { get; set; } = null!;
        public virtual DbSet<CaptainUser> CaptainUsers { get; set; } = null!;
        public virtual DbSet<CaptainUserAcceptedRequest> CaptainUserAcceptedRequests { get; set; } = null!;
        public virtual DbSet<CaptainUserAccount> CaptainUserAccounts { get; set; } = null!;
        public virtual DbSet<CaptainUserAccountHistory> CaptainUserAccountHistories { get; set; } = null!;
        public virtual DbSet<CaptainUserActiveHistory> CaptainUserActiveHistories { get; set; } = null!;
        public virtual DbSet<CaptainUserActivity> CaptainUserActivities { get; set; } = null!;
        public virtual DbSet<CaptainUserBonus> CaptainUserBonuses { get; set; } = null!;
        public virtual DbSet<CaptainUserBox> CaptainUserBoxs { get; set; } = null!;
        public virtual DbSet<CaptainUserCurrentActivity> CaptainUserCurrentActivities { get; set; } = null!;
        public virtual DbSet<CaptainUserCurrentBalance> CaptainUserCurrentBalances { get; set; } = null!;
        public virtual DbSet<CaptainUserCurrentLocation> CaptainUserCurrentLocations { get; set; } = null!;
        public virtual DbSet<CaptainUserCurrentStatus> CaptainUserCurrentStatuses { get; set; } = null!;
        public virtual DbSet<CaptainUserIgnoredPenalty> CaptainUserIgnoredPenalties { get; set; } = null!;
        public virtual DbSet<CaptainUserIgnoredRequest> CaptainUserIgnoredRequests { get; set; } = null!;
        public virtual DbSet<CaptainUserInactiveHistory> CaptainUserInactiveHistories { get; set; } = null!;
        public virtual DbSet<CaptainUserMessageHub> CaptainUserMessageHubs { get; set; } = null!;
        public virtual DbSet<CaptainUserNewRequest> CaptainUserNewRequests { get; set; } = null!;
        public virtual DbSet<CaptainUserPayment> CaptainUserPayments { get; set; } = null!;
        public virtual DbSet<CaptainUserPaymentHistory> CaptainUserPaymentHistories { get; set; } = null!;
        public virtual DbSet<CaptainUserPromotion> CaptainUserPromotions { get; set; } = null!;
        public virtual DbSet<CaptainUserShift> CaptainUserShifts { get; set; } = null!;
        public virtual DbSet<CaptainUserStatusHistory> CaptainUserStatusHistories { get; set; } = null!;
        public virtual DbSet<CaptainUserVehicle> CaptainUserVehicles { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<CityOrderPrice> CityOrderPrices { get; set; } = null!;
        public virtual DbSet<CityPrice> CityPrices { get; set; } = null!;
        public virtual DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<CountryOrderPrice> CountryOrderPrices { get; set; } = null!;
        public virtual DbSet<CountryPrice> CountryPrices { get; set; } = null!;
        public virtual DbSet<CountryPriceHistory> CountryPriceHistories { get; set; } = null!;
        public virtual DbSet<CountryProductPrice> CountryProductPrices { get; set; } = null!;
        public virtual DbSet<CountryProductPriceHistory> CountryProductPriceHistories { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<CouponAssign> CouponAssigns { get; set; } = null!;
        public virtual DbSet<CouponType> CouponTypes { get; set; } = null!;
        public virtual DbSet<CouponUsage> CouponUsages { get; set; } = null!;
        public virtual DbSet<DepositType> DepositTypes { get; set; } = null!;
        public virtual DbSet<IgnorPerType> IgnorPerTypes { get; set; } = null!;
        public virtual DbSet<MessageStatusType> MessageStatusTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderAssignment> OrderAssignments { get; set; } = null!;
        public virtual DbSet<OrderCurrentStatus> OrderCurrentStatuses { get; set; } = null!;
        public virtual DbSet<OrderEndLocation> OrderEndLocations { get; set; } = null!;
        public virtual DbSet<OrderInvoice> OrderInvoices { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<OrderQrcode> OrderQrcodes { get; set; } = null!;
        public virtual DbSet<OrderStartLocation> OrderStartLocations { get; set; } = null!;
        public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; } = null!;
        public virtual DbSet<OrderStatusType> OrderStatusTypes { get; set; } = null!;
        public virtual DbSet<PaidOrder> PaidOrders { get; set; } = null!;
        public virtual DbSet<PaymentStatusType> PaymentStatusTypes { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<PenaltyPerType> PenaltyPerTypes { get; set; } = null!;
        public virtual DbSet<PenaltyStatusType> PenaltyStatusTypes { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<PromotionType> PromotionTypes { get; set; } = null!;
        public virtual DbSet<RunningOrder> RunningOrders { get; set; } = null!;
        public virtual DbSet<Shift> Shifts { get; set; } = null!;
        public virtual DbSet<StatusType> StatusTypes { get; set; } = null!;
        public virtual DbSet<SupportType> SupportTypes { get; set; } = null!;
        public virtual DbSet<SupportUser> SupportUsers { get; set; } = null!;
        public virtual DbSet<SupportUserAccount> SupportUserAccounts { get; set; } = null!;
        public virtual DbSet<SupportUserCurrentStatus> SupportUserCurrentStatuses { get; set; } = null!;
        public virtual DbSet<SupportUserMessageHub> SupportUserMessageHubs { get; set; } = null!;
        public virtual DbSet<SupportUserWorkingState> SupportUserWorkingStates { get; set; } = null!;
        public virtual DbSet<SystemSetting> SystemSettings { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TicketAssignment> TicketAssignments { get; set; } = null!;
        public virtual DbSet<TicketMessage> TicketMessages { get; set; } = null!;
        public virtual DbSet<TicketStatusType> TicketStatusTypes { get; set; } = null!;
        public virtual DbSet<TicketType> TicketTypes { get; set; } = null!;
        public virtual DbSet<Transfer> Transfers { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<Webhook> Webhooks { get; set; } = null!;
        public virtual DbSet<WebhookType> WebhookTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=TreePortsDB");
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

                entity.Property(e => e.AdminUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("AdminUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.AdminUserAccount)
                    .WithMany(p => p.AdminCurrentStatus)
                    .HasForeignKey(d => d.AdminUserAccountId)
                    .HasConstraintName("FK_AdminCurrentStatuses_AdminUserAccounts");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.AdminCurrentStatus)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_AdminCurrentStatuses_StatusTypes");
            });

            modelBuilder.Entity<AdminType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(400);

                entity.Property(e => e.Gender).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(400);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.NationalNumber).HasMaxLength(400);

                entity.Property(e => e.ResidenceCityId).HasColumnName("ResidenceCityID");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.Property(e => e.ResidenceExpireDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.AdminUserCities)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_AdminUsers_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AdminUserCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_AdminUsers_Countries");

                entity.HasOne(d => d.ResidenceCity)
                    .WithMany(p => p.AdminUserResidenceCities)
                    .HasForeignKey(d => d.ResidenceCityId)
                    .HasConstraintName("FK_AdminUsers_Residence_Cities");

                entity.HasOne(d => d.ResidenceCountry)
                    .WithMany(p => p.AdminUserResidenceCountries)
                    .HasForeignKey(d => d.ResidenceCountryId)
                    .HasConstraintName("FK_AdminUsers_Residence_Countries");
            });

            modelBuilder.Entity<AdminUserAccount>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.AdminTypeId).HasColumnName("AdminTypeID");

                entity.Property(e => e.AdminUserId)
                    .HasMaxLength(400)
                    .HasColumnName("AdminUserID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Password).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.AdminType)
                    .WithMany(p => p.AdminUserAccounts)
                    .HasForeignKey(d => d.AdminTypeId)
                    .HasConstraintName("FK_AdminUserAccounts_AdminTypes");

                entity.HasOne(d => d.AdminUser)
                    .WithMany(p => p.AdminUserAccounts)
                    .HasForeignKey(d => d.AdminUserId)
                    .HasConstraintName("FK_AdminUserAccounts_AdminUsers");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.AdminUserAccounts)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_AdminUserAccounts_StatusTypes");
            });

            modelBuilder.Entity<AdminUserMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdminUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("AdminUserAccountID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.AdminUserAccount)
                    .WithMany(p => p.AdminUserMessageHubs)
                    .HasForeignKey(d => d.AdminUserAccountId)
                    .HasConstraintName("FK_AdminUserMessageHubs_AdminUserAccounts");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.AgentTypeId).HasColumnName("AgentTypeID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CommercialRegistrationNumber).HasMaxLength(200);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(400);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Password).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.Website).HasMaxLength(200);

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgentTypeId)
                    .HasConstraintName("FK_Agents_AgentTypes");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Agents_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Agents_Countries");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_Agents_StatusTypes");
            });

            modelBuilder.Entity<AgentBranch>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentBranchId).HasColumnName("AgentBranchID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentBranches)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentBranches_Agents");
            });

            modelBuilder.Entity<AgentCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentCurrentStatus)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentCurrentStatuses_Agents");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.AgentCurrentStatus)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_AgentCurrentStatuses_StatusTyps");
            });

            modelBuilder.Entity<AgentDeliveryPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentDeliveryPrices)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentDeliveryPrices_Agents");
            });

            modelBuilder.Entity<AgentLocationHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentLocationHistories)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentLocationHistories_Agents");
            });

            modelBuilder.Entity<AgentMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentMessageHubs)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentMessageHubs_Agents");
            });

            modelBuilder.Entity<AgentOrderDeliveryPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentDeliveryPriceId).HasColumnName("AgentDeliveryPriceID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.AgentDeliveryPrice)
                    .WithMany(p => p.AgentOrderDeliveryPrices)
                    .HasForeignKey(d => d.AgentDeliveryPriceId)
                    .HasConstraintName("FK_AgentOrderDeliveryPrices_AgentDeliveryPrices");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.AgentOrderDeliveryPrices)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_AgentOrderDeliveryPrices_Orders");
            });

            modelBuilder.Entity<AgentType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<AndroidVersion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FileExtension).HasMaxLength(400);

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.Property(e => e.Version).HasMaxLength(400);
            });

            modelBuilder.Entity<Bonus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BonusPerDay).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BonusPerMonth).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BonusPerYear).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Bonus)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Bonuses_Countries");
            });

            modelBuilder.Entity<BonusType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<Bookkeeping>(entity =>
            {
                entity.ToTable("Bookkeeping");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DepositTypeId).HasColumnName("DepositTypeID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.Bookkeepings)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_Bookkeeping_CaptainUserAccounts");

                entity.HasOne(d => d.DepositType)
                    .WithMany(p => p.Bookkeepings)
                    .HasForeignKey(d => d.DepositTypeId)
                    .HasConstraintName("FK_Bookkeeping_DepositTypes");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Bookkeepings)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Bookkeeping_Orders");
            });

            modelBuilder.Entity<BoxType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<CaptainUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Gender).HasMaxLength(200);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.NationalNumber).HasMaxLength(400);

                entity.Property(e => e.NationalNumberExpireDate).HasColumnType("datetime");

                entity.Property(e => e.RecidenceImage).IsUnicode(false);

                entity.Property(e => e.ResidenceCityId).HasColumnName("ResidenceCityID");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CaptainUserCities)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_CaptainUsers_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CaptainUserCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CaptainUsers_Countries");

                entity.HasOne(d => d.ResidenceCity)
                    .WithMany(p => p.CaptainUserResidenceCities)
                    .HasForeignKey(d => d.ResidenceCityId)
                    .HasConstraintName("FK_CaptainUsers_Residence_Cities");

                entity.HasOne(d => d.ResidenceCountry)
                    .WithMany(p => p.CaptainUserResidenceCountries)
                    .HasForeignKey(d => d.ResidenceCountryId)
                    .HasConstraintName("FK_CaptainUsers_Residence_Countries");
            });

            modelBuilder.Entity<CaptainUserAcceptedRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserAcceptedRequests)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserAcceptedRequests_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CaptainUserAcceptedRequests)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CaptainUserAcceptedRequests_Orders");
            });

            modelBuilder.Entity<CaptainUserAccount>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.CaptainUserId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Password).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.CaptainUser)
                    .WithMany(p => p.CaptainUserAccounts)
                    .HasForeignKey(d => d.CaptainUserId)
                    .HasConstraintName("FK_CaptainUserAccounts_CaptainUsers");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.CaptainUserAccounts)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_CaptainUserAccounts_StatusTypes");
            });

            modelBuilder.Entity<CaptainUserAccountHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserAccountHistories)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserAccountHistories_CaptainUserAccounts");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.CaptainUserAccountHistories)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_CaptainUserAccountHistories_StatusTypes");
            });

            modelBuilder.Entity<CaptainUserActiveHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserActiveHistories)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserActiveHistories_CaptainUserAccounts");
            });

            modelBuilder.Entity<CaptainUserActivity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CaptainUserActivities_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserActivities)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserActivities_CaptainUserAccounts");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.CaptainUserActivities)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_CaptainUserActivities_StatusTypes");
            });

            modelBuilder.Entity<CaptainUserBonus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BonusTypeId).HasColumnName("BonusTypeID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.WithdrawDate).HasColumnType("datetime");

                entity.HasOne(d => d.BonusType)
                    .WithMany(p => p.CaptainUserBonus)
                    .HasForeignKey(d => d.BonusTypeId)
                    .HasConstraintName("FK_CaptainUserBonuses_BonusTypes");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserBonus)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserBonuses_CaptainUserAccounts");
            });

            modelBuilder.Entity<CaptainUserBox>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BoxTypeId).HasColumnName("BoxTypeID");

                entity.Property(e => e.CaptainUserVehicleId).HasColumnName("CaptainUserVehicleID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.BoxType)
                    .WithMany(p => p.CaptainUserBoxes)
                    .HasForeignKey(d => d.BoxTypeId)
                    .HasConstraintName("FK_CaptainUserBoxs_BoxTypes");

                entity.HasOne(d => d.CaptainUserVehicle)
                    .WithMany(p => p.CaptainUserBoxes)
                    .HasForeignKey(d => d.CaptainUserVehicleId)
                    .HasConstraintName("FK_CaptainUserBoxs_CaptainUserVehicles");
            });

            modelBuilder.Entity<CaptainUserCurrentActivity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserCurrentActivities)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserCurrentActivities_CaptainUserAccounts");
            });

            modelBuilder.Entity<CaptainUserCurrentBalance>(entity =>
            {
                entity.ToTable("CaptainUserCurrentBalance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserPaymentId).HasColumnName("CaptainUserPaymentID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.PaymentStatusTypeId).HasColumnName("PaymentStatusTypeID");

                entity.HasOne(d => d.CaptainUserPayment)
                    .WithMany(p => p.CaptainUserCurrentBalances)
                    .HasForeignKey(d => d.CaptainUserPaymentId)
                    .HasConstraintName("FK_CaptainUserCurrentBalance_CaptainUserPayments");

                entity.HasOne(d => d.PaymentStatusType)
                    .WithMany(p => p.CaptainUserCurrentBalances)
                    .HasForeignKey(d => d.PaymentStatusTypeId)
                    .HasConstraintName("FK_CaptainUserCurrentBalance_PaymentStatusTypes");
            });

            modelBuilder.Entity<CaptainUserCurrentLocation>(entity =>
            {
                entity.ToTable("CaptainUserCurrentLocation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserCurrentLocations)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserCurrentLocation_CaptainUserAccounts");
            });

            modelBuilder.Entity<CaptainUserCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserCurrentStatus)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserCurrentStatuses_CaptainUserAccounts");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.CaptainUserCurrentStatus)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_CaptainUserCurrentStatuses_StatusTypes");
            });

            modelBuilder.Entity<CaptainUserIgnoredPenalty>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.PenaltyStatusTypeId).HasColumnName("PenaltyStatusTypeID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserIgnoredPenalties)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserIgnoredPenalties_CaptainUserAccounts");

                entity.HasOne(d => d.PenaltyStatusType)
                    .WithMany(p => p.CaptainUserIgnoredPenalties)
                    .HasForeignKey(d => d.PenaltyStatusTypeId)
                    .HasConstraintName("FK_CaptainUserIgnoredPenalties_PenaltyStatusTypes");

                entity.HasOne(d => d.SystemSetting)
                    .WithMany(p => p.CaptainUserIgnoredPenalties)
                    .HasForeignKey(d => d.SystemSettingId)
                    .HasConstraintName("FK_CaptainUserIgnoredPenalties_SystemSettings");
            });

            modelBuilder.Entity<CaptainUserIgnoredRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CaptainUserIgnoredRequests)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_CaptainUserIgnoredRequests_Agents");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserIgnoredRequests)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserIgnoredRequests_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CaptainUserIgnoredRequests)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CaptainUserIgnoredRequests_Orders");
            });

            modelBuilder.Entity<CaptainUserInactiveHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserInactiveHistories)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserInactiveHistories_CaptainUserAccounts");
            });

            modelBuilder.Entity<CaptainUserMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserMessageHubs)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserMessageHubs_CaptainUserAccounts");
            });

            modelBuilder.Entity<CaptainUserNewRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CaptainUserNewRequests)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_CaptainUserNewRequests_Agents");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserNewRequests)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserNewRequests_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CaptainUserNewRequests)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CaptainUserNewRequests_Orders");
            });

            modelBuilder.Entity<CaptainUserPayment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaymentStatusTypeId).HasColumnName("PaymentStatusTypeID");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserPayments)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserPayments_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CaptainUserPayments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CaptainUserPayments_Orders");

                entity.HasOne(d => d.PaymentStatusType)
                    .WithMany(p => p.CaptainUserPayments)
                    .HasForeignKey(d => d.PaymentStatusTypeId)
                    .HasConstraintName("FK_CaptainUserPayments_PaymentStatusTypes");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.CaptainUserPayments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_CaptainUserPayments_PaymentTypes");

                entity.HasOne(d => d.SystemSetting)
                    .WithMany(p => p.CaptainUserPayments)
                    .HasForeignKey(d => d.SystemSettingId)
                    .HasConstraintName("FK_CaptainUserPayments_SystemSettings");
            });

            modelBuilder.Entity<CaptainUserPaymentHistory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CaptainUserPaymentHistories_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaymentStatusTypeId).HasColumnName("PaymentStatusTypeID");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.SystemSettingId).HasColumnName("SystemSettingID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserPaymentHistories)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserPaymentHistories_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CaptainUserPaymentHistories)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CaptainUserPaymentHistories_Orders");

                entity.HasOne(d => d.PaymentStatusType)
                    .WithMany(p => p.CaptainUserPaymentHistories)
                    .HasForeignKey(d => d.PaymentStatusTypeId)
                    .HasConstraintName("FK_CaptainUserPaymentHistories_PaymentStatusTypes");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.CaptainUserPaymentHistories)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_CaptainUserPaymentHistories_PaymentTypes");

                entity.HasOne(d => d.SystemSetting)
                    .WithMany(p => p.CaptainUserPaymentHistories)
                    .HasForeignKey(d => d.SystemSettingId)
                    .HasConstraintName("FK_CaptainUserPaymentHistories_SystemSettings");
            });

            modelBuilder.Entity<CaptainUserPromotion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("UserPromotions_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserPromotions)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserPromotions_CaptainUserAccounts");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.CaptainUserPromotions)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_CaptainUserPromotions_Promotions");
            });

            modelBuilder.Entity<CaptainUserShift>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.EndHour).HasMaxLength(400);

                entity.Property(e => e.EndMinutes).HasMaxLength(400);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.StartHour).HasMaxLength(400);

                entity.Property(e => e.StartMinutes).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserShifts)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserShifts_CaptainUserAccounts");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.CaptainUserShifts)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("FK_CaptainUserShifts_Shifts");
            });

            modelBuilder.Entity<CaptainUserStatusHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserStatusHistories)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserStatusHistories_CaptainUserAccounts");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.CaptainUserStatusHistories)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_CaptainUserStatusHistories_StatusTypes");
            });

            modelBuilder.Entity<CaptainUserVehicle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseNumber).HasMaxLength(400);

                entity.Property(e => e.Model).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.PlateNumber).HasMaxLength(400);

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.CaptainUserVehicles)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_CaptainUserVehicles_CaptainUserAccounts");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.CaptainUserVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_CaptainUserVehicles_Vehicles");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicName).HasMaxLength(400);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<CityOrderPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityPriceId).HasColumnName("CityPriceID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.CityPrice)
                    .WithMany(p => p.CityOrderPrices)
                    .HasForeignKey(d => d.CityPriceId)
                    .HasConstraintName("FK_CityOrderPrices_CityPrices");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CityOrderPrices)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CityOrderPrices_Orders");
            });

            modelBuilder.Entity<CityPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityPrices)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_CityPrices_Cities");
            });

            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(400);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(400);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicName).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyArabicName).HasMaxLength(200);

                entity.Property(e => e.CurrencyIso)
                    .HasMaxLength(200)
                    .HasColumnName("CurrencyISO");

                entity.Property(e => e.CurrencyName).HasMaxLength(200);

                entity.Property(e => e.Iso)
                    .HasMaxLength(400)
                    .HasColumnName("ISO");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(400);
            });

            modelBuilder.Entity<CountryOrderPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryPriceId).HasColumnName("CountryPriceID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.CountryPrice)
                    .WithMany(p => p.CountryOrderPrices)
                    .HasForeignKey(d => d.CountryPriceId)
                    .HasConstraintName("FK_CountryOrderPrices_CountryPrices");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CountryOrderPrices)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CountryOrderPrices_Orders");
            });

            modelBuilder.Entity<CountryPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

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

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

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

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryProductPrices)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CountryProductPrices_Countries");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.CountryProductPrices)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_CountryProductPrices_ProductTypes");
            });

            modelBuilder.Entity<CountryProductPriceHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryProductPriceId).HasColumnName("CountryProductPriceID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraKiloPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CountryProductPrice)
                    .WithMany(p => p.CountryProductPriceHistories)
                    .HasForeignKey(d => d.CountryProductPriceId)
                    .HasConstraintName("FK_CountryProductPriceHistories_CountryProductPrices");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponTypeId).HasColumnName("CouponTypeID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CouponType)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.CouponTypeId)
                    .HasConstraintName("FK_Coupons_CouponTypes");
            });

            modelBuilder.Entity<CouponAssign>(entity =>
            {
                entity.ToTable("CouponAssign");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CouponAssigns)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_CouponAssign_Agents");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CouponAssigns)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CouponAssign_Countries");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponAssigns)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_CouponAssign_Coupons");
            });

            modelBuilder.Entity<CouponType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<CouponUsage>(entity =>
            {
                entity.ToTable("CouponUsage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasMaxLength(400);

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.UsageDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CouponUsages)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_CouponUsages_Agents");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponUsages)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_CouponUsages_Coupons");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CouponUsages)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CouponUsages_Orders");
            });

            modelBuilder.Entity<DepositType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<IgnorPerType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<MessageStatusType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("MessageStatusTypes_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId)
                    .HasMaxLength(400)
                    .HasColumnName("AgentID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(400);

                entity.Property(e => e.CustomerPhone).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderStatusTypeId).HasColumnName("OrderStatusTypeID");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.ProductOtherTypeInfo).HasMaxLength(400);

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Orders_Agents");

                entity.HasOne(d => d.OrderStatusType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusTypeId)
                    .HasConstraintName("FK_Orders_OrderStatusTypes");

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

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ToAgentKilometer).HasMaxLength(400);

                entity.Property(e => e.ToAgentTime).HasMaxLength(400);

                entity.Property(e => e.ToCustomerKilometer).HasMaxLength(400);

                entity.Property(e => e.ToCustomerTime).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.OrderAssignments)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_OrderAssignments_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderAssignments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderAssignments_Orders");
            });

            modelBuilder.Entity<OrderCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderStatusTypeId).HasColumnName("OrderStatusTypeID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCurrentStatus)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderCurrentStatuses_Orders");

                entity.HasOne(d => d.OrderStatusType)
                    .WithMany(p => p.OrderCurrentStatus)
                    .HasForeignKey(d => d.OrderStatusTypeId)
                    .HasConstraintName("FK_OrderCurrentStatuses_OrderStatusTypes");
            });

            modelBuilder.Entity<OrderEndLocation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

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

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(400);

                entity.Property(e => e.FileType).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderAssignId).HasColumnName("OrderAssignID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaidOrderId).HasColumnName("PaidOrderID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.OrderInvoices)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_OrderInvoices_CaptainUserAccounts");

                entity.HasOne(d => d.OrderAssign)
                    .WithMany(p => p.OrderInvoices)
                    .HasForeignKey(d => d.OrderAssignId)
                    .HasConstraintName("FK_OrderInvoices_OrderAssignments");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderInvoices)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderInvoices_Orders");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItems_Orders");
            });

            modelBuilder.Entity<OrderQrcode>(entity =>
            {
                entity.ToTable("OrderQRCodes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.OrderQrcodes)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_OrderQRCodes_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderQrcodes)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderQRCodes_Orders");
            });

            modelBuilder.Entity<OrderStartLocation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

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

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderStatusTypeId).HasColumnName("OrderStatusTypeID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatusHistories)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderStatusHistories_Orders");

                entity.HasOne(d => d.OrderStatusType)
                    .WithMany(p => p.OrderStatusHistories)
                    .HasForeignKey(d => d.OrderStatusTypeId)
                    .HasConstraintName("FK_OrderStatusHistories_OrderStatusTypes");
            });

            modelBuilder.Entity<OrderStatusType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PaidOrder>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PaidOrders_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderAssignId).HasColumnName("OrderAssignID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.PaidOrders)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_PaidOrders_CaptainUserAccounts");

                entity.HasOne(d => d.OrderAssign)
                    .WithMany(p => p.PaidOrders)
                    .HasForeignKey(d => d.OrderAssignId)
                    .HasConstraintName("FK_PaidOrders_OrderAssignments");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PaidOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_PaidOrders_Orders");
            });

            modelBuilder.Entity<PaymentStatusType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Allowed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PenaltyPerType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<PenaltyStatusType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.Property(e => e.PromotionTypeId).HasColumnName("PromotionTypeID");

                entity.HasOne(d => d.PromotionType)
                    .WithMany(p => p.Promotions)
                    .HasForeignKey(d => d.PromotionTypeId)
                    .HasConstraintName("FK_Promotions_PromotionTypes");
            });

            modelBuilder.Entity<PromotionType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<RunningOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.RunningOrders)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_RunningOrders_CaptainUserAccounts");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.RunningOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_RunningOrders_Orders");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.EndHour).HasMaxLength(400);

                entity.Property(e => e.EndMinutes).HasMaxLength(400);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StartHour).HasMaxLength(400);

                entity.Property(e => e.StartMinutes).HasMaxLength(400);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Shifts_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Shifts_Countries");
            });

            modelBuilder.Entity<StatusType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<SupportType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<SupportUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(400);

                entity.Property(e => e.Gender).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(400);

                entity.Property(e => e.Mobile).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.NationalNumber).HasMaxLength(400);

                entity.Property(e => e.ResidenceCityId).HasColumnName("ResidenceCityID");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.Property(e => e.ResidenceExpireDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SupportUserCities)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_SupportUsers_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SupportUserCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_SupportUsers_Countries");

                entity.HasOne(d => d.ResidenceCity)
                    .WithMany(p => p.SupportUserResidenceCities)
                    .HasForeignKey(d => d.ResidenceCityId)
                    .HasConstraintName("FK_SupportUsers_Residence_Cities");

                entity.HasOne(d => d.ResidenceCountry)
                    .WithMany(p => p.SupportUserResidenceCountries)
                    .HasForeignKey(d => d.ResidenceCountryId)
                    .HasConstraintName("FK_SupportUsers_Residence_Countries");
            });

            modelBuilder.Entity<SupportUserAccount>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Password).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.SupportTypeId).HasColumnName("SupportTypeID");

                entity.Property(e => e.SupportUserId)
                    .HasMaxLength(400)
                    .HasColumnName("SupportUserID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.SupportUserAccounts)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_SupportUserAccounts_StatusTypes");

                entity.HasOne(d => d.SupportType)
                    .WithMany(p => p.SupportUserAccounts)
                    .HasForeignKey(d => d.SupportTypeId)
                    .HasConstraintName("FK_SupportUserAccounts_SupportTypes");

                entity.HasOne(d => d.SupportUser)
                    .WithMany(p => p.SupportUserAccounts)
                    .HasForeignKey(d => d.SupportUserId)
                    .HasConstraintName("FK_SupportUserAccounts_SupportUsers");
            });

            modelBuilder.Entity<SupportUserCurrentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.SupportUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("SupportUserAccountID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.SupportUserCurrentStatus)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_SupportUserCurrentStatuses_StatusTypes");

                entity.HasOne(d => d.SupportUserAccount)
                    .WithMany(p => p.SupportUserCurrentStatus)
                    .HasForeignKey(d => d.SupportUserAccountId)
                    .HasConstraintName("FK_SupportUserCurrentStatuses_SupportUserAccounts");
            });

            modelBuilder.Entity<SupportUserMessageHub>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.SupportUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("SupportUserAccountID");

                entity.HasOne(d => d.SupportUserAccount)
                    .WithMany(p => p.SupportUserMessageHubs)
                    .HasForeignKey(d => d.SupportUserAccountId)
                    .HasConstraintName("FK_SupportUserMessageHubs_SupportUserAccounts");
            });

            modelBuilder.Entity<SupportUserWorkingState>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SupportUserWorkingState_pk")
                    .IsClustered(false);

                entity.ToTable("SupportUserWorkingState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.SupportUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("SupportUserAccountID");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.SupportUserWorkingStates)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_SupportUserWorkingState_StatusTypes");

                entity.HasOne(d => d.SupportUserAccount)
                    .WithMany(p => p.SupportUserWorkingStates)
                    .HasForeignKey(d => d.SupportUserAccountId)
                    .HasConstraintName("FK_SupportUserWorkingState_SupportUserAccounts");
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IgnorePenaltyPerTypeId).HasColumnName("IgnorePenaltyPerTypeID");

                entity.Property(e => e.IgnorePerTypeId).HasColumnName("IgnorePerTypeID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.IgnorePenaltyPerType)
                    .WithMany(p => p.SystemSettings)
                    .HasForeignKey(d => d.IgnorePenaltyPerTypeId)
                    .HasConstraintName("FK_SystemSettings_PenaltyPerTypes");

                entity.HasOne(d => d.IgnorePerType)
                    .WithMany(p => p.SystemSettings)
                    .HasForeignKey(d => d.IgnorePerTypeId)
                    .HasConstraintName("FK_SystemSettings_IgnorePerTypes");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.TicketStatusTypeId).HasColumnName("TicketStatusTypeID");

                entity.Property(e => e.TicketTypeId).HasColumnName("TicketTypeID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_Tickets_CaptainUserAccounts");

                entity.HasOne(d => d.TicketStatusType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketStatusTypeId)
                    .HasConstraintName("FK_Tickets_TicketStatusTypes");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketTypeId)
                    .HasConstraintName("FK_Tickets_TicketTypes");
            });

            modelBuilder.Entity<TicketAssignment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.SupportUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("SupportUserAccountID");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.TicketStatusTypeId).HasColumnName("TicketStatusTypeID");

                entity.HasOne(d => d.CaptainUserAccount)
                    .WithMany(p => p.TicketAssignments)
                    .HasForeignKey(d => d.CaptainUserAccountId)
                    .HasConstraintName("FK_TicketAssignments_CaptainUserAccounts");

                entity.HasOne(d => d.SupportUserAccount)
                    .WithMany(p => p.TicketAssignments)
                    .HasForeignKey(d => d.SupportUserAccountId)
                    .HasConstraintName("FK_TicketAssignments_SupportUserAccounts");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketAssignments)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK_TicketAssignments_Tickets");

                entity.HasOne(d => d.TicketStatusType)
                    .WithMany(p => p.TicketAssignments)
                    .HasForeignKey(d => d.TicketStatusTypeId)
                    .HasConstraintName("FK_TicketAssignments_TicketStatusTypes");
            });

            modelBuilder.Entity<TicketMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.TicketAssignId).HasColumnName("TicketAssignID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.TicketAssign)
                    .WithMany(p => p.TicketMessages)
                    .HasForeignKey(d => d.TicketAssignId)
                    .HasConstraintName("FK_TicketMessages_TicketAssignments");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.TicketMessages)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK_TicketMessages_UserTypes");
            });

            modelBuilder.Entity<TicketStatusType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Transfers_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BookkeepingId).HasColumnName("BookkeepingID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.HasOne(d => d.Bookkeeping)
                    .WithMany(p => p.Transfers)
                    .HasForeignKey(d => d.BookkeepingId)
                    .HasConstraintName("FK_Transfers_Bookkeeping");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            modelBuilder.Entity<Webhook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.WebhookTypeId).HasColumnName("WebhookTypeID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Webhooks)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Webhooks_Agents");

                entity.HasOne(d => d.WebhookType)
                    .WithMany(p => p.Webhooks)
                    .HasForeignKey(d => d.WebhookTypeId)
                    .HasConstraintName("Fk_WebhooksTypes_Webhooks");
            });

            modelBuilder.Entity<WebhookType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArabicType).HasMaxLength(400);

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.Type).HasMaxLength(400);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
