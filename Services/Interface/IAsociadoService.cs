using CSM_Registro.Models;

namespace CSM_Registro.Services.Interface
{
    public interface IAsociadoService
    {
        Task<bool> RegistrarAsociado (Asociado asociado);
        
    }
}
