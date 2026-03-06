using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        private RepositoryCoches repo;

        public CochesController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        //ESTA SRÁ LA VISTA PRINCIPAL

        public IActionResult Index()
        {
            return View();
        }
        //TENDREMOS UN IACTION RESULT PARTIAL
        //PARA INTEGRAR DENTRO DE INDEX

        public IActionResult _CochesPartial()
        {
            //DEBEMOS DEVOLVER EL DIBUJO QUE DESEEMOS EN AJAX
            //INDICAMOS EL NOMBRE DEL FICHERO DE ESE CSHTML Y SU MODEL
            
            return PartialView("_CochesPartial",this.repo.GetCoches);

        }

        //nUEVA PARTIALvIEW
        public IActionResult _CochesDetails(int idcoche)
        {
            Coche car = this.repo.findCoche(idcoche);

            return PartialView("_CochesDetailsView", car);

        }

        //CREAMOS DETAILS PARA EL VIEWCOMPONENT
        public IActionResult Details(int idcoche)
        {
            Coche car = this.repo.findCoche(idcoche);

            return View(car);

        }




    }
}
