using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeatBurguer.Models.ViewModels;

namespace NeatBurguer.Controllers
{
    public class HomeController : Controller
    {
        //Instancie el context para poder usarlo
        NeatBurguer.Models.neatContext context = new();
        [Route("/")]
        [Route("/Principal")]
        [Route("/Home")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Menu1/{opcion}")]
        public IActionResult Menu1(int opcion)
        {
            var count = context.Menus.Count();
            int anterior = 0;
            int siguiente = 0;

            if (opcion < 1 || opcion > count)
            {
                return RedirectToAction("Index");
            }

            var burger = context.Menus.Skip(opcion - 1).First();

            if (opcion < 2)
            {
                anterior = count;
                siguiente = opcion + 1;
                if (siguiente > count)
                {
                    siguiente = 1;
                }
            }
            else if (opcion == count)
            {
                anterior = opcion - 1;
                if (anterior < 1)
                {
                    anterior = count;
                }
                siguiente = 1;
            }
            else
            {
                anterior = opcion - 1;
                siguiente = opcion + 1;
            }
            Menu1ViewModel vm = new();

            vm.Nombre = burger.Nombre;
            vm.Id = burger.Id;
            vm.Precio = burger.Precio;
            vm.Descripción = burger.Descripción;
            vm.OpcionAnterior = anterior;
            vm.OpcionSiguiente = siguiente;


            return View(vm);
        }

        [Route("/Home/menu2")]

        //Use el context para tomar de clasificaciones las hamburguesas
        public IActionResult Menu2()
        {
            var burger = context.Clasificacions.Include(x => x.Menus);
            return View(burger);
        }

        [Route("menu3/{id}")]
        public IActionResult Menu3(int id)
        {
            Menu3ViewModel vm = new Menu3ViewModel();
            vm.Menus = context.Menus.OrderBy(x => x.Nombre);
            vm.Descripcion = context.Menus.Find(id).Descripción;
            vm.IdMenu = id;
            return View(vm);
        }
    }
}
