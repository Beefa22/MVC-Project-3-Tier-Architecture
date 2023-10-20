using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is Required!!")]
		[EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }
	}
}
