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
                    .WithMany(p => p.CaptainUsers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__CaptainUs__CityI__42E1EEFE");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CaptainUsers)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__CaptainUs__Count__41EDCAC5");
            });

            modelBuilder.Entity<CaptainUserAcceptedRequest>(entity =>
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
            });

            modelBuilder.Entity<CaptainUserActiveHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptainUserAccountId)
                    .HasMaxLength(400)
                    .HasColumnName("CaptainUserAccountID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);
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

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
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
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponTypeId).HasColumnName("CouponTypeID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);
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
            });

            modelBuilder.Entity<CouponType>(entity =>
            {
                entity.ToTable("CouponType");

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

                entity.Property(e => e.CurrentOrderStatusTypeId).HasColumnName("CurrentOrderStatusTypeID");

                entity.Property(e => e.CustomerName).HasMaxLength(400);

                entity.Property(e => e.CustomerPhone).HasMaxLength(400);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.ProductOtherTypeInfo).HasMaxLength(400);

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
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
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(400);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IgnorPenaltyPerTypeId).HasColumnName("IgnorPenaltyPerTypeID");

                entity.Property(e => e.IgnorPerTypeId).HasColumnName("IgnorPerTypeID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(400);
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
