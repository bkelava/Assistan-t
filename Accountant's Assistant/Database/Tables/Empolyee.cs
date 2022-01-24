using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant_s_Assistant.Database.Tables
{
    // EmpolyeeRoot myDeserializedClass = JsonConvert.DeserializeObject<EmployeeRoot>(myJsonResponse);

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string VAT { get; set; }
        public string Birthday { get; set; }
    }

    public class EmployeeRoot
    {
        public List<Employee> Employee { get; set; }
    }


}
