using System.ComponentModel.DataAnnotations;

namespace CarDealerCore.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Ваш логин")]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} и не более {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} и не более {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        public ChangePasswordViewModel()
        {
        }

        public ChangePasswordViewModel(string id, string login)
        {
            Id = id;
            Login = login;
        }
    }
}