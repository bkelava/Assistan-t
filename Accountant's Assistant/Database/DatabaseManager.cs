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

        private static void appendToJsonFile(string filename, string json)
        {
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

            JArray array = JArray.FromObject(items);
            JObject jObject = new JObject();
            jObject["Employer"] = array;

            string json = jObject.ToString();
            appendToJsonFile("employer.json", json);
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
