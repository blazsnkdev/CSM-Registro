using CSM_Registro.Models;

namespace CSM_Registro.Services.Interface
{
    public interface IAsociadoService
    {
        Task<bool> RegistrarAsociado (Asociado asociado, IFormFile fotoVoucher);
        Task<List<Asociado>> ObtenerAsociados();


        Task<List<Asociado>> ListarPorRangoFechas(DateTime inicio, DateTime fin);


        Task<List<Asociado>> ListarPorEstadoYFechas(string estado, DateTime desde, DateTime hasta);



        Task<List<Asociado>> ListarPendientesPaginado(DateTime desde, DateTime hasta, int pagina = 1);
        Task<int> ContarPendientes(DateTime desde, DateTime hasta);
    }
}
