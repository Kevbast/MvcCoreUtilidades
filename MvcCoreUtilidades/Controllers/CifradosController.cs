using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {

       

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CifradoEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoEficiente(string contenido,string resultado,string accion)
        {

            if (accion.ToLower() == "cifrar")
            {
                string response = HelperCyptography.EncriptartextoConSalt(contenido, false);
                ViewData["TEXTOCIFRADO"] = response;
                ViewData["SALT"] = HelperCyptography.Salt;
            }
            else if (accion.ToLower() == "comparar")
            {
                string response = HelperCyptography.EncriptartextoConSalt(contenido, true);
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "los datos no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"] = "los datos son iguales!!";
                }


            }
            return View();
        }





        public IActionResult CifradoBasico()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoBasico(string contenido,string resultado,string accion)
        {
            //CIFRAMOS EL CONTENIDO
            string response = HelperCyptography.EncriptarTextoBasico(contenido);

            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }else if (accion.ToLower() == "comparar")
            {
                //SI EL USUARIO QUIERE COMPARAR,NOS ESTARÁ ENVIANDO 
                //EL TEXTO PARA COMPARAR EL RESULTADO
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "los datos no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"] = "los datos son iguales!!";
                }

            }

                return View();
        }



    }
}
