namespace CSM_Registro.Models
{
    public class FiltroEstadoViewModel
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public List<Asociado> Pendientes { get; set; } = new();
        public List<Asociado> Aprobados { get; set; } = new();
        public List<Asociado> Desaprobados { get; set; } = new();
    }
}
