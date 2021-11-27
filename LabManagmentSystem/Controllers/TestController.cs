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
    public class TestController : Controller
    {
        private readonly PathoDbContext _db;
        public TestController(PathoDbContext db)
        {
            _db = db;

        }
        //public IActionResult TestDetail()
        //{
        //    IEnumerable<Test> objlist = _db.Tests;
        //    return View(objlist);
        //}
        public IActionResult AddTest()
        {
            return View();
        }
        //Add test
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddTest(Test obj)
        {
            var exsitTest = _db.Tests.Any(w => w.TestName == obj.TestName & w.TestId != obj.TestId);
            if(exsitTest)
            {
                return Json(new { status = false, message = "Test already exist" });
            }
            try {
                var list = _db.Tests.Find(obj.TestId);
                if (list == null)
                {
                    list = new Test();
                    list.TestCode = obj.TestCode;
                    list.TestName = obj.TestName;
                    list.TestDescription = obj.TestDescription;
                    list.TestPrice = obj.TestPrice;
                     _db.Tests.Add(list);
                }
                await _db.SaveChangesAsync();
                return Json(new { status = true, message = "Test Created successfully." });
            }          
           catch (Exception ex)
            {
                return Json(new { status = false, message = "Failed to create Test.", ex = ex.ToString() });
            }
        }
        public async Task<IActionResult> TestList ([FromBody] DataManagerRequest dm)
        {
            var list = _db.Tests.AsNoTracking().Select(s => new
            {
                s.TestId,
                s.TestName,
                s.TestCode,
                s.TestDescription,
                s.TestPrice
            }).AsNoTracking();
            if (dm.Search != null)
            {
                string keyword = dm.Search.Select(s => s.Key).FirstOrDefault();
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    list = list.Where(w => w.TestName.ToLower().Contains(keyword) || w.TestCode.ToLower().Contains(keyword));
                }
            }

            return Json(new { result = list, count = await list.CountAsync()});
        }
        // Edit Test Deteails


        public IActionResult Edit(int? id)
        {
            var obj = _db.Tests.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        // Update Patient Details
        [HttpPost]
        public IActionResult Update(Test obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tests.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("TestDetail");
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
            var obj = _db.Tests.Find(id);
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
        public IActionResult DeletePat(Test model)
        {
            var obj = _db.Tests.Find(model.TestId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Tests.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("TestDetail");

        }
    }
}
