using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Demo.Pl.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is required!!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required!!")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "Date of creation")]
        public DateTime DateOfCreation { get; set; }

        //Navigational Property [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
