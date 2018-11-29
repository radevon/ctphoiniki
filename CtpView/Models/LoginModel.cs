using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CtpView
{
    public class LoginModel
    {
        [Required(ErrorMessage="Введите логин пользователя")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль пользователя")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "запомнить меня")]
        public bool RememberMe { get; set; }
    }
}