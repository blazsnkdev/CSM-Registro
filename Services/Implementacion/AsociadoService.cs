using CSM_Registro.Data.Abstraccion;
using CSM_Registro.Models;
using CSM_Registro.Services.Interface;

namespace CSM_Registro.Services.Implementacion
{
    public class AsociadoService : IAsociadoService
    {

        private readonly IMongoDb _mongoDb;
        public AsociadoService(IMongoDb mongoDb)
        {
            _mongoDb = mongoDb;
        }
        public async Task<bool> RegistrarAsociado(Asociado asociado)
        {
            asociado.FechaRegistro = DateTime.Now;
            asociado.Estado = "Pendiente";
            asociado.FechaAprobado = null;
            if (string.IsNullOrEmpty(asociado.FotoVoucher))
            {
                return false;
            }
            await _mongoDb.AddAsync(asociado);
            return true;
        }




    }
}
