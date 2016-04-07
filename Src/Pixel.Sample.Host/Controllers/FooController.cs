using System.Web.Mvc;
using Pixel.Sample.Business.Manager;

namespace Pixel.Sample.Host.Controllers
{
    public class FooController : Controller
    {
        public IFooManager FooManager { get; set; }
        public ActionResult Index()
        {
            var  liste = FooManager.GetAll();
            return View();
        }
    }
}