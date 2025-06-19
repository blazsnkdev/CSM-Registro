using CSM_Registro.Data.Abstraccion;
using CSM_Registro.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CSM_Registro.Data.Repositories
{
    public class MongoDbRepositorio : IMongoDb
    {
        private readonly IMongoCollection<Asociado> _asociadoCollection;
        public MongoDbRepositorio(IOptions<MongoDbSettings> options)
        {
            var cliente = new MongoClient(options.Value.ConnectionString);
            var database = cliente.GetDatabase(options.Value.DatabaseName);
            _asociadoCollection = database.GetCollection<Asociado>("Asociado");
        }

        public async Task AddAsync(Asociado asociado)
        {
            await _asociadoCollection.InsertOneAsync(asociado);
        }




    }
}
