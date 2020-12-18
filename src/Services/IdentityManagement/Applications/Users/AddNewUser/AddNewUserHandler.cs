using IdentityManagement.Applications.Users.ViewModels;
using IdentityManagement.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityManagement.Applications.Users.AddNewUser
{
    public class AddNewUserHandler : IRequestHandler<AddNewUserCommand, UserResponseViewModel>
    {
        private readonly IRepository repository;
        private readonly ILogger<AddNewUserHandler> logger;
        public AddNewUserHandler(IRepository repository, ILogger<AddNewUserHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;

        }
        public async Task<UserResponseViewModel> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            var currentDate = DateTime.UtcNow;
            var newGuid = Guid.NewGuid();
            var newUser = new Models.Users()
            {
                Id = newGuid,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                NickName = request.NickName,
                PersonalEmail = request.PersonalEmail,
                Gender = (int)request.Gender,
                DateOfBirth = request.DateOfBirth,
                IsActive = true,
                CreatedDate = currentDate
            };
            await repository.AddAsync(newUser);
            logger.LogInformation("Executed command: {UserName}", request.UserName);
            return new UserResponseViewModel() { UserId = newUser.Id, UserName = newUser.UserName };
        }
    }
}
