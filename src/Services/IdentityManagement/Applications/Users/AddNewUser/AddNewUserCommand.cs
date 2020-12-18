using IdentityManagement.Applications.Users.ViewModels;
using IdentityManagement.Models.Enums;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityManagement.Applications.Users.AddNewUser
{
    public class AddNewUserCommand : IRequest<UserResponseViewModel>
    {
        private DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string PersonalEmail { get; set; }
        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth
        {
            get { return BirthDate; }
            set { BirthDate = value.Date; }
        }
    }
}
