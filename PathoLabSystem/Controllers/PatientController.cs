using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathologyLabManagment.Data;
using PathologyLabManagment.Models;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathologyLabManagment.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly PathoContext _db;
        private object _pathoContext;

        public PatientController(PathoContext db)
        {
            _db = db;
        }

        public IActionResult PatientSignUpDeatails()
        {
            IEnumerable<PatientSignUp> objlist = _db.PatientSignUps;

            return View(objlist);
        }
        public IActionResult CreateSignUpPatient()
        {
            return View();
        }
        public async Task<IActionResult> PatientList([FromBody] DataManagerRequest dm)
        {
            var fold = _db.PatientSignUps.AsNoTracking();
            if (dm.Search != null)
            {
                string keyword = dm.Search.Select(s => s.Key).FirstOrDefault();
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    fold = fold.Where(w => w.FirstName.ToLower().Contains(keyword) || w.LastName.ToLower().Contains(keyword));
                }
            }

            var list = fold.OrderByDescending(o => o.AppointDate).Skip(dm.Skip).Take(dm.Take).Select(s => new
            {
                s.Patient_Id,
                s.FirstName,
                s.MobileNo,
                s.Test,
                s.Sex,
                s.AppointDate,
                s.Email
            });
            return Json(new { result = list, count = await fold.CountAsync() });
        }

        [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult CreateSignUpPatient(PatientSignUp obj)
        {
            _db.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("PatientSignUpDeatails");
        }
        // Update Patient Details
        
        public IActionResult Edit(int? id)
        {
            var obj = _db.PatientSignUps.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(PatientSignUp obj)
        {
            if (ModelState.IsValid)
            {
                _db.PatientSignUps.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("PatientSignUpDeatails");
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
            var obj = _db.PatientSignUps.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult DeletePat()
        {
            return View();
        }
        //post delete
        [HttpPost]
        public IActionResult DeletePat(PatientSignUp model)
        {
            var obj = _db.PatientSignUps.Find(model.Patient_Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.PatientSignUps.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("PatientSignUpDeatails");

        }
    }
}
