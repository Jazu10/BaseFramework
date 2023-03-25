using Backend.Core.Common.ErrorHandlers;
using Backend.Core.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedUsers(modelBuilder);
            SeedRole(modelBuilder);
            SeedUserRole(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            IdentityUser user = new IdentityUser()
            {
                Id = "056f40f2-5fc9-41fb-8036-f1670e7344a3",
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "1234567890",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM"
            };
            //PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            var password = new PasswordHasher<IdentityUser>();
            var hashead = password.HashPassword(user, "Admin@123");
            user.PasswordHash = hashead;

            UserModel model = new UserModel()
            {
                UserId = user.Id,
                FirstName = "Root",
                LastName = "User",
                Gender = "Male",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Image = "",
                Address = "UIS Global"
            };

            modelBuilder.Entity<IdentityUser>().HasData(user);
            modelBuilder.Entity<UserModel>().HasData(model);

        }
        private void SeedRole(ModelBuilder modelBuilder)
        {
            IdentityRole role = new IdentityRole()
            {
                Id = "e46f1bfb-a425-4438-831c-6e7ab5dc46bb",
                ConcurrencyStamp = "1",
                Name = "superuser",
                NormalizedName = "SUPERUSER"
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);
        }
        private void SeedUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "e46f1bfb-a425-4438-831c-6e7ab5dc46bb",
                UserId = "056f40f2-5fc9-41fb-8036-f1670e7344a3"
            });
        }

        public DbSet<UserModel> UserList { get; set; }

        //Comment These Lines Before Migration
        public DbSet<NewsModel> NewsList { get; set; }
        public DbSet<AdvertisementModel> AdvertisementList { get; set; }
        public DbSet<ImageModel> ImageList { get; set; }
        public DbSet<PostModel> PostList { get; set; }
        public DbSet<CommentModel> CommentList { get; set; }
        //public DbSet<PostModel> LikeList { get; set; }

    }
}
