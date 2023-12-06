using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMediTech.Models
{
    public class Paciente
    {
        [Key]
        public int PacienteID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        public int Edad { get; set; }

        [Required]
        [StringLength(100)]
        public string Dirección { get; set; }

        public string HistorialMédico { get; set; }


        // Relación con la entidad de Cita (uno a muchos)
        [InverseProperty("Paciente")]
        public ICollection<Cita> Citas { get; set; }
    }
}
