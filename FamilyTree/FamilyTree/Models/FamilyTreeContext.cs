﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FamilyTree.Models
{
    public partial class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext()
        {
        }

        public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountReport> AccountReports { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventReport> EventReports { get; set; } = null!;
        public virtual DbSet<Family> Families { get; set; } = null!;
        public virtual DbSet<Relationship> Relationships { get; set; } = null!;
        public virtual DbSet<RelationshipDetail> RelationshipDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserJoin> UserJoins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionString"];
            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountReport>(entity =>
            {
                entity.ToTable("AccountReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EventName).HasMaxLength(200);

                entity.Property(e => e.Information).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventReport>(entity =>
            {
                entity.ToTable("EventReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventId).HasColumnName("EventID");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("Family");

                entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Information).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.ToTable("Relationship");

                entity.Property(e => e.RelationshipId)
                    .ValueGeneratedNever()
                    .HasColumnName("RelationshipID");

                entity.Property(e => e.RelationshipDetailId).HasColumnName("RelationshipDetailID");

                entity.Property(e => e.UserId1).HasColumnName("UserID_1");

                entity.Property(e => e.UserId2).HasColumnName("UserID_2");

                entity.HasOne(d => d.RelationshipDetail)
                    .WithMany(p => p.Relationships)
                    .HasForeignKey(d => d.RelationshipDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relationship_RelationshipDetail");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.RelationshipUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relationship_User");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.RelationshipUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relationship_User1");
            });

            modelBuilder.Entity<RelationshipDetail>(entity =>
            {
                entity.ToTable("RelationshipDetail");

                entity.Property(e => e.RelationshipDetailId).HasColumnName("RelationshipDetailID");

                entity.Property(e => e.RelationshipName)
                    .HasMaxLength(180)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Status).HasMaxLength(40);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FamilyId)
                    .HasConstraintName("FK_User_Family1");
            });

            modelBuilder.Entity<UserJoin>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.UserId })
                    .HasName("PK__UserJoin__A83C44BA78B7B8E8");

                entity.ToTable("UserJoin");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.UserJoins)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserJoin_Event");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserJoins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserJoin_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
