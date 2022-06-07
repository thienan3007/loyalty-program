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
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<ConditionGroup> ConditionGroups { get; set; } = null!;
        public virtual DbSet<ConditionRule> ConditionRules { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<EventSource> EventSources { get; set; } = null!;
        public virtual DbSet<MemberReferrerLevel> MemberReferrerLevels { get; set; } = null!;
        public virtual DbSet<MemberTier> MemberTiers { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipCurrency> MembershipCurrencies { get; set; } = null!;
        public virtual DbSet<OrderAmountCondition> OrderAmountConditions { get; set; } = null!;
        public virtual DbSet<OrderItemCondition> OrderItemConditions { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
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
                optionsBuilder.UseSqlServer("Server=tcp:loyaltyprogram.database.windows.net,1433;Initial Catalog=Loyalty;Persist Security Info=False;User ID=azureuser;Password=Loyalty@Program;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
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
                    .HasColumnName("action_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyalty_program_id");

                entity.Property(e => e.MembershipId).HasColumnName("membership_id");

                entity.Property(e => e.MembershipRewardId).HasColumnName("membership_reward_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.ReferrerId).HasColumnName("referrer_id");

                entity.Property(e => e.ReferrerPoints).HasColumnName("referrer_points");

                entity.Property(e => e.ReferrerRewardId).HasColumnName("referrer_reward_id");

                entity.Property(e => e.RewardId).HasColumnName("reward_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .HasColumnName("type");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Action_Program");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.MembershipId)
                    .HasConstraintName("FK_Action_Membership");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.RewardId)
                    .HasConstraintName("FK_Action_Reward");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Brand_Organization");
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
                    .HasColumnType("text")
                    .HasColumnName("description");

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
                    .HasColumnType("text")
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

                entity.Property(e => e.SpendingValue).HasColumnName("spendingValue");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.ConditionRules)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_ConditionRule_LoyaltyProgram");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.NextResetDate)
                    .HasColumnType("date")
                    .HasColumnName("nextResetDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Currencies)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Currency_LoyaltyProgram1");
            });

            modelBuilder.Entity<EventSource>(entity =>
            {
                entity.HasKey(e => e.PartnerId);

                entity.ToTable("EventSource");

                entity.Property(e => e.PartnerId)
                    .ValueGeneratedNever()
                    .HasColumnName("partner_id");

                entity.Property(e => e.Description).HasColumnName("description");

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
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.RatioReferrerPoints).HasColumnName("ratioReferrerPoints");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TierSequenceNumber).HasColumnName("tierSequenceNumber");
            });

            modelBuilder.Entity<MemberTier>(entity =>
            {
                entity.HasKey(e => new { e.LoyaltyMemberId, e.LoyaltyTierId });

                entity.ToTable("MemberTier");

                entity.Property(e => e.LoyaltyMemberId).HasColumnName("loyaltyMemberId");

                entity.Property(e => e.LoyaltyTierId).HasColumnName("loyaltyTierId");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("effectiveDate");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("expirationDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UdpateTierDate)
                    .HasColumnType("date")
                    .HasColumnName("udpateTierDate");

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
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EnrollmenDate)
                    .HasColumnType("date")
                    .HasColumnName("enrollmenDate");

                entity.Property(e => e.LastTransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("lastTransactionDate");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.MembershipCode)
                    .HasColumnType("text")
                    .HasColumnName("membershipCode");

                entity.Property(e => e.MembershipEndDate)
                    .HasColumnType("date")
                    .HasColumnName("membershipEndDate");

                entity.Property(e => e.ReferrerMemberDate)
                    .HasColumnType("date")
                    .HasColumnName("referrerMemberDate");

                entity.Property(e => e.ReferrerMemberId).HasColumnName("referrerMemberId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Membership_LoyaltyProgram");
            });

            modelBuilder.Entity<MembershipCurrency>(entity =>
            {
                entity.ToTable("MembershipCurrency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BalanceBeforeReset).HasColumnName("balanceBeforeReset");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyId");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.ExpirationPoints).HasColumnName("expirationPoints");

                entity.Property(e => e.LastResetDate)
                    .HasColumnType("date")
                    .HasColumnName("lastResetDate");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
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

            modelBuilder.Entity<OrderAmountCondition>(entity =>
            {
                entity.ToTable("OrderAmountCondition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConditionGroupId).HasColumnName("conditionGroupId");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.MinOrderAmount).HasColumnName("minOrderAmount");

                entity.Property(e => e.NextOrderTotalAmount).HasColumnName("nextOrderTotalAmount");

                entity.Property(e => e.NextOrderTotalAmountAfterDiscount).HasColumnName("nextOrderTotalAmountAfterDiscount");

                entity.Property(e => e.OrderTotalAmount).HasColumnName("orderTotalAmount");

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
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.NextQuantity).HasColumnName("nextQuantity");

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
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_LoyaltyProgram_Brand");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("Reward");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Images).HasColumnName("images");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyalty_program_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Paramaters).HasColumnName("paramaters");

                entity.Property(e => e.Redeemed).HasColumnName("redeemed");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
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
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.LoyaltyProgramId).HasColumnName("loyaltyProgramId");

                entity.Property(e => e.MinPoints).HasColumnName("minPoints");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.RatioPoints).HasColumnName("ratioPoints");

                entity.Property(e => e.SequenceNumber).HasColumnName("sequenceNumber");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.LoyaltyProgram)
                    .WithMany(p => p.Tiers)
                    .HasForeignKey(d => d.LoyaltyProgramId)
                    .HasConstraintName("FK_Tier_LoyaltyProgram");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.MemberCurrencyId).HasColumnName("memberCurrencyId");

                entity.Property(e => e.MembershipId).HasColumnName("membershipID");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.ReferrerId).HasColumnName("referrerId");

                entity.Property(e => e.ReferrerPoints).HasColumnName("referrerPoints");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transactionDate");

                entity.HasOne(d => d.MemberCurrency)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MemberCurrencyId)
                    .HasConstraintName("FK_Transaction_MembershipCurrency");
            });

            modelBuilder.Entity<VoucherDefinition>(entity =>
            {
                entity.ToTable("VoucherDefinition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
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
                    .HasMaxLength(10)
                    .HasColumnName("expirationPeriodUnits")
                    .IsFixedLength();

                entity.Property(e => e.IsPartialRedeemable).HasColumnName("isPartialRedeemable");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.VoucherCode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("voucherCode");
            });

            modelBuilder.Entity<VoucherWallet>(entity =>
            {
                entity.HasKey(e => new { e.MembershipId, e.VoucherDefinitionId });

                entity.ToTable("VoucherWallet");

                entity.Property(e => e.MembershipId).HasColumnName("membershipId");

                entity.Property(e => e.VoucherDefinitionId).HasColumnName("voucherDefinitionId");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.IsPartialRedeemable).HasColumnName("isPartialRedeemable");

                entity.Property(e => e.RedeemedValue).HasColumnName("redeemedValue");

                entity.Property(e => e.RemainingValue).HasColumnName("remainingValue");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UseDate)
                    .HasColumnType("date")
                    .HasColumnName("useDate");
            });

            modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
