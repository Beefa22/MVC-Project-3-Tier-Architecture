using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.Pl.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 character!")]
        [MinLength(5, ErrorMessage = "Min Length is 5 character!")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{3,30}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}$"
            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
       
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
        public Department Department { get; set; }


        public int? DepartmentId { get; set; }
    }
}
