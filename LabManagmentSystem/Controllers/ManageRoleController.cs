using LabManagmentSystem.Data;
using LabManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Controllers
{
    public class ManageRoleController : Controller

    {
        private readonly ILogger<ManageRoleController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PathoDbContext _context;
        

        public ManageRoleController(ILogger<ManageRoleController> logger,
            RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, PathoDbContext context )
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;            
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        //get
        public async Task<IActionResult> Edit(string Id)
        {
            ManageUserRoles UserRoles = new ManageUserRoles();
            var user = _context.Users.Where(x => x.Id == Id).SingleOrDefault();
            var userInRole = _context.UserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).ToList();
            UserRoles.roles = await _roleManager.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userInRole.Contains(x.Id)
            }).ToListAsync();
            UserRoles.AppUser = user;
            
           
            return View(UserRoles);
        }
        //post
        [HttpPost]
        public IActionResult Edit(ManageUserRoles mr)
        {
            var selectedRoleId = mr.roles.Where(x => x.Selected).Select(x => x.Value);
            var alreadyExistRoleId = _context.UserRoles.Where(x => x.UserId == mr.AppUser.Id).Select(x => x.RoleId).ToList();
            var toAdd = selectedRoleId.Except(alreadyExistRoleId);
            var toRemove = alreadyExistRoleId.Except(selectedRoleId);
            foreach (var item in toRemove)
            {                
            _context.UserRoles.Remove(new IdentityUserRole<string>
            {
                RoleId=item,
                UserId=mr.AppUser.Id
            });
            }
            foreach (var item in toAdd)
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = mr.AppUser.Id
                });
            }
            _context.SaveChanges();
            return View("Index");
        }

    }
}
