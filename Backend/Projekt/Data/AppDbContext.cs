using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Projekt.Models;


namespace Projekt.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<ForumSubcategory> ForumSubcategories { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<GalleryDisplaySequence> GalleryDisplaySequence { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehaviour", true);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ForumSubcategory>()
                .HasOne(c => c.ForumCategory)
                .WithMany(s => s.Subcategories)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();
            builder.Entity<ForumPost>()
                .HasOne(s => s.ForumSubcategory)
                .WithMany(p => p.Posts )
                .HasForeignKey(c => c.SubCategoryId);
            builder.Entity<ForumPost>()
                .HasOne(u => u.AppUser)
                .WithMany(p => p.Posts)
                .HasForeignKey(k => k.UserId);
            builder.Entity<ForumComment>()
                .HasOne(p => p.ForumPost)
                .WithMany(c => c.Comments )
                .HasForeignKey(k => k.PostId);
            builder.Entity<ForumComment>()
                .HasOne(u => u.AppUser)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.UserId);
            builder.Entity<LoanBook>()
                .HasKey(lb => new { lb.BookId, lb.LoanId });
        }
        
    }
}
