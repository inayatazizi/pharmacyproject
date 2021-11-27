using LabManagmentSystem.Data;
using LabManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly PathoDbContext _db;
        //private object _pathoDbContext;

        public PatientController(PathoDbContext db)
        {
            _db = db;
        }

       public IActionResult CreatePatient()
        {
            ViewBag.testName = new SelectList(_db.Tests.Select(s => new { Id = s.TestId, Name = s.TestName }).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePatient(Patient_Table obj)
        {
            if (obj != null)
            {
                try
                {
                    //var PatietnD = new Patient_Table();
                    //PatietnD.FirstName = obj.FirstName;
                    //PatietnD.MobileNo = obj.MobileNo;
                    //PatietnD.Sex = obj.Sex;
                    //PatietnD.Age = obj.Age;
                    //PatietnD.Email = obj.Email;
                    //PatietnD.AppointDate = obj.AppointDate;
                    //PatietnD.LastName = obj.LastName;
                    //PatietnD.Address = obj.Address;
                    _db.Patient_Tables.Add(obj);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return RedirectToAction("CreatePatient");
        }
        public async Task<IActionResult> PatientList(DataManagerRequest dm)
        {
            var fold = _db.Patient_Tables.AsNoTracking();            

            var list = fold.OrderByDescending(o => o.AppointDate).Skip(dm.Skip).Take(dm.Take).Select(s => new
            {                
                s.FirstName,
                s.MobileNo,
                s.Test,
                s.Sex,
                s.AppointDate,
                s.Email
            });
            if (dm.Search != null)
            {
                string keyword = dm.Search.Select(s => s.Key).FirstOrDefault();
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    fold = fold.Where(w => w.FirstName.ToLower().Contains(keyword) || w.LastName.ToLower().Contains(keyword));
                }
            }
            return Json(new { result = list, count = await fold.CountAsync() });
        }

                // Update Patient Details

        public IActionResult Edit(int? id)
        {
            var obj = _db.Patient_Tables.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Patient_Table obj)
        {
            if (ModelState.IsValid)
            {
                _db.Patient_Tables.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
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
            var obj = _db.Patient_Tables.Find(id);
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
        public IActionResult DeletePat(Patient_Table model)
        {
            var obj = _db.Patient_Tables.Find(model.Patient_Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Patient_Tables.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
