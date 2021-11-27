using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PathologyLabManagment.Data;
using PathologyLabManagment.Models;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLabSystem.Controllers
{
    public class PathologyController : Controller
    {
        private readonly PathoContext _pathoContext;
        private string message = "";
        private int status = 0;

        public PathologyController(PathoContext pathoContext)
        {
            _pathoContext = pathoContext;
        }

       public IActionResult Index()
        {

            var patientList = new List<PatientSignUp>();
            
            patientList= _pathoContext.PatientSignUps.ToList();
            
            return View(patientList);
        }



        public IActionResult PatientCreateView()
        {
            return View();
        }

        public IActionResult PatientCreate(PatientSignUp patientSignUp)
        {

            
            try
            {
                _pathoContext.PatientSignUps.Add(patientSignUp);
                _pathoContext.SaveChanges();

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return Json(new { message = ex.ToString(), status = 0 });
            }
        }
        public async Task<IActionResult> PatientList([FromBody] DataManagerRequest dm)
        {
            var fold = _pathoContext.PatientSignUps.AsNoTracking();
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


        public IActionResult EditPatient(int id)
        {
            return View(_pathoContext.PatientSignUps.Find(id));
        }


        public IActionResult UpdatePatient(PatientSignUp patientSignUp)
        {
            
            var editPatient = _pathoContext.PatientSignUps.Find(patientSignUp.Patient_Id);
             //Donot assign id
            // delPatient.Address = patientSignUp.Address;
            try
            {
                if (editPatient!=null)
                {
                    editPatient.FirstName = patientSignUp.FirstName + patientSignUp.MiddleName + patientSignUp.LastName;
                    editPatient.Age = patientSignUp.Age;
                    editPatient.Email = patientSignUp.Email;
                    editPatient.Sex = patientSignUp.Sex;
                    editPatient.Test = patientSignUp.Test;
                    editPatient.DrName = patientSignUp.DrName;
                    editPatient.AppointDate = patientSignUp.AppointDate;
                    editPatient.MobileNo = patientSignUp.MobileNo;
                    editPatient.Address = patientSignUp.Address;

                    _pathoContext.Update(editPatient);
                    _pathoContext.SaveChanges();
                }

                return RedirectToAction("Index");
                //   return Json(new { message = "Success", status = 1, create = true }) ;

            }

            catch (Exception ex)
            {
                return Json(new { message = ex.ToString(), status = 0 });
            }



        }



       
        public IActionResult DeletePatient(int id)
         {
            var delPatient = _pathoContext.PatientSignUps.Find(id);
            try
            {
                if (delPatient != null)
                {
                    _pathoContext.PatientSignUps.Remove(delPatient);
                    _pathoContext.SaveChanges();
                }

                return Json(new { message="Success", status=1 });
            }

            catch(Exception ex)
            {
                return Json(new { message = ex.ToString(), status = 0 });
            }
          

            
        }

        // ViewBag.Employee = new SelectList(_pathoContext.PatientSignUps.Select(s => new { Id=s.Patient_Id, Name = s.FirstName }), "Id", "Name");



    }
}
