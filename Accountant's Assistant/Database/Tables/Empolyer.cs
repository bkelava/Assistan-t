using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant_s_Assistant.Database.Tables
{
    // EmployerRoot myDeserializedClass = JsonConvert.DeserializeObject<EmployerRoot>(myJsonResponse);
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class Employer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string VAT { get; set; }
        public string Director { get; set; }
    }

    public class EmployerRoot
    {
        public List<Employer> Employer { get; set; }
    }
}
