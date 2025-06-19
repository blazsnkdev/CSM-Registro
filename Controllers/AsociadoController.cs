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
        public async Task<IActionResult> Registro(Asociado asociado, IFormFile fotoVoucher)
        {

            try
            {
                await _asociadoService.RegistrarAsociado(asociado, fotoVoucher);
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
        public IActionResult Pendientes()
        {
            return View(new List<Asociado>());
        }

        [HttpPost]
        public async Task<IActionResult> Pendientes(DateTime desde, DateTime hasta, int pagina = 1)
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
        public IActionResult Aprobados()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Aprobados(DateTime desde, DateTime hasta)
        {
            var aprobados = await _asociadoService.ListarPorEstadoYFechas("Aprobado", desde, hasta);
            return View("ListaAprobados", aprobados);
        }

        [HttpGet]
        public IActionResult Desaprobados()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Desaprobados(DateTime desde, DateTime hasta)
        {
            var desaprobados = await _asociadoService.ListarPorEstadoYFechas("Desaprobado", desde, hasta);
            return View("ListaDesaprobados", desaprobados);
        }




    }
}
