#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RTMS.Entities.PhuocDH.Models;

namespace RTMS.Repositories.PhuocDH.Models;

public partial class RTMSContext : DbContext
{
    public RTMSContext()
    {
    }

    public RTMSContext(DbContextOptions<RTMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HealthRecord> HealthRecords { get; set; }

    public virtual DbSet<Hourse> Hourses { get; set; }

    public virtual DbSet<MetricType> MetricTypes { get; set; }

    public virtual DbSet<PerformanceMetric> PerformanceMetrics { get; set; }

    public virtual DbSet<RacePhuocDh> RacePhuocDhs { get; set; }

    public virtual DbSet<RaceResultPhuocDh> RaceResultPhuocDhs { get; set; }

    public virtual DbSet<Stable> Stables { get; set; }

    public virtual DbSet<SystemUserAccount> SystemUserAccounts { get; set; }

    public virtual DbSet<TraningPlan> TraningPlans { get; set; }

    public virtual DbSet<TranninSession> TranninSessions { get; set; }

    public virtual DbSet<Treatment> Treatments { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-JP4QSV9A;Initial Catalog=RTMS;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=False");

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HealthRecord>(entity =>
        {
            entity.HasKey(e => e.HealthRecordId).HasName("PK__HealthRe__3BE0B8BD61650D1C");

            entity.ToTable("HealthRecord");

            entity.Property(e => e.Diagnosis).HasMaxLength(255);
            entity.Property(e => e.RecordDate).HasColumnType("datetime");
            entity.Property(e => e.TreatmentCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Hourse).WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.HourseId)
                .HasConstraintName("FK_HealthRecord_Hourse");
        });

        modelBuilder.Entity<Hourse>(entity =>
        {
            entity.HasKey(e => e.HourseId).HasName("PK__Hourse__99F259603F224632");

            entity.ToTable("Hourse");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Pedigree).HasMaxLength(255);
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<MetricType>(entity =>
        {
            entity.HasKey(e => e.MetricTypeId).HasName("PK__MetricTy__79DBA064E35F3430");

            entity.ToTable("MetricType");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Unit).HasMaxLength(255);
        });

        modelBuilder.Entity<PerformanceMetric>(entity =>
        {
            entity.HasKey(e => e.PerformanceMetricId).HasName("PK__Performa__1016D7E9A8C120CF");

            entity.ToTable("Performance_Metric");

            entity.Property(e => e.PerformanceMetricId).HasColumnName("Performance_MetricId");
            entity.Property(e => e.EvaluationNote).HasMaxLength(255);
            entity.Property(e => e.RecordedAt).HasColumnType("datetime");
            entity.Property(e => e.TranninSessionId).HasColumnName("Trannin_SessionId");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MetricType).WithMany(p => p.PerformanceMetrics)
                .HasForeignKey(d => d.MetricTypeId)
                .HasConstraintName("FK_Performance_Metric_MetricType");

            entity.HasOne(d => d.TranninSession).WithMany(p => p.PerformanceMetrics)
                .HasForeignKey(d => d.TranninSessionId)
                .HasConstraintName("FK_Performance_Metric_Trannin_Session");
        });

        modelBuilder.Entity<RacePhuocDh>(entity =>
        {
            entity.HasKey(e => e.RacePhuocDhid).HasName("PK__RacePhuo__628E3B26CD9BFB46");

            entity.ToTable("RacePhuocDH");

            entity.Property(e => e.RacePhuocDhid).HasColumnName("RacePhuocDHId");
            entity.Property(e => e.PrizePool).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RaceDate).HasColumnType("datetime");
            entity.Property(e => e.RaceName).HasMaxLength(255);
        });

        modelBuilder.Entity<RaceResultPhuocDh>(entity =>
        {
            entity.HasKey(e => e.RaceResultPhuocDhid).HasName("PK__RaceResu__87515A5FCF7B8CE5");

            entity.ToTable("RaceResultPhuocDH");

            entity.Property(e => e.RaceResultPhuocDhid).HasColumnName("RaceResultPhuocDHId");
            entity.Property(e => e.PrizeEarned).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RacePhuocDhid).HasColumnName("RacePhuocDHId");

            entity.HasOne(d => d.Hourse).WithMany(p => p.RaceResultPhuocDhs)
                .HasForeignKey(d => d.HourseId)
                .HasConstraintName("FK_RaceResultPhuocDH_Hourse");

            entity.HasOne(d => d.RacePhuocDh).WithMany(p => p.RaceResultPhuocDhs)
                .HasForeignKey(d => d.RacePhuocDhid)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_RaceResultPhuocDH_RacePhuocDH");
        });

        modelBuilder.Entity<Stable>(entity =>
        {
            entity.HasKey(e => e.StableId).HasName("PK__stable__5CC4FC9C5F47028F");

            entity.ToTable("stable");

            entity.Property(e => e.StableId).HasColumnName("stableId");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.StableCode).HasMaxLength(255);
            entity.Property(e => e.TaskType).HasMaxLength(255);

            entity.HasOne(d => d.Hourse).WithMany(p => p.Stables)
                .HasForeignKey(d => d.HourseId)
                .HasConstraintName("FK_stable_Hourse");
        });

        modelBuilder.Entity<SystemUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__System.U__DA6C70BA2ECD55A2");

            entity.ToTable("System.UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TraningPlan>(entity =>
        {
            entity.HasKey(e => e.TraningPlanId).HasName("PK__Traning___74350135C271CD0B");

            entity.ToTable("Traning_Plan");

            entity.Property(e => e.TraningPlanId).HasColumnName("Traning_PlanId");
            entity.Property(e => e.PhaseName).HasMaxLength(255);
            entity.Property(e => e.TargetDistance).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Hourse).WithMany(p => p.TraningPlans)
                .HasForeignKey(d => d.HourseId)
                .HasConstraintName("FK_Traning_Plan_Hourse");
        });

        modelBuilder.Entity<TranninSession>(entity =>
        {
            entity.HasKey(e => e.TranninSessionId).HasName("PK__Trannin___9FF341642B3F1D05");

            entity.ToTable("Trannin_Session");

            entity.Property(e => e.TranninSessionId).HasColumnName("Trannin_SessionId");
            entity.Property(e => e.Distance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SessionDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.TraningPlanId).HasColumnName("Traning_PlanId");

            entity.HasOne(d => d.TraningPlan).WithMany(p => p.TranninSessions)
                .HasForeignKey(d => d.TraningPlanId)
                .HasConstraintName("FK_Trannin_Session_Traning_Plan");
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.HasKey(e => e.TreatmentId).HasName("PK__Treatmen__1A57B7F1A4C7D8CC");

            entity.ToTable("Treatment");

            entity.Property(e => e.Dosage).HasMaxLength(255);
            entity.Property(e => e.Medication).HasMaxLength(255);

            entity.HasOne(d => d.HealthRecord).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.HealthRecordId)
                .HasConstraintName("FK_Treatment_HealthRecord");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}