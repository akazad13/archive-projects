using TakeItToTheCloud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TakeItToTheCloud.DataAccess
{
    public class DataContext : IdentityDbContext<User, Role, int,
      IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
      IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>(u =>
            {
                u.ToTable(name: "Users");
                u.Property(x => x.UserName).HasMaxLength(20);
                u.Property(x => x.NormalizedUserName).HasMaxLength(20);
                u.Property(x => x.PhoneNumber).HasMaxLength(20);
                u.Property(x => x.Email).IsRequired().HasMaxLength(50);
                u.Property(x => x.NormalizedEmail).HasMaxLength(50);
            });

            builder.Entity<User>()
                .HasMany(u => u.UploadedFiles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Role>(r =>
            {
                r.ToTable(name: "Roles");
                r.Property(x => x.Name).HasMaxLength(20);
                r.Property(x => x.NormalizedName).HasMaxLength(20);
            });


            builder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable(name: "UserRoles");
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasIndex(x => new { x.ProviderKey, x.LoginProvider });
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

        }
    }
}
