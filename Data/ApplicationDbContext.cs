using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvaEva.Models;

namespace EvaEva.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Las entidades personalizadas que has creado
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Esto es necesario para aplicar la configuración de Identity.

            // Aquí es donde puedes agregar configuraciones personalizadas para tus modelos, pero no es necesario para Identity.
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.AutorId);
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(e => e.LibroId);
                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.AutorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Libros_Autores");
            });
        }
    }
}
