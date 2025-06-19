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
        public async Task<IActionResult> Registro(Asociado asociado)
        {

            try
            {
                await _asociadoService.RegistrarAsociado(asociado);
                return View("Confirmacion");
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




    }
}
