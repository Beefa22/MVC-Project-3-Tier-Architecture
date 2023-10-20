using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.Pl.Helper;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(/*IEmployeeRepository employeeRepository,*/
            /*IDepartmentRepository departmentRepository*/  IUnitOfWork unitOfWork,
            IMapper mapper)//Ask CLR to create obj from class DepartmentRepo that impleminting Interface(IDepartmentRepository)
        {
           // _employeeRepository = employeeRepository;
           //_departmentRepository = departmentRepository;
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                 employees =await _unitOfWork.EmployeeRepository.GetAll();

               
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.SearchEmployeeByName(SearchValue);
               
            }
            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);

        }

        //[HttpGet]
        public async Task<IActionResult> Create()
        {
           ViewBag.Departments=await _unitOfWork.DepartmentRepository.GetAll();


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
           
            //Server Side Validation
            if (ModelState.IsValid)
            { //Manual Mapping
                ///var employee = new Employee()
                ///{
                ///    Name = employeeViewModel.Name,
                ///    Address = employeeViewModel.Address,
                ///    Age = employeeViewModel.Age,
                ///    //And so on ....
                ///};
                ///

                employeeViewModel.ImageName = DocumentSettings.UploadFile(employeeViewModel.Image, "Images");

                //Using AutoMapper
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                
              await  _unitOfWork.EmployeeRepository.Add(MappedEmp);

              int Count = await _unitOfWork.Complete();

                //TempData
                if (Count >0)
                    TempData["Message"] = "The employee is created successfully :)"; 
               
                
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Employee =await _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (Employee is null)
                return NotFound();

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(viewName, mappedEmp);

        }

        //Employee/Edit/1
        //Employee/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments =await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");
            ///if (id is null)
            ///    return BadRequest();

            ///var Employee = _departmentRepository.GetById(id.Value);

            ///if (Employee is null)
            ///    return NotFound();

            ///return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    employeeViewModel.ImageName = DocumentSettings.UploadFile(employeeViewModel.Image, "Images");
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
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
            return View(employeeViewModel);

        }

        //Employee/Delete/1
        //Employee/Delete
        //[HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
            ///if (id is null)
            ///    return BadRequest();

            ///var Employee = _departmentRepository.GetById(id.Value);

            ///if (Employee is null)
            ///    return NotFound();

            ///return View(Employee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);

                    _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                  var count=await  _unitOfWork.Complete();
                    if (count > 0)
                        DocumentSettings.DeleteFile(mappedEmp.ImageName, "Images");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //log exception
                    //friendly message

                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(employeeViewModel);


        }
    }
}
