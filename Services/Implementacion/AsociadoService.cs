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

        public async Task<List<Asociado>> ListarPorRangoFechas(DateTime inicio, DateTime fin)
        {
            return await _mongoDb.GetByFechaRegistroAsync(inicio, fin);
        }


        public async Task<List<Asociado>> ListarPorEstadoYFechas(string estado, DateTime desde, DateTime hasta)
        {
            return await _mongoDb.GetByEstadoYFechasAsync(estado, desde, hasta);
        }




        public async Task<List<Asociado>> ListarPendientesPaginado(DateTime desde, DateTime hasta, int pagina = 1)
        {
            int tamañoPagina = 5;

            var lista = await _mongoDb.GetByEstadoYFechasAsync("Pendiente", desde, hasta);

            var paginados = lista
                .OrderByDescending(a => a.FechaRegistro)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToList();

            return paginados;
        }


        public async Task<int> ContarPendientes(DateTime desde, DateTime hasta)
        {
            var lista = await _mongoDb.GetByEstadoYFechasAsync("Pendiente", desde, hasta);
            return (int)Math.Ceiling((double)lista.Count / 5); // 5 por página
        }




        public Task<List<Asociado>> ObtenerAsociados()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegistrarAsociado(Asociado asociado, IFormFile fotoVoucher)
        {
            if (fotoVoucher == null || fotoVoucher.Length == 0)
            {
                return false; 
            }

            using (var ms = new MemoryStream())
            {
                await fotoVoucher.CopyToAsync(ms);
                var bytes = ms.ToArray();
                string base64 = Convert.ToBase64String(bytes);
                string contentType = fotoVoucher.ContentType;

                asociado.FotoVoucher = $"data:{contentType};base64,{base64}";
            }

            asociado.FechaRegistro = DateTime.UtcNow;
            asociado.Estado = "Pendiente";
            asociado.FechaAprobado = null;

            await _mongoDb.AddAsync(asociado);
            return true;
        }




    }
}
