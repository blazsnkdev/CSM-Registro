using CSM_Registro.Models;

namespace CSM_Registro.Services.Interface
{
    public interface IAsociadoService
    {
        Task<bool> RegistrarAsociado (Asociado asociado, IFormFile fotoVoucher);
        Task<List<Asociado>> ObtenerAsociados();

        Task<List<Asociado>> ListarPendientesPaginado(DateTime? desde, DateTime? hasta, int pagina = 1);
        Task<List<Asociado>> ListarAprobadosPaginado(DateTime? desde, DateTime? hasta, int pagina = 1);
        Task<List<Asociado>> ListarDesaprobadosPaginado(DateTime? desde, DateTime? hasta, int pagina = 1);
        Task<int> ContarPendientes(DateTime? desde, DateTime? hasta);
        Task<int> ContarAprobados(DateTime desde, DateTime hasta);
        Task<int> ContarDesaprobados(DateTime desde, DateTime hasta);

        Task<Asociado?> ObtenerAsociadoPorId(string id);

        Task AprobarAsociado(string id);
        Task DesaprobarAsociado(string id);

    }
}
