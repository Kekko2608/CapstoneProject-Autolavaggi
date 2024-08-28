using CapstoneProject_Autolavaggi.Models;
using CapstoneProject_Autolavaggi.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_Autolavaggi.Context
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<Autolavaggio> Autolavaggi { get; set; }
        public virtual DbSet<Prenotazione> Prenotazioni { get; set; }
        public virtual DbSet<Orario> Orari { get; set; }
        public virtual DbSet<Recensione> Recensioni { get; set; }
        public virtual DbSet<Servizio> Servizi { get; set; }
        public virtual DbSet<Tipo> Tipi { get; set; }
        public virtual DbSet<ServizioPrenotazione> ServiziPrenotazioni { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=Autolavaggio;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Roles");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Users");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_UserRoles");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
