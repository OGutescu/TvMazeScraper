using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Infrastructure
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext()
        {
            
        }
        public TvMazeDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseIdentityColumns();
            builder.Entity<TvShow>(ConfigureTvShow);
            builder.Entity<Person>(ConfigurePerson);
            builder.Entity<Bookmark>(ConfigureBookmark);
        }

        private void ConfigureTvShow(EntityTypeBuilder<TvShow> builder)
        {
            builder.ToTable("TvShows");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseHiLo("tvshow_hilo")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();
            builder.Property(c => c.ShowId)
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasMany(c => c.Persons)
                .WithOne()
                .HasForeignKey("TvShowIdFk")
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigurePerson(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseHiLo("person_hilo")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(c => c.PersonId)
                .IsRequired();
            builder.Property(c =>c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(c => c.TvShow)
                .WithMany(c => c.Persons)
                .HasForeignKey(c=>c.TvShowIdFk);
        }

        private void ConfigureBookmark(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable("Bookmarks");
            builder.HasKey(c => c.PageNumber);
            builder.Property(c => c.PageNumber)
                .UseHiLo("bookmark_hilo")
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            builder.Property(c =>c.Status)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
