using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class PostDbContext : DbContext
    {
        public PostDbContext()
        {
        }

        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Equip> Equips { get; set; }
        public virtual DbSet<Jugador> Jugadors { get; set; }
        public virtual DbSet<Partit> Partits { get; set; }
        public virtual DbSet<Penye> Penyes { get; set; }
        public virtual DbSet<Penyiste> Penyistes { get; set; }
        public virtual DbSet<Porre> Porres { get; set; }
        public virtual DbSet<Puntuacion> Puntuacions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql(Startup.ConnectionStrings, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.21-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<Equip>(entity =>
            {
                entity.HasKey(e => e.Idequip)
                    .HasName("PRIMARY");

                entity.ToTable("equips");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.Property(e => e.Idequip)
                    .HasColumnType("int(11)")
                    .HasColumnName("idequip");

                entity.Property(e => e.Imatge)
                    .HasColumnType("mediumblob")
                    .HasColumnName("imatge");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.HasKey(e => e.Idjugador)
                    .HasName("PRIMARY");

                entity.ToTable("jugadors");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Idequip, "idequip");

                entity.Property(e => e.Idjugador)
                    .HasColumnType("int(11)")
                    .HasColumnName("idjugador");

                entity.Property(e => e.Idequip)
                    .HasColumnType("int(11)")
                    .HasColumnName("idequip");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");

                entity.Property(e => e.Temporada)
                    .HasMaxLength(50)
                    .HasColumnName("temporada");

                entity.HasOne(d => d.IdequipNavigation)
                    .WithMany(p => p.Jugadors)
                    .HasForeignKey(d => d.Idequip)
                    .HasConstraintName("jugadors_ibfk_2");
            });

            modelBuilder.Entity<Partit>(entity =>
            {
                entity.HasKey(e => e.Idpartit)
                    .HasName("PRIMARY");

                entity.ToTable("partits");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Idequiplocal, "idlocal");

                entity.HasIndex(e => e.Idequipvisitant, "idvisitant");

                entity.Property(e => e.Idpartit)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpartit");

                entity.Property(e => e.Datainici)
                    .HasColumnType("datetime")
                    .HasColumnName("datainici");

                entity.Property(e => e.Finalitzat)
                    .HasMaxLength(10)
                    .HasColumnName("finalitzat");

                entity.Property(e => e.Golslocal)
                    .HasColumnType("int(11)")
                    .HasColumnName("golslocal");

                entity.Property(e => e.Golsvisitant)
                    .HasColumnType("int(11)")
                    .HasColumnName("golsvisitant");

                entity.Property(e => e.Idequiplocal)
                    .HasColumnType("int(11)")
                    .HasColumnName("idequiplocal");

                entity.Property(e => e.Idequipvisitant)
                    .HasColumnType("int(11)")
                    .HasColumnName("idequipvisitant");

                entity.Property(e => e.Idsjugadorslocal)
                    .HasMaxLength(100)
                    .HasColumnName("idsjugadorslocal");

                entity.Property(e => e.Idsjugadorsvisitant)
                    .HasMaxLength(100)
                    .HasColumnName("idsjugadorsvisitant");

                entity.Property(e => e.Jornada)
                    .HasColumnType("int(11)")
                    .HasColumnName("jornada");

                entity.Property(e => e.Temporada)
                    .HasMaxLength(50)
                    .HasColumnName("temporada");

                entity.HasOne(d => d.IdequiplocalNavigation)
                    .WithMany(p => p.PartitIdequiplocalNavigations)
                    .HasForeignKey(d => d.Idequiplocal)
                    .HasConstraintName("partits_ibfk_4");

                entity.HasOne(d => d.IdequipvisitantNavigation)
                    .WithMany(p => p.PartitIdequipvisitantNavigations)
                    .HasForeignKey(d => d.Idequipvisitant)
                    .HasConstraintName("partits_ibfk_5");
            });

            modelBuilder.Entity<Penye>(entity =>
            {
                entity.HasKey(e => e.Idpenya)
                    .HasName("PRIMARY");

                entity.ToTable("penyes");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.Property(e => e.Idpenya)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpenya");

                entity.Property(e => e.Nom)
                    .HasMaxLength(20)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Penyiste>(entity =>
            {
                entity.HasKey(e => e.Idpenyista)
                    .HasName("PRIMARY");

                entity.ToTable("penyistes");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Idpenya, "idpenya");

                entity.Property(e => e.Idpenyista)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpenyista");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .HasColumnName("alias");

                entity.Property(e => e.Cognoms)
                    .HasMaxLength(50)
                    .HasColumnName("cognoms");

                entity.Property(e => e.Dataalta)
                    .HasColumnType("datetime")
                    .HasColumnName("dataalta");

                entity.Property(e => e.Idpenya)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpenya");

                entity.Property(e => e.Imatge)
                    .HasColumnType("mediumblob")
                    .HasColumnName("imatge");

                entity.Property(e => e.Nif)
                    .HasMaxLength(15)
                    .HasColumnName("nif");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");

                entity.Property(e => e.Numsoci)
                    .HasMaxLength(50)
                    .HasColumnName("numsoci");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Rol)
                    .HasMaxLength(15)
                    .HasColumnName("rol");

                entity.HasOne(d => d.IdpenyaNavigation)
                    .WithMany(p => p.Penyistes)
                    .HasForeignKey(d => d.Idpenya)
                    .HasConstraintName("penyistes_ibfk_2");
            });

            modelBuilder.Entity<Porre>(entity =>
            {
                entity.HasKey(e => e.Idporra)
                    .HasName("PRIMARY");

                entity.ToTable("porres");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Idpartit, "idpartit");

                entity.HasIndex(e => e.Idpenyista, "idpenyista");

                entity.Property(e => e.Idporra)
                    .HasColumnType("int(11)")
                    .HasColumnName("idporra");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("data");

                entity.Property(e => e.Golslocal)
                    .HasColumnType("int(11)")
                    .HasColumnName("golslocal");

                entity.Property(e => e.Golsvisitant)
                    .HasColumnType("int(11)")
                    .HasColumnName("golsvisitant");

                entity.Property(e => e.Idpartit)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpartit");

                entity.Property(e => e.Idpenyista)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpenyista");

                entity.Property(e => e.Idsgolejadorslocal)
                    .HasMaxLength(100)
                    .HasColumnName("idsgolejadorslocal");

                entity.Property(e => e.Idsgolejadorsvisitant)
                    .HasMaxLength(100)
                    .HasColumnName("idsgolejadorsvisitant");

                entity.HasOne(d => d.IdpartitNavigation)
                    .WithMany(p => p.Porres)
                    .HasForeignKey(d => d.Idpartit)
                    .HasConstraintName("porres_ibfk_4");

                entity.HasOne(d => d.IdpenyistaNavigation)
                    .WithMany(p => p.Porres)
                    .HasForeignKey(d => d.Idpenyista)
                    .HasConstraintName("porres_ibfk_3");
            });

            modelBuilder.Entity<Puntuacion>(entity =>
            {
                entity.HasKey(e => e.Idpuntuacio)
                    .HasName("PRIMARY");

                entity.ToTable("puntuacions");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Idpuntuacio)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpuntuacio");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("alias");

                entity.Property(e => e.Idpenyista)
                    .HasColumnType("int(11)")
                    .HasColumnName("idpenyista");

                entity.Property(e => e.Puntuacio)
                    .HasColumnType("int(11)")
                    .HasColumnName("puntuacio");

                entity.Property(e => e.Temporada)
                    .HasMaxLength(50)
                    .HasColumnName("temporada");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
