using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvaEva.Models
{
    public class Autor
    {
        [Key]
        [Column("autor_id")]
        [Display(Name = "ID del Autor")]
        public int AutorId { get; set; }

        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        [Required(ErrorMessage = "El campo 'Nombre' es requerido")]
        [MinLength(3, ErrorMessage = "El campo 'Nombre' requiere mínimo 3 letras")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = null!;

        [Column("apellido")]
        [StringLength(50)]
        [Unicode(false)]
        [Required(ErrorMessage = "El campo 'Apellido' es requerido")]
        [MinLength(3, ErrorMessage = "El campo 'Apellido' requiere mínimo 3 letras")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = null!;

        [Column("fecha_nacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateOnly? FechaNacimiento { get; set; }

        [Column("nacionalidad")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Nacionalidad")]
        public string? Nacionalidad { get; set; }

        [InverseProperty("Autor")]
        [Display(Name = "Libros del Autor")]
        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}

