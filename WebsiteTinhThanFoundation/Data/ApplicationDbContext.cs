using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebsiteTinhThanFoundation.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Registeredvolunteers>? Registeredvolunteers { get; set; } = null!;
        public DbSet<BlogArticle> BlogArticles { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<BlogArticleTag> BlogArticleTags { get; set; } = null!;
        public DbSet<BlogArticleComment> BlogArticleComments { get; set; } = null!;

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Id = "a19e491a-02b5-4f8b-8aec-ac2becb88eca", Name = Constants.Roles.Admin, NormalizedName = Constants.Roles.Admin, ConcurrencyStamp = null }
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
            SeedUser(modelBuilder);
            SeedUserRole(modelBuilder);
            modelBuilder.Entity<BlogArticleTag>()
            .HasKey(m => new { m.BlogArticleId, m.TagId });

            modelBuilder.Entity<BlogArticle>()
            .HasIndex(m => new { m.Permalink })
            .IsUnique(true);


            modelBuilder.Entity<Tag>()
            .HasIndex(m => new { m.Name })
            .IsUnique(true);
        }

        private void SeedUser(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048",
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "admin@tiemkiet.vn",
                    NormalizedEmail = "admin@tiemkiet.vn",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAECAsUeOByw0jsD4x7X0K9WQdxWV/RrvPBnHITnRzdbrhHKzmf35BZDPXJBcVjp5FIQ==", //Admin@123
                    SecurityStamp = "ZD5UZJQK6Q5W6N7O6RBRF6DB2Q2G2AIJ",
                    ConcurrencyStamp = "b19f1b24-5ac9-4c8d-9b7c-5e5d5f5cfb1e",
                    FullName = "Admin",
                    TwoFactorEnabled = false,
                    PhoneNumber = "0923425148",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
        }

        private void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048",
                    RoleId = "a19e491a-02b5-4f8b-8aec-ac2becb88eca"
                }
            );
        }
    }
}
