using BasicRepository.Data;
using BasicRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BasicRepository.Controllers
{
    public class PersonelController : Controller
    {
        private readonly Repository<Personel> _rep;
        //newleme işlemi
        public PersonelController(Repository<Personel> rep)
        {
            _rep = rep;
        }
        //başka yerde kullanmayalım diye

        public IActionResult List()
        {

            return View(_rep.Liste());
        }
        public IActionResult Create()
        {
            Personel personel = new Personel();
            return View("Crud", personel);
        }
        [HttpPost]
        public IActionResult Create(Personel p)
        {
            _rep.Create(p);
            _rep.Save();
            return RedirectToAction("List");

        }
        public IActionResult Edit(int Id)
        {
            Personel p = _rep.Find(Id);
            return View("Crud", p);
        }
        [HttpPost]
        public IActionResult Edit(Personel p)
        {
            _rep.Update(p);
            string msg = "";
            try
            {
                msg = _rep.Save();
                ViewBag.Message = msg;
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Crud", p);
            }


         

        }
        public IActionResult Delete(int Id)
        {
            Personel p = _rep.Find(Id);
            return View("Crud", p);
        }
        [HttpPost]
        public IActionResult Delete(Personel p)
        {
            _rep.Delete(p);
            _rep.Save();
            return RedirectToAction("List");

        }

    }
}
