using IdentityManagement.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityManagement.Models
{
    public class Users : BaseEntity
    {
        private DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth
        {
            get { return BirthDate; }
            set { BirthDate = value.Date; }
        }
        public string PersonalEmail { get; set; }
    }
}
