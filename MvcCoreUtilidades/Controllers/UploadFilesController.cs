using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.Threading.Tasks;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        //COMO TENEMOS EL HELPER PROVIDER YA NO USAREMOS EL ENVIRONMENT
        //private IWebHostEnvironment environment;

        private HelperPathProvider helper;

        public UploadFilesController(HelperPathProvider helper)//IWebHostEnvironment environment
        {
            this.helper = helper;
            //this.environment = environment;
        }

        public IActionResult SubirFile()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            //VAMOS A SUBIR EL FICHERO A LOS ELEMENTOS TEMPORALES DEL EQUIPO
            //string tempFolder = Path.GetTempPath();
                //string rootpath = this.environment.WebRootPath;
            string fileName = fichero.FileName;

            string path =this.helper.MapPath(fileName,Folders.Images);
            //CUANDO PENSAMOS EN FICHEROS Y SUS RUTAS
            //ESTAMOS PENSANDO EN ALGO PARECIDO A ESTO:
            //C:\misficheros\CARPETA\1.TXT
            //NETCORE NO ES WINDOWS,ESTA RUTA ES DE WINDOWS
            //LAS RUTAS DE LINUX PUEDEN SER DISTINTAS Y MACOS
            //DEBEMOS CREAR UTAS CON HERRAMIENTAS DE NET CORE:Path
            //string path = Path.Combine(rootpath,"uploads", fileName);

            // 2. RUTA WEB: Para la vista (HTML la necesita)
            // Esto devuelve: https://localhost:7282/images/archivo.png
            string urlWeb = this.helper.MapUrlPath(fileName, Folders.Images);

            //Y LO SUBIMOS MEDIANTE STREAM
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["URL"] = urlWeb;
            ViewData["FILENAME"] = fileName;
                return View();
        }


    }
}
