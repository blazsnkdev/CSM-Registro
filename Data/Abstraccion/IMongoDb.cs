using CSM_Registro.Models;

namespace CSM_Registro.Data.Abstraccion
{
    public interface IMongoDb
    {

        Task AddAsync(Asociado asociado);



    }
}
