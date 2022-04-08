using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FarmManagementApp.Models
{
    public partial class FarmContext : DbContext
    {
        private readonly IConfiguration _config;

        public FarmContext()
        {
        }

        public FarmContext(DbContextOptions<FarmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connString = _config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__Farm__A2B5777CBDA2C794");

                entity.ToTable("Farm");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MainEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MainPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.PostalZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Postal_Zip_Code");

                entity.Property(e => e.StateProv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("State_Prov");

                entity.HasOne(d => d.HeadManagerNavigation)
                    .WithMany(p => p.Farms)
                    .HasForeignKey(d => d.HeadManager)
                    .HasConstraintName("FK__Farm__HeadManage__5165187F");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__Role__A2B5777CE0B30C4B");

                entity.ToTable("Role");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__User__A2B5777C1293CC06");

                entity.ToTable("User");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.FarmGu)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FarmGuid)
                    .HasConstraintName("FK__User__FarmGuid__52593CB8");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserGuid)
                    .HasName("PK__UserProf__99B7F23A7F73AF83");

                entity.ToTable("UserProfile");

                entity.Property(e => e.UserGuid).ValueGeneratedNever();

                entity.Property(e => e.Extension)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.RoleGu)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.RoleGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__RoleG__4D94879B");

                entity.HasOne(d => d.UserGu)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__UserG__4CA06362");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
