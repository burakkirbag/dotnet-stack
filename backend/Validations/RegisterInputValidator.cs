using FluentValidation;
using stack.Models.DTOs;

namespace stack.Validations
{
    public class RegisterInputValidator : AbstractValidator<RegisterInput>
    {
        public RegisterInputValidator()
        {
            RuleFor(e => e.Email)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girmelisiniz.");

            RuleFor(e => e.Password)
                .NotNull().WithMessage("Şifrenizi girmelisiniz.")
                .NotEmpty().WithMessage("Şifrenizi girmelisiniz.")
                .MinimumLength(6).WithMessage("Şifreniz en az 6 karakter uzunluğunda olmalıdır.")
                .MaximumLength(20).WithMessage("Şifreniz en çok 20 karakter uzunluğunda olmalıdır.");

            RuleFor(e => e.Firstname)
                .NotNull().WithMessage("Adınızı girmelisiniz.")
                .NotEmpty().WithMessage("Adınızı girmelisiniz.")
                .MinimumLength(3).WithMessage("Adınız en az 3 karakter uzunluğunda olmalıdır.")
                .MaximumLength(20).WithMessage("Adınız en çok 20 karakter uzunluğunda olmalıdır.");

            RuleFor(e => e.Lastname)
                .NotNull().WithMessage("Soyadınızı girmelisiniz.")
                .NotEmpty().WithMessage("Soyadınızı girmelisiniz.")
                .MinimumLength(3).WithMessage("Soyadınız en az 3 karakter uzunluğunda olmalıdır.")
                .MaximumLength(20).WithMessage("Soyadınız en çok 20 karakter uzunluğunda olmalıdır.");
        }
    }
}
