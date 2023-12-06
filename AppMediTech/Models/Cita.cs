using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMediTech.Models
{
    public class Cita
    {
        [Key]
        public int CitaID { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        [Required]
        [StringLength(200)]
        public string Motivo { get; set; }

        // Relación con la entidad de Paciente (muchos a uno)
        public int PacienteID { get; set; }
        public Paciente Paciente { get; set; }

        // Relación con la entidad de Doctor (muchos a uno)
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public string NotasDoctor { get; set; }
    }
}
