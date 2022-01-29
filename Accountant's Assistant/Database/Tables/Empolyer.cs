using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant_s_Assistant.Database.Tables
{
    // EmployerRoot myDeserializedClass = JsonConvert.DeserializeObject<EmployerRoot>(myJsonResponse);
    public class Address
    {
        public Address()
        {
            init();
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        private void init()
        {
            Street = "";
            City = "";
            PostalCode = "";
        }
    }

    public class Employer
    {

        public Employer()
        {
            init();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string VAT { get; set; }
        public string Director { get; set; }

        private void init()
        {
            Id = "";
            Name = "";
            Address = new Address();
            VAT = "";
            Director = "";
        }
    }

    public class EmployerRoot
    {
        public List<Employer> Employer { get; set; }
    }
}
