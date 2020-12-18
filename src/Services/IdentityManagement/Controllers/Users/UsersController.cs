using IdentityManagement.Applications.Users.AddNewUser;
using IdentityManagement.Applications.Users.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IdentityManagement.Controllers.Users
{
    public class UsersController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<UsersController> logger;
        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost]
        public async Task<ActionResult<UserResponseViewModel>> AddNewUserAsync([FromBody] AddNewUserCommand command)
        {
            logger.LogInformation("Executed command: {UserName}", command.UserName);
            return await mediator.Send(command);
        }
    }
}
