using CSM_Registro.Models;
using CSM_Registro.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CSM_Registro.Controllers
{
    public class AsociadoController : Controller
    {
        private readonly IAsociadoService _asociadoService;
        public AsociadoController(IAsociadoService asociadoService)
        {
            _asociadoService = asociadoService;
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(Asociado asociado, IFormFile fotoVoucher, IFormFile fotoAsociado)
        {

            try
            {
                await _asociadoService.RegistrarAsociado(asociado, fotoVoucher,fotoAsociado);
                TempData["Asociado"] = $"Asociado {asociado.NombreAsociado} Registrado";
                return RedirectToAction("Confirmacion");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el asociado: " + ex.Message);
                return View(asociado);

            }
            
        }


        public IActionResult Confirmacion()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Pendientes(int pagina = 1)
        {
            var asociados = await _asociadoService.ListarPendientesPaginado(null, null, pagina);
            int totalPaginas = await _asociadoService.ContarPendientes(null, null);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Desde = null;
            ViewBag.Hasta = null;

            return View(asociados);
        }

        [HttpPost]
        public async Task<IActionResult> Pendientes(DateTime? desde, DateTime? hasta, int pagina = 1)
        {
            var asociados = await _asociadoService.ListarPendientesPaginado(desde, hasta, pagina);
            int totalPaginas = await _asociadoService.ContarPendientes(desde, hasta);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Desde = desde;
            ViewBag.Hasta = hasta;

            return View(asociados);
        }



        [HttpGet]
        public async Task<IActionResult> Aprobados(int pagina = 1)
        {
            var asociados = await _asociadoService.ListarAprobadosPaginado(null, null, pagina);
            int totalPaginas = await _asociadoService.ContarAprobados(DateTime.MinValue, DateTime.MaxValue);
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Desde = null;
            ViewBag.Hasta = null;
            return View(asociados);
        }

        [HttpPost]
        public async Task<IActionResult> Aprobados(DateTime? desde, DateTime? hasta, int pagina = 1)
        {
            var asociados = await _asociadoService.ListarAprobadosPaginado(desde, hasta, pagina);
            int totalPaginas = await _asociadoService.ContarPendientes(desde, hasta);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Desde = desde;
            ViewBag.Hasta = hasta;

            return View(asociados);
        }

        [HttpGet]
        public async Task<IActionResult> Desaprobados(int pagina = 1)
        {
            var asociados = await _asociadoService.ListarDesaprobadosPaginado(null, null, pagina);
            int totalPaginas = await _asociadoService.ContarAprobados(DateTime.MinValue, DateTime.MaxValue);
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Desde = null;
            ViewBag.Hasta = null;
            return View(asociados);
        }

        [HttpPost]
        public async Task<IActionResult> Desaprobados(DateTime? desde, DateTime? hasta, int pagina = 1)
        {
            var asociados = await _asociadoService.ListarDesaprobadosPaginado(desde, hasta, pagina);
            int totalPaginas = await _asociadoService.ContarPendientes(desde, hasta);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Desde = desde;
            ViewBag.Hasta = hasta;

            return View(asociados);
        }



        public async Task<IActionResult> Aprobar(string id)
        {
            try
            {
                await _asociadoService.AprobarAsociado(id);
                TempData["Exito"] = "Asociado aprobado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al aprobar el asociado: " + ex.Message;
            }
            return RedirectToAction("Pendientes");
        }



        public async Task<IActionResult> Desaprobar(string id)
        {
            try
            {
                await _asociadoService.DesaprobarAsociado(id);
                TempData["Exito"] = "Asociado desaprobad ";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al desaprobar el asociado: " + ex.Message;
            }
            return RedirectToAction("Pendientes");
        }

        
        public async Task<IActionResult> Detalle(string id)
        {
            var asociado = await _asociadoService.ObtenerAsociadoPorId(id);
            if (asociado == null)
            {
                return NotFound();
            }
            return View(asociado);

        }

    }
}
