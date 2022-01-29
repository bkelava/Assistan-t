using System;
using System.IO;
using System.Windows.Forms;

namespace Accountant_s_Assistant.App
{
    class ApplicationManager
    {
        private static bool exist = true;
        private static string path = "";
        private static string dir = "";


        public static void closeForm(Form form)
        {
            form.Close();
        }
        public static void switchForm(bool close, Form oldForm, Form newForm)
        {
            if (close)
            {
                oldForm.Enabled = false;
                oldForm.Close();

                newForm.Enabled = true;
            }
            else
            {
                oldForm.Enabled = false;
                oldForm.Hide();

                newForm.Enabled = true;
            }
        }

        public static void switchForm(Form oldForm, Form newForm, bool hide)
        {
            if (hide)
            {
                oldForm.Enabled = false;
                oldForm.Hide();

                newForm.Enabled = true;
                newForm.Show();
            }
            else
            {
                oldForm.Enabled = false;

                newForm.Enabled = true;
                newForm.Show();
            }
        }

        public static void putFromIntoPanel(Form form, Panel panel)
        {
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(form);
            form.Show();
        }

        public static void maximizeWindow(Form form)
        {
            if (form.WindowState == FormWindowState.Normal)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Normal;
            }
        }

        public static void minimizeWindow(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Minimized;
        }

        public static void exitApplication()
        {
            if (MessageBox.Show("Molim potvrdite za izlazak iz aplikacije.", "Izlaz iz knjigovodstvenog  pomoćnika", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
            
        }

        private static void setUpJson(string path, string filename)
        {
            string filePath = Path.Combine(path, filename);
            exist = File.Exists(filePath);
            if (!exist) //if doesn't exist, create one
            {
                File.Create(filePath).Close();
            }
        }

        public static void setUpEnviroment()
        {
            dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(dir, "Database/");
            exist = Directory.Exists(path);
            
            if (!exist) //database folder do not exist, create one
            {
                Directory.CreateDirectory(path);
                setUpJson(path, "employer.json"); //create employer folder
                setUpJson(path, "employee.json"); //create employee folder
            }
            else
            {
                setUpJson(path, "employer.json"); //create employer folder
                setUpJson(path, "employee.json"); //create employee folder
            }


        }
    }
}
