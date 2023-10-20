using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required!!")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm password is Required!!")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Confirm Password doesn't match!!")]
		public string ConfirmPassword { get; set; }
	}
}
