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


        public async Task<List<Asociado>> ListarPendientesPaginado(DateTime? desde, DateTime? hasta, int pagina = 1)
        {
            int tamañoPagina = 5;
            var lista = await _mongoDb.GetAllByEstadoAsync("Pendiente");

            
            if (desde.HasValue && hasta.HasValue)
            {
                lista = lista.Where(a => a.FechaRegistro.Date >= desde.Value.Date && a.FechaRegistro.Date <= hasta.Value.Date).ToList();
            }

            var paginados = lista
                .OrderByDescending(a => a.FechaRegistro)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToList();

            return paginados;
        }



        public async Task<List<Asociado>> ListarAprobadosPaginado(DateTime? desde, DateTime? hasta, int pagina = 1)
        {
            int tamañoPagina = 5;
            var lista = await _mongoDb.GetAllByEstadoAsync("Aprobado");


            if (desde.HasValue && hasta.HasValue)
            {
                lista = lista.Where(a => a.FechaRegistro.Date >= desde.Value.Date && a.FechaRegistro.Date <= hasta.Value.Date).ToList();
            }

            var paginados = lista
                .OrderByDescending(a => a.FechaRegistro)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToList();

            return paginados;
        }

        public async Task<List<Asociado>> ListarDesaprobadosPaginado(DateTime? desde, DateTime? hasta, int pagina = 1)
        {
            int tamañoPagina = 5;
            var lista = await _mongoDb.GetAllByEstadoAsync("Desaprobado");


            if (desde.HasValue && hasta.HasValue)
            {
                lista = lista.Where(a => a.FechaRegistro.Date >= desde.Value.Date && a.FechaRegistro.Date <= hasta.Value.Date).ToList();
            }

            var paginados = lista
                .OrderByDescending(a => a.FechaRegistro)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToList();

            return paginados;
        }


        public async Task<int> ContarPendientes(DateTime? desde, DateTime? hasta)
        {
            var lista = await _mongoDb.GetAllByEstadoAsync("Pendiente");

            if (desde.HasValue && hasta.HasValue)
            {
                lista = lista.Where(a => a.FechaRegistro.Date >= desde.Value.Date && a.FechaRegistro.Date <= hasta.Value.Date).ToList();
            }

            return (int)Math.Ceiling((double)lista.Count / 5);
        }

        public async Task<int> ContarAprobados(DateTime desde, DateTime hasta)
        {
            var lista = await _mongoDb.GetByEstadoYFechasAsync("Aprobado", desde, hasta);
            return (int)Math.Ceiling((double)lista.Count / 5);
        }
        public async Task<int> ContarDesaprobados(DateTime desde, DateTime hasta)
        {
            var lista = await _mongoDb.GetByEstadoYFechasAsync("Desaprobado", desde, hasta);
            return (int)Math.Ceiling((double)lista.Count / 5);
        }

        public Task<List<Asociado>> ObtenerAsociados()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegistrarAsociado(Asociado asociado, IFormFile fotoVoucher,IFormFile fotoAsociado)
        {
            if (fotoVoucher == null || fotoVoucher.Length == 0)
            {
                return false; 
            }
            if (fotoAsociado == null || fotoAsociado.Length == 0)
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
            using (var ms = new MemoryStream())
            {
                await fotoAsociado.CopyToAsync(ms);
                var bytes = ms.ToArray();
                string base64 = Convert.ToBase64String(bytes);
                string contentType = fotoAsociado.ContentType;
                asociado.FotoAsociado = $"data:{contentType};base64,{base64}";
            }
            asociado.FechaRegistro = DateTime.UtcNow;
            asociado.Estado = "Pendiente";
            asociado.FechaAprobado = null;
            
            


            await _mongoDb.AddAsync(asociado);
            return true;
        }

        public async Task<Asociado?> ObtenerAsociadoPorId(string id)
        {
            return await _mongoDb.GetByIdAsync(id);
        }

        public async Task AprobarAsociado(string id)
        {
            var seleccionado = _mongoDb.GetByIdAsync(id);
            await _mongoDb.UpdateEstadoAprobadoAsync(id);
        }

        public Task DesaprobarAsociado(string id)
        {
            throw new NotImplementedException();
        }
    }
}
