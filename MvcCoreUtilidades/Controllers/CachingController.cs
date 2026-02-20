using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        //PARA EL CACHÉ PERSONALIZADO
        private IMemoryCache memorycache;

        public CachingController(IMemoryCache memorycache)
        {
            this.memorycache = memorycache;
        }
        //nos creamos un étodo
        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            if(tiempo==null)
            {
                tiempo = 60;
            }

            string fecha = DateTime.Now.ToLongDateString() + "--" + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            //COMO ESTO ES MANUAL DEBEMOS PREGUNTAR SI EXISTE ALGO EN CACHÉ O NO
            if (this.memorycache.Get("FECHA") == null)
            {
                //SI NO EXISTE,pues lo guardamos 
                //CREAMOS EL OBJETO ENTRYOPTIONS CON EL TIEMPO
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                //y le pasamos las options
                this.memorycache.Set("FECHA", fecha,options);
                ViewData["MENSAJE"] = "FECHA ALMACENADA CORRECTAMENTE";
                ViewData["FECHA"] = this.memorycache.Get("FECHA");
            }
            else
            {
                //EXISTE CACHE Y LO RECUPERAMOS
                fecha = this.memorycache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "FECHA RECUPERADA CORRECTAMENTE";
                ViewData["FECHA"] = fecha;
            }
                return View();
        }





        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString()+"--"+ DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}
