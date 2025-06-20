using CSM_Registro.Data.Abstraccion;
using CSM_Registro.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

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

        public async Task DeleteAsync(string id)
        {
            await _asociadoCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<List<Asociado>> GetAllAsync()
        {
            return await _asociadoCollection.Find(_ => true).ToListAsync();
        }

        public Task<List<Asociado>> GetAllByEstadoAsync(string estado)
        {
            var filtro = Builders<Asociado>.Filter.Eq(a => a.Estado, estado);
            return _asociadoCollection.Find(filtro).ToListAsync();
        }

        public Task<List<Asociado>> GetByEstadoYFechasAsync(string estado, DateTime desde, DateTime hasta)
        {
            var filtro = Builders<Asociado>.Filter.And(
                Builders<Asociado>.Filter.Eq(a => a.Estado, estado),
                Builders<Asociado>.Filter.Gte(a => a.FechaRegistro, desde),
                Builders<Asociado>.Filter.Lte(a => a.FechaRegistro, hasta)
            );

            return _asociadoCollection.Find(filtro).ToListAsync();
        }


        public async Task<List<Asociado>> GetByFechaRegistroAsync(DateTime desde, DateTime hasta)
        {
            var filtro = Builders<Asociado>.Filter.Gte(a => a.FechaRegistro, desde) &
                         Builders<Asociado>.Filter.Lte(a => a.FechaRegistro, hasta);

            return await _asociadoCollection.Find(filtro).ToListAsync();
        }

        public async Task<Asociado> GetByIdAsync(string id)
        {
            return await _asociadoCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateEstadoAprobadoAsync(string id)
        {
            var filtro = Builders<Asociado>.Filter.Eq(a => a.Id, id);
            var actualizacion = Builders<Asociado>.Update.Set(a => a.Estado, "Aprobado")
                                                         .Set(a => a.FechaAprobado, DateTime.UtcNow);
            await _asociadoCollection.UpdateOneAsync(filtro, actualizacion);


        }

        public async Task UpdateEstadoDesaprobadoAsync(string id)
        {
            var filtro = Builders<Asociado>.Filter.Eq(a => a.Id, id);
            var actualizacion = Builders<Asociado>.Update.Set(a => a.Estado, "Desaprobado")
                                                         .Set(a => a.FechaAprobado, DateTime.UtcNow);
            await _asociadoCollection.UpdateOneAsync(filtro, actualizacion);
        }
    }
}
