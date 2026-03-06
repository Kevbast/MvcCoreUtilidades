using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.ViewComponents
{
    //heredamos
    public class MenuCochesViewComponent:ViewComponent
    {
        //INYECTAMOS EL REPO
        private RepositoryCoches repo;

        public MenuCochesViewComponent(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        //PODEMOS TENER TODOS LOS METODOS QUE DESEEMOS
        //PERO SU QUREMOS DEVOLVER DATOS A LA VISTA Y AL LAYOUT
        //NECESITAMOS EL METODO InvokeAsync()
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Coche> cars = this.repo.GetCoches();
            return View(cars);
        }
    }
}
