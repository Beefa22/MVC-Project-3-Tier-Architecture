using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
    public class RegisterViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        [Required(ErrorMessage ="Email is Required!!")]
        [EmailAddress(ErrorMessage ="Invaild Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required!!")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        
        [Required(ErrorMessage ="Confirm password is Required!!")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm Password doesn't match!!")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
