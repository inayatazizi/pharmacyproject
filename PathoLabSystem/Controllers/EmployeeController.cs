using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathoLabSystem.Models;
using PathologyLabManagment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLabSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly PathoContext _db;
        public EmployeeController(PathoContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult EmployeeDetails()
        {
            IEnumerable<Empolyee> objlist = _db.Empolyees;
            return View(objlist);



        }
        //insert values
        public IActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(Empolyee obj)
        {
            _db.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("EmployeeDetails");

        }
        // update Employee deatails
        public IActionResult Edit(int? id)
        {
            var obj = _db.Empolyees.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Empolyee obj)
        {
            if(ModelState.IsValid)
            {
                _db.Empolyees.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("EmployeeDetails");
            }
            return View(obj);

        }








































    }
}
