using LabManagmentSystem.Models;
using LabManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        //get 
        public IActionResult CreateRole()
        {
            return View();
        }
        //post
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleStore role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
            if(!roleExist)
            {
               await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return RedirectToAction("RoleIndex");
        }
        //get
        public IActionResult RoleIndex()
        {
            var roles = _roleManager.Roles.ToList();
            List<RoleStore> vm = new List<RoleStore>();
            foreach( var item in roles)
            {
                vm.Add(new RoleStore { Id=item.Id, RoleName = item.Name });
            }
            return View(vm);
        }
        // edit role get
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await _roleManager.Roles.Where(x=>x.Id==Id).FirstOrDefaultAsync();
            RoleStore vm = new RoleStore();
            vm.Id = role.Id;
            vm.RoleName = role.Name;
            return View(vm);
        }
        //post method
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleStore rs)
         {
            var role = await _roleManager.Roles.Where(y => y.Id == rs.Id).FirstOrDefaultAsync();
            role.Name = rs.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("RoleIndex");
            }
            return View();
        }

        // Delete role get
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.Roles.Where(x => x.Id == Id).FirstOrDefaultAsync();
            RoleStore vm = new RoleStore();
            vm.Id = role.Id;
            vm.RoleName = role.Name;
            return View(vm);
        }
        //post method
        [HttpPost]
        public async Task<IActionResult> DeleteRole(RoleStore rs)
        {
            var role = await _roleManager.Roles.Where(y => y.Id == rs.Id).FirstOrDefaultAsync();
            role.Name = rs.RoleName;
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("RoleIndex");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
