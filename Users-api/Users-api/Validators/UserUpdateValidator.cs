using FluentValidation;
using Users_api.Dto;

namespace Users_api.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("El campo Name no puede estar vacío");
            RuleFor(user => user.Email).NotEmpty().WithMessage("El campo Email no puede estar vacío");
            RuleFor(user => user.Email).EmailAddress().WithMessage("El campo Email tiene que ser un email válido");
        }
    }
}
