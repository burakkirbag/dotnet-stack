using FluentValidation;
using stack.Models.DTOs;

namespace stack.Validations
{
    public class LoginInputValidator : AbstractValidator<LoginInput>
    {
        public LoginInputValidator()
        {
            RuleFor(e => e.Email)
                .NotNull().WithMessage("E-Posta adresinizi girmelisiniz.")
                .NotEmpty().WithMessage("E-Posta adresinizi girmelisiniz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girmelisiniz.");

            RuleFor(e => e.Password)
                .NotNull().WithMessage("Şifrenizi girmelisiniz.")
                .NotEmpty().WithMessage("Şifrenizi girmelisiniz.")
                .MinimumLength(6).WithMessage("Şifreniz en az 6 karakter uzunluğunda olmalıdır.")
                .MaximumLength(20).WithMessage("Şifreniz en çok 20 karakter uzunluğunda olmalıdır.");
        }
    }
}
