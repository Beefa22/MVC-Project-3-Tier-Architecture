using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Demo.Pl.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName=R.Name,

                    
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);

                if (role is not null)
                {
                    var mappedRole = new RoleViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name
                    };
                    return View(new List<RoleViewModel>() { mappedRole });
                }
                return View(Enumerable.Empty<RoleViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(roleViewModel);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel); 
        
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Role = await _roleManager.FindByIdAsync(id);

            if (Role is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
            return View(viewName, mappedRole);

        }

        public async Task<IActionResult> Edit(string id)
        {
            //ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel updatedRole)
        {
            if (id != updatedRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);

                    role.Name = updatedRole.RoleName;
                  

                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    //1-Log Exception
                    //2-Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updatedRole);

        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //log exception
                //friendly message

                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction("Error", "Home");
            }




        }
    }
}
