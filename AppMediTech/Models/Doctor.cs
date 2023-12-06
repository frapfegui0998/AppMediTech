using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMediTech.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [StringLength(100)]
        public string Especialidad { get; set; }

        public int Experiencia { get; set; }


        // Relación con la entidad de Cita (uno a muchos)
        [InverseProperty("Doctor")]
        public ICollection<Cita> Citas { get; set; }
    }
}
