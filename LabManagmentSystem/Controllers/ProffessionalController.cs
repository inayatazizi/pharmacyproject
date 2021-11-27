using LabManagmentSystem.Data;
using LabManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Controllers
{
    public class ProffessionalController : Controller
    {
        private readonly PathoDbContext _db;
        public ProffessionalController(PathoDbContext db)
        {
            _db = db;
        }
        //public IActionResult ProfDetails()
        //{
        //    IEnumerable<Proffessional> objlist = _db.Proffessionals;

        //    return View(objlist);
        //}
        public IActionResult ProffRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProffRegister(Proffessional obj)
        {
            _db.Add(obj);
            _db.SaveChanges();
            return View();
        }
        public async Task<IActionResult> PassengerList([FromBody] DataManagerRequest dm)
        {
            var passenger = (from p in _db.Proffessionals
                             select new
                             {
                                 p.ProfId,
                                 p.Name,
                                 p.Email,
                                 p.MobileNo,
                                 p.Qualification,
                                 p.University,
                                 p.Experince,
                                 p.Address,
                             }).AsNoTracking();

            if (dm.Search != null)
            {
                string keyword = dm.Search.Select(s => s.Key).FirstOrDefault();
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    passenger = passenger.Where(w => w.Name.ToLower().Contains(keyword) || w.Email.ToLower().Contains(keyword) || w.ProfId.ToString().Contains(keyword));
                }
            }

            var _passenger = passenger.OrderByDescending(P => P.ProfId).Skip(dm.Skip).Take(dm.Take);

            return Json(new { result = _passenger, count = await _passenger.CountAsync() });

        }

        //edit proffisonals
        public IActionResult Edit(int? id)
        {
            var obj = _db.Proffessionals.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Proffessional obj)
        {
            if (ModelState.IsValid)
            {
                _db.Proffessionals.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("ProfDetails");
            }
            return View(obj);
        }
        /// Delete Paitent Details
         //get delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Proffessionals.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult DeleteProff()
        {
            return View();
        }
        //post delete
        [HttpPost]
        public IActionResult DeleteProff(Proffessional model)
        {
            var obj = _db.Proffessionals.Find(model.ProfId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Proffessionals.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("PatientDetails");

        }

    }

}
