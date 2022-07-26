using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoyaltyProgram.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<ConditionGroup> ConditionGroups { get; set; } = null!;
        public virtual DbSet<ConditionRule> ConditionRules { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<EventSource> EventSources { get; set; } = null!;
        public virtual DbSet<MemberReferrerLevel> MemberReferrerLevels { get; set; } = null!;
        public virtual DbSet<MemberTier> MemberTiers { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipCurrency> MembershipCurrencies { get; set; } = null!;
        public virtual DbSet<Noti> Notis { get; set; } = null!;
        public virtual DbSet<OrderAmountCondition> OrderAmountConditions { get; set; } = null!;
        public virtual DbSet<OrderItemCondition> OrderItemConditions { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Program> Programs { get; set; } = null!;
        public virtual DbSet<Reward> Rewards { get; set; } = null!;
        public virtual DbSet<Tier> Tiers { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<VoucherDefinition> VoucherDefinitions { get; set; } = null!;
        public virtual DbSet<VoucherWallet> VoucherWallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=13.232.213.53,1433;Database=Loyalty;user id=sa;password=Loyalty@Program");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("Action");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("date")
                    .HasColumnName("actionDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.MembershipRewardId).HasColumnName("membershipRewardId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.ReferrerId).HasColumnName("referrerId");

                entity.Property(e => e.ReferrerPoints).HasColumnName("referrerPoints");

                entity.Property(e => e.ReferrerRewardId).HasColumnName("referrerRewardId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Action_Program");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.MembershipId)
                    .HasConstraintName("FK_Action_Membership");

                entity.HasOne(d => d.MembershipReward)
                    .WithMany(p => p.ActionMembershipRewards)
                    .HasForeignKey(d => d.MembershipRewardId)
                    .HasConstraintName("FK_Action_Reward");

                entity.HasOne(d => d.ReferrerReward)
                    .WithMany(p => p.ActionReferrerRewards)
                    .HasForeignKey(d => d.ReferrerRewardId)
                    .HasConstraintName("FK_Action_Reward1");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Admin");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("email");

                entity.Property(e => e.ProgramId).HasColumnName("programID");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Brand_Organization");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.CardholderName)
                    .HasMaxLength(200)
                    .HasColumnName("cardholderName");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .HasColumnName("type");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Card_Brand");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Card_Currency");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.MembershipId)
                    .HasConstraintName("FK_Card_Membership");
            });

            modelBuilder.Entity<ConditionGroup>(entity =>
            {
                entity.ToTable("ConditionGroup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConditionRuleId).HasColumnName("conditionRuleId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("date")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.ConditionRule)
                    .WithMany(p => p.ConditionGroups)
                    .HasForeignKey(d => d.ConditionRuleId)
                    .HasConstraintName("FK_ConditionGroup_ConditionRule");
            });

            modelBuilder.Entity<ConditionRule>(entity =>
            {
                entity.ToTable("ConditionRule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.MaxPoints).HasColumnName("maxPoints");

                entity.Property(e => e.MinPointsForRedemption).HasColumnName("minPointsForRedemption");

                entity.Property(e => e.MinRedeemableAmount).HasColumnName("minRedeemableAmount");

                entity.Property(e => e.MinRedeemablePoints).HasColumnName("minRedeemablePoints");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SpendingValue).HasColumnName("spendingValue");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.ConditionRules)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_ConditionRule_Program");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.NextResetDate)
                    .HasColumnType("date")
                    .HasColumnName("nextResetDate");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.DeviceId });

                entity.ToTable("Device");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("deviceId");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Membership");
            });

            modelBuilder.Entity<EventSource>(entity =>
            {
                entity.HasKey(e => e.PartnerId);

                entity.ToTable("EventSource");

                entity.Property(e => e.PartnerId).HasColumnName("partnerId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<MemberReferrerLevel>(entity =>
            {
                entity.ToTable("MemberReferrerLevel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.RatioReferrerPoints).HasColumnName("ratioReferrerPoints");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TierSequenceNumber).HasColumnName("tierSequenceNumber");
            });

            modelBuilder.Entity<MemberTier>(entity =>
            {
                entity.ToTable("MemberTier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("effectiveDate");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("expirationDate");

                entity.Property(e => e.LoyaltyMemberId).HasColumnName("loyaltyMemberId");

                entity.Property(e => e.LoyaltyTierId).HasColumnName("loyaltyTierId");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateTierDate)
                    .HasColumnType("date")
                    .HasColumnName("updateTierDate");

                entity.HasOne(d => d.LoyaltyMember)
                    .WithMany(p => p.MemberTiers)
                    .HasForeignKey(d => d.LoyaltyMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberTier_Membership");

                entity.HasOne(d => d.LoyaltyTier)
                    .WithMany(p => p.MemberTiers)
                    .HasForeignKey(d => d.LoyaltyTierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberTier_Tier");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("Membership");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.CanReceivePromotions).HasColumnName("canReceivePromotions");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EnrollmentDate)
                    .HasColumnType("date")
                    .HasColumnName("enrollmentDate");

                entity.Property(e => e.LastTransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("lastTransactionDate");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.MembershipCode)
                    .HasMaxLength(400)
                    .HasColumnName("membershipCode");

                entity.Property(e => e.MembershipEndDate)
                    .HasColumnType("date")
                    .HasColumnName("membershipEndDate");

                entity.Property(e => e.ReferrerMemberDate)
                    .HasColumnType("date")
                    .HasColumnName("referrerMemberDate");

                entity.Property(e => e.ReferrerMemberId).HasColumnName("referrerMemberId");

                entity.Property(e => e.RefreshToken)
                    .HasColumnType("text")
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("date")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Membership_Program");
            });

            modelBuilder.Entity<MembershipCurrency>(entity =>
            {
                entity.ToTable("MembershipCurrency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BalanceBeforeReset).HasColumnName("balanceBeforeReset");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.ExpirationPoints).HasColumnName("expirationPoints");

                entity.Property(e => e.LastResetDate)
                    .HasColumnType("date")
                    .HasColumnName("lastResetDate");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.PointsBalance).HasColumnName("pointsBalance");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPointsAccrued).HasColumnName("totalPointsAccrued");

                entity.Property(e => e.TotalPointsExpired).HasColumnName("totalPointsExpired");

                entity.Property(e => e.TotalPointsRedeemed).HasColumnName("totalPointsRedeemed");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.MembershipCurrencies)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_MembershipCurrency_Currency");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.MembershipCurrencies)
                    .HasForeignKey(d => d.MembershipId)
                    .HasConstraintName("FK_MembershipCurrency_Membership");
            });

            modelBuilder.Entity<Noti>(entity =>
            {
                entity.ToTable("Noti");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.Body)
                    .HasMaxLength(800)
                    .HasColumnName("body");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.Title)
                    .HasMaxLength(400)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<OrderAmountCondition>(entity =>
            {
                entity.ToTable("OrderAmountCondition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConditionGroupId).HasColumnName("conditionGroupId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.MinOrderAmount).HasColumnName("minOrderAmount");

                entity.Property(e => e.NextOrderTotalAmount).HasColumnName("nextOrderTotalAmount");

                entity.Property(e => e.NextOrderTotalAmountAfterDiscont).HasColumnName("nextOrderTotalAmountAfterDiscont");

                entity.Property(e => e.OrderTotalAmountAfterDiscount).HasColumnName("orderTotalAmountAfterDiscount");

                entity.Property(e => e.OrderTotalAmountAfterDiscountGainPoint).HasColumnName("orderTotalAmountAfterDiscountGainPoint");

                entity.Property(e => e.OrderTotalAmountGainPoint).HasColumnName("orderTotalAmountGainPoint");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TierSequenceNumber).HasColumnName("tierSequenceNumber");

                entity.HasOne(d => d.ConditionGroup)
                    .WithMany(p => p.OrderAmountConditions)
                    .HasForeignKey(d => d.ConditionGroupId)
                    .HasConstraintName("FK_OrderAmountCondition_ConditionGroup");
            });

            modelBuilder.Entity<OrderItemCondition>(entity =>
            {
                entity.ToTable("OrderItemCondition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConditionGroupId).HasColumnName("conditionGroupId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.NextQuantity).HasColumnName("nextQuantity");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.QuantityGainPoint).HasColumnName("quantityGainPoint");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TierSequenceNumber).HasColumnName("tierSequenceNumber");

                entity.HasOne(d => d.ConditionGroup)
                    .WithMany(p => p.OrderItemConditions)
                    .HasForeignKey(d => d.ConditionGroupId)
                    .HasConstraintName("FK_OrderItemCondition_ConditionGroup");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Program_Brand");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("Reward");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Images)
                    .HasColumnType("text")
                    .HasColumnName("images");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Parameters)
                    .HasColumnType("text")
                    .HasColumnName("parameters");

                entity.Property(e => e.Redeemed).HasColumnName("redeemed");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .HasColumnName("type");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Reward_Program");
            });

            modelBuilder.Entity<Tier>(entity =>
            {
                entity.ToTable("Tier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.MinPoints).HasColumnName("minPoints");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.RatioPoints).HasColumnName("ratioPoints");

                entity.Property(e => e.SequenceNumber).HasColumnName("sequenceNumber");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Tiers)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Tier_Program");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CardId).HasColumnName("cardId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transactionDate");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_Transaction_Card");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MembershipId)
                    .HasConstraintName("FK_Transaction_Membership");
            });

            modelBuilder.Entity<VoucherDefinition>(entity =>
            {
                entity.ToTable("VoucherDefinition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.DiscountValue).HasColumnName("discountValue");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("effectiveDate");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("expirationDate");

                entity.Property(e => e.ExpirationPeriod).HasColumnName("expirationPeriod");

                entity.Property(e => e.ExpirationPeriodUnits)
                    .HasMaxLength(50)
                    .HasColumnName("expirationPeriodUnits");

                entity.Property(e => e.Image)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.IsPartialRedeemable).HasColumnName("isPartialRedeemable");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.VoucherCode)
                    .HasColumnType("text")
                    .HasColumnName("voucherCode");
            });

            modelBuilder.Entity<VoucherWallet>(entity =>
            {
                entity.HasKey(e => new { e.MembershipId, e.VoucherDefinitionId });

                entity.ToTable("VoucherWallet");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.VoucherDefinitionId).HasColumnName("voucherDefinitionId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.IsPartialRedeemable).HasColumnName("isPartialRedeemable");

                entity.Property(e => e.RedeemedValue).HasColumnName("redeemedValue");

                entity.Property(e => e.RemainingValue).HasColumnName("remainingValue");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UseDate)
                    .HasColumnType("date")
                    .HasColumnName("useDate");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.VoucherWallets)
                    .HasForeignKey(d => d.MembershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherWallet_Membership");

                entity.HasOne(d => d.VoucherDefinition)
                    .WithMany(p => p.VoucherWallets)
                    .HasForeignKey(d => d.VoucherDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherWallet_VoucherDefinition");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
