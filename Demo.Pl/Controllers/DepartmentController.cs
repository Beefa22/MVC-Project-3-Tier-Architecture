using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    //inhiritance : DepartmentController is a Controller
    //Aggregation: DepartmentController has a DepartmentRepository
    [Authorize]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController( IUnitOfWork unitOfWork,IMapper mapper)//Ask CLR to create obj from class DepartmentRepo that impleminting Interface(IDepartmentRepository)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            var department =await _unitOfWork.DepartmentRepository.GetAll();

            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);

            return View(mappedDep); 
        }
       
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Create(DepartmentViewModel departmentViewModel)
        {
            //Server Side Validation
            if (ModelState.IsValid)
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentViewModel);

               await _unitOfWork.DepartmentRepository.Add(mappedDep);
                int count = await _unitOfWork.Complete();
                //TempData
                if (count > 0)
                    TempData["Message"] = "Department is Created successfully :)";


                return RedirectToAction(nameof(Index));
            }
            return View(departmentViewModel);
        }

        public async Task<IActionResult> Details(int? id,string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);

            

            if (department is null)
                return NotFound();
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(viewName, mappedDep);

        }

        //Department/Edit/1
        //Department/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            ///if (id is null)
            ///    return BadRequest();

            ///var department = _departmentRepository.GetById(id.Value);

            ///if (department is null)
            ///    return NotFound();

            ///return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, DepartmentViewModel departmentViewModel
            )
        {
            if (id != departmentViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentViewModel);

                    _unitOfWork.DepartmentRepository.Update(mappedDep);
                   await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    //1-Log Exception
                    //2-Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentViewModel);

        }

        //Department/Delete/1
        //Department/Delete
        //[HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
            ///if (id is null)
            ///    return BadRequest();

            ///var department = _departmentRepository.GetById(id.Value);

            ///if (department is null)
            ///    return NotFound();

            ///return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,DepartmentViewModel departmentViewModel)
        {
            if (id != departmentViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentViewModel);

                    _unitOfWork.DepartmentRepository.Delete(mappedDepartment);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //log exception
                    //friendly message

                    ModelState.AddModelError(string.Empty, ex.Message);
                    
                }
            }
            return View(departmentViewModel);


        }
    }
}
