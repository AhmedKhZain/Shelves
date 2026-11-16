using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Domain
{
    public class Client : BaseEntity
    {
        private Client(string name, string email, string phoneNumber, string nationalID) : base()
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            NationalID = nationalID;
        }

        public string NationalID { get; set; } 
        public string Name { get; set; } 
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
