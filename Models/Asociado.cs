using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CSM_Registro.Models
{
    public class Asociado
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nombre Completo")]
        [StringLength(100)]
        public string NombreAsociado { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateOnly FechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Required]
        [Display(Name = "DNI")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener 8 dígitos.")]
        public string Dni { get; set; }

        //[Required]
        //[Display(Name = "País")]
        //public string Pais { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required]
        [Display(Name = "Base Zonal")]
        public string BaseZonal { get; set; }

        [Required]
        [Display(Name = "Número")]
        [Phone(ErrorMessage = "Número inválido")]
        [StringLength(15)]
        public string NumeroCelular { get; set; }

        [Required]
        [Display(Name = "Correo Actual")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        public string CorreoActual { get; set; }

        [Required]
        [Display(Name = "Ocupación")]
        public string Ocupacion { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }
        public string EstadoCivil { get; set; }

        [Required]
        [Display(Name = "Nivel de Instrucción")]
        public string NivelInstruccion { get; set; }
        public string LibretaMilitar { get; set; }
        public string NumeroRuc { get; set; }
        public string? FotoAsociado { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Foto Voucher")]
        public string? FotoVoucher { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Fecha de Aprobación")]
        [DataType(DataType.Date)]
        public DateTime? FechaAprobado { get; set; }

        public Conyuge? Conyuge { get; set; }
        public List<Hijo?> Hijos { get; set; } = new ();
        
    }
}
