using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment environment;

        public UploadFilesController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult SubirFile()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            //VAMOS A SUBIR EL FICHERO A LOS ELEMENTOS TEMPORALES DEL EQUIPO
            string tempFolder = Path.GetTempPath();
            string rootpath = this.environment.WebRootPath;
            string fileName = fichero.FileName;
            //CUANDO PENSAMOS EN FICHEROS Y SUS RUTAS
            //ESTAMOS PENSANDO EN ALGO PARECIDO A ESTO:
            //C:\misficheros\CARPETA\1.TXT
            //NETCORE NO ES WINDOWS,ESTA RUTA ES DE WINDOWS
            //LAS RUTAS DE LINUX PUEDEN SER DISTINTAS Y MACOS
            //DEBEMOS CREAR UTAS CON HERRAMIENTAS DE NET CORE:Path
            string path = Path.Combine(rootpath,"images", fileName);
            //Y LO SUBIMOS MEDIANTE STREAM
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
                return View();
        }


    }
}
