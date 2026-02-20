namespace MvcCoreUtilidades.Helpers
{
    //Enumeración con las carpetas que deseemos subir ficheros
    public enum Folders { Uploads,Images,Facturas, Productos, Temporal}
    public class HelperPathProvider
    {//LO CREAMOS DE MANERA EFICIENTE PARA NO CARGAR EL CÓDIGO Y HACERLO MÁS ORGANIZADO
        private IWebHostEnvironment environment;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HelperPathProvider(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            this.environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }
        //tendremos un metodo que se encargará de devolver una ruta como string cuando recibamos el fichero y la carpeta
        public string MapPath(string fileName,Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }

            string rootPath = this.environment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);

            return path;

        }

        public string MapUrlPath(string fileName,Folders folder)
        {
            string carpeta = "";

            // 1. Recuperamos la URL del servidor dinámicamente
            // Esto detecta si es http o https, el dominio (o localhost) y el puerto
            var request = _httpContextAccessor.HttpContext.Request;
            string serverUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Productos)
            {//ESTA SI VA A CAMBIAR PORQUE ESTO ES UN SISTEMA DE FICHEROS
                //NECESITAMOS WEB
                carpeta = "images/productos";
            }
            //QUIERO BUSCAR LA FORMA DE RECUPERAR la url de nuestro server en mvc net core
            //string server = "http://localhost:7999";

            // 3. Retornamos la URL completa combinada
            return $"{serverUrl}/{carpeta}/{fileName}";

        }



    }
}
