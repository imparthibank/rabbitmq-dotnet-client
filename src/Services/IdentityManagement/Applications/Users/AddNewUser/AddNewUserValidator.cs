using FluentValidation;
using IdentityManagement.Infrastructure.Repositories;

namespace IdentityManagement.Applications.Users.AddNewUser
{
    public class AddNewUserValidator : AbstractValidator<AddNewUserCommand>
    {
        public AddNewUserValidator(IRepository repository)
        {
            RuleFor(command => command.FirstName).NotEmpty().Length(1, 50);
            RuleFor(command => command.LastName).NotEmpty().Length(1, 50);
            RuleFor(command => command.UserName).Length(1, 15);
            RuleFor(command => command.NickName);

            RuleFor(command => command.PersonalEmail).EmailAddress().WithMessage("Please enter valid email format").Length(1, 50);
            RuleFor(command => command.PersonalEmail)
                .MustAsync((command, PersonalEmail, _) => repository.PersonalEmailDuplicateCheck(command.PersonalEmail))
                .When(c => !string.IsNullOrEmpty(c.PersonalEmail))
                .WithMessage("The given personal email already exists");

            RuleFor(command => command.Gender).NotEmpty().IsInEnum();

            RuleFor(command => command.DateOfBirth).NotEmpty();
        }
    }
}
