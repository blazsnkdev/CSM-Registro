using CSM_Registro.Models;

namespace CSM_Registro.Data.Abstraccion
{
    public interface IMongoDb
    {

        Task AddAsync(Asociado asociado);
        Task<List<Asociado>> GetAllAsync();
        Task<List<Asociado>> GetByFechaRegistroAsync(DateTime desde, DateTime hasta);

        Task<List<Asociado>> GetByEstadoYFechasAsync(string estado, DateTime desde, DateTime hasta);



    }
}
