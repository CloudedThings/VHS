using System;
using System.Collections.Generic;
using System.Text;

namespace VHS.Entity
{
    public class Owner
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
        public int OwnerStatus { get; set; }
    }
}
