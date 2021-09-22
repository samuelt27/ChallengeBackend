using ChallengeBackend.WebAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ChallengeBackend.WebAPI.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCharacter> MovieCharacters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configuration(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        private void Configuration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(i =>
            {
                i.HasKey(i => i.Id);

                i.Property(i => i.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });


            modelBuilder.Entity<Movie>(i =>
            {
                i.HasKey(i => i.Id);

                i.Property(i => i.Title)
                    .IsRequired()
                    .HasMaxLength(120);

                i.Property(i => i.Release)
                    .HasColumnType("DATE")
                    .IsRequired();

                i.Property(i => i.Rating)
                    .HasColumnType("DECIMAL(2, 1)")
                    .IsRequired();

                i.Property(i => i.Image)
                    .HasMaxLength(240)
                    .IsRequired();
            });


            modelBuilder.Entity<Character>(i =>
            {
                i.HasKey(i => i.Id);

                i.Property(i => i.Name)
                    .HasMaxLength(120)
                    .IsRequired();

                i.Property(i => i.Age)
                    .HasColumnType("TINYINT")
                    .IsRequired();

                i.Property(i => i.Story)
                    .HasMaxLength(1200)
                    .IsRequired();

                i.Property(i => i.Image)
                    .HasMaxLength(240)
                    .IsRequired();
            });


            modelBuilder.Entity<MovieGenre>().HasKey(i => new { i.GenreId, i.MovieId });


            modelBuilder.Entity<MovieCharacter>().HasKey(i => new { i.MovieId, i.CharacterId });
        }
    }
}
