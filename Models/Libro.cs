using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaEva.Models
{
    public class Libro
    {
        [Key]
        [Column("libro_id")]
        [Display(Name = "ID del Libro")]
        public int LibroId { get; set; }

        [Column("titulo")]
        [StringLength(100)]
        [Unicode(false)]
        [Required(ErrorMessage = "El campo 'Título' es requerido")]
        [MinLength(3, ErrorMessage = "El campo 'Título' requiere mínimo 3 letras")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = null!;

        [Column("genero")]
        [StringLength(50)]
        [Unicode(false)]
        [Required(ErrorMessage = "El campo 'Género' es requerido")]
        [Display(Name = "Género")]
        public string? Genero { get; set; }

        [Column("fecha_publicacion")]
        [Required(ErrorMessage = "El campo 'Fecha' es requerido")]
        [Display(Name = "Fecha de Publicación")]
        public DateOnly? FechaPublicacion { get; set; }

        [Column("isbn")]
        [StringLength(20)]
        [Unicode(false)]
        [Required(ErrorMessage = "El campo 'ISBN' es requerido")]
        [Display(Name = "ISBN")]
        public string? Isbn { get; set; }

        [Column("autor_id")]
        [Display(Name = "ID del Autor")]
        public int? AutorId { get; set; }

        [ForeignKey("AutorId")]
        [InverseProperty("Libros")]
        [Display(Name = "Autor")]
        public virtual Autor? Autor { get; set; }
    }
}
