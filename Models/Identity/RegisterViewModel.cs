using System;
using System.ComponentModel.DataAnnotations;

namespace Econoterm.Models.Identity
{
    public class RegisterViewModel
    {
		[Required]
		[EmailAddress(ErrorMessage = "El correo no es valido")]
		public string Email
		{
			get;
			set;
		}

		[Required]
		[DataType(DataType.Password, ErrorMessage = "El password no es valido")]
		public string Password
		{
			get;
			set;
		}

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Los passwords no concuerdan, intente de nuevo")]
        [Display(Name = "Confirmar Password")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        public RegisterViewModel()
        {
        }
    }
}
