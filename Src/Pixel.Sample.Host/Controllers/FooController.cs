using System;
using System.Linq;
using System.Web.Mvc;
using Pixel.Sample.Business.Manager;
using Pixel.Sample.Core.Domain;

namespace Pixel.Sample.Host.Controllers
{
    public class FooController : Controller
    {
        public IFooManager FooManager { get; set; }
        public ActionResult Create()
        {
            //Sample Eager loading transaction out of scope
            var foo = new Foo() { Title = Guid.NewGuid().ToString(), Bar = new Bar() { Title = Guid.NewGuid().ToString() } };
            FooManager.Save(foo);
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            //Sample Eager loading transaction out of scope
            var liste = FooManager.GetAll();
            var first = liste.FirstOrDefault();
            if (first != null && first.Bar != null)
                ViewBag.Title = first.Bar.Title;
            return View();
        }
    }
}