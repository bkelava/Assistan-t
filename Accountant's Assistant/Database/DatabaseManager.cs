using Accountant_s_Assistant.Database.Tables;
using Accountant_s_Assistant.Forms;
using Accountant_s_Assistant.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Accountant_s_Assistant.Database
{
    class DatabaseManager
    {
        private DatabaseManager()
        {
            //empty
        }

        private static string readJsonFromFile(string path)
        {

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }

        public static List<Employee> getAllEmployees()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database/employee.json");
            string json = readJsonFromFile(path);

            List<Employee> items;

            if (json.Equals(""))
            {
                items = new List<Employee>();
            }
            else
            {
                EmployeeRoot myDeserializedClass = JsonConvert.DeserializeObject<EmployeeRoot>(json);
                items = myDeserializedClass.Employee;
            }

            return items;
        }

        public static List<Employer> getAllEmployers()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database/employer.json");
            string json = readJsonFromFile(path);

            List<Employer> items;

            if (json.Equals(""))
            {
                items = new List<Employer>();
            }
            else
            {
                EmployerRoot myDeserializedClass = JsonConvert.DeserializeObject<EmployerRoot>(json);
                items = myDeserializedClass.Employer;
            }
            return items;
        }

        public static List<string> getEstates()
        {
            List<string> list = new List<string>();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/EstatesInCroatia.txt");
            foreach(string line in File.ReadLines(path))
            {
                list.Add(line);
            }
            return list;
        }
    }
}
