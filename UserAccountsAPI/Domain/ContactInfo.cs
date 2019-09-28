using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAccountsAPI.Domain
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string PhotoUrl { get; set; }
        public string FbUrl { get; set; }

        //ForiegnKey relation with Address table
        public int HomeAddressId { get; set; }
        public virtual Address HomeAddress { get; set; }

        //ForiegnKey relation with Address table
        public int BillingAddressId { get; set; }
        public virtual Address BillingAddress { get; set; }
    }
}
