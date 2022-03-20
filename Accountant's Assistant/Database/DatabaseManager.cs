using Accountant_s_Assistant.Database.Tables;
using Accountant_s_Assistant.Forms;
using Accountant_s_Assistant.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private string TableName(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        private static void appendToJsonFile(string filename, object list)
        {
            JArray array = JArray.FromObject(list);
            JObject jObject = new JObject();
            jObject["Employer"] = array;

            string json = jObject.ToString();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database/" + filename);
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(json);
            }
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

        public static void insertIntoEmployers(Employer employer)
        {
            List<Employer> items = getAllEmployers();
            
            int id = int.Parse(items[items.Count - 1].Id);
            id = id + 1;
            employer.Id = id.ToString();
            items.Add(employer);

            appendToJsonFile("employer.json", items);
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

        public static void alterEmployer(Employer employer)
        {
            List<Employer> list = getAllEmployers();
            var index = list.FindIndex(x => x.Id.Equals(employer.Id));
            list[index] = employer;

            //appendToJsonFile("Employers.json")
        }
    }
}
