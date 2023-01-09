﻿using System.ComponentModel.DataAnnotations;

namespace YZL3161BlogProject.ViewModels.Auth.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username can not be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
