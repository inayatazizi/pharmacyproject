using Microsoft.AspNetCore.Mvc;
using PathoLabSystem.Models;
using PathologyLabManagment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLabSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly PathoContext _db;
        public TestController (PathoContext db)
        {
            _db = db;

        }
        public IActionResult TestDetail()
        {
            IEnumerable<Test> objlist = _db.Tests;
            return View(objlist);
        }
        public IActionResult AddTest()
        {
            return View();

        }
        //Add test
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTest(Test obj)
        {
            _db.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("AddTest");
        }
        
        // Edit Test Deteails

    }
}
