using Microsoft.AspNetCore.Mvc;
using PathoLabSystem.Models;
using PathologyLabManagment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLabSystem.Controllers
{
    public class ProffessionalController : Controller
    {
        private readonly PathoContext _db;
        public ProffessionalController(PathoContext db)
        {
            _db = db;
        }
        public IActionResult ProfDetails ()
        {
            IEnumerable<Proffessional> objlist = _db.Proffessionals;

            return View(objlist);
        }
        public IActionResult ProffRegister()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProffRegister( Proffessional obj)
        {
            _db.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("ProffRegister");
        }

    }
    
}
