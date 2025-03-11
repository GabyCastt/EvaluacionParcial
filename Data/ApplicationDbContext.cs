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
        
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

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
