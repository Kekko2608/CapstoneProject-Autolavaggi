using CapstoneProject_Autolavaggi.Models;
using CapstoneProject_Autolavaggi.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_Autolavaggi.Context
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<Autolavaggio> Autolavaggi { get; set; }
        public virtual DbSet<Prenotazione> Prenotazioni { get; set; }
        public virtual DbSet<Recensione> Recensioni { get; set; }
        public virtual DbSet<Servizio> Servizi { get; set; }
        public virtual DbSet<Tipo> Tipi { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
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

                // Configurazione della relazione con Prenotazione
                entity.HasMany(u => u.Prenotazioni)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevenzione di cicli
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

            modelBuilder.Entity<Prenotazione>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Prenotazioni");

                entity.HasOne(p => p.User)
                      .WithMany(u => u.Prenotazioni)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevenzione di cicli

                entity.HasOne(p => p.Autolavaggio)
                      .WithMany(a => a.Prenotazioni)
                      .HasForeignKey(p => p.AutolavaggioId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevenzione di cicli
            });

            modelBuilder.Entity<ServizioPrenotazione>()
            .HasOne(sp => sp.Servizio)
            .WithMany(s => s.ServizioPrenotazioni)
            .HasForeignKey(sp => sp.ServizioId);

            modelBuilder.Entity<ServizioPrenotazione>()
                .HasOne(sp => sp.Prenotazione)
                .WithMany(p => p.ServiziPrenotazione)
                .HasForeignKey(sp => sp.PrenotazioneId);

            modelBuilder.Entity<Autolavaggio>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Autolavaggi");

                entity.HasOne(a => a.Owner)
                      .WithMany() // un User può essere proprietario di più autolavaggi
                      .HasForeignKey(a => a.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

           
            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
