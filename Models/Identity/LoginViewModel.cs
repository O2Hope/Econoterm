﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Econoterm.Models.Identity
{
    public class LoginViewModel
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

        public LoginViewModel()
        {
        }
    }
}
