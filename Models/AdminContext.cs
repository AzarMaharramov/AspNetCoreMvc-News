using Microsoft.EntityFrameworkCore;

namespace MyFirstProject.Models
{
    public partial class AdminContext : DbContext
    {
        public AdminContext()
        {
        }

        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthUser> AuthUsers { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Admin;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("AuthUser");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Surname).HasMaxLength(30);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Image).HasMaxLength(70);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(32);

                entity.Property(e => e.Surname).HasMaxLength(30);

                entity.Property(e => e.Username).HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
