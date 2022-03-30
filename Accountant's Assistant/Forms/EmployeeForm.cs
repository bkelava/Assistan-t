using Accountant_s_Assistant.App;
using Accountant_s_Assistant.Database;
using Accountant_s_Assistant.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Accountant_s_Assistant.Forms
{
    public partial class EmployeeForm : Form
    {
        private bool toggleBtnInsertMode;
        private bool validation;
        private Employee employee;
        private List<Employee> list;
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (App.EventHandler.sentFromForm1)
            {
                App.EventHandler.sentFromForm1 = false;
                ApplicationManager.closeForm(this);
            }
            else
            {
                ApplicationManager.switchForm(this, new ContractOnDefiniteTime(), true);
            }
        }

        private void addEmployee()
        {
            if (makeChecks())
            {
                Employee employee = new Employee();

                employee.Name = tbEmployeeName.Text;
                employee.Address.Street = tbEmployeeStreet.Text.ToString();
                employee.Address.City = tbEmployeeCity.Text;
                employee.Address.PostalCode = tbEmployeePostal.Text;
                employee.VAT = tbEmployeeVAT.Text;
                employee.Birthday = tbEmployeeBirthDay.Text;

                DatabaseManager.insertIntoEmployees(employee);

                MessageBox.Show("Radnik unesen.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearFields();
            }
            else
            {
                MessageBox.Show("Greška!\nMolim da ispunite sva potrebna polja!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool makeChecks()
        {
            validation = true;
            foreach (TextBox tb in panel2.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    validation = false;
                }
            }

            return validation;
        }

        private void clearFields()
        {
            tbEmployeeName.Text = "";
            tbEmployeeStreet.Text = "";
            tbEmployeeCity.Text = "";
            tbEmployeePostal.Text = "";
            tbEmployeeVAT.Text = "";
            tbEmployeeBirthDay.Text = "";

            if (toggleBtnInsertMode)
            {
                btnInsert.Text = "Unesi";
                toggleBtnInsertMode = false;
            }
            else
            {
                btnInsert.Text = "Unesi";
            }
            btnRemoveEmployee.Enabled = false;
        }

        private void setTextBoxMaxLength()
        {
            tbEmployeeName.MaxLength = 30;
            tbEmployeeStreet.MaxLength = 40;
            tbEmployeeCity.MaxLength = 15;
            tbEmployeePostal.MaxLength = 5;
            tbEmployeeVAT.MaxLength = 11;
            tbEmployeeBirthDay.MaxLength = 50;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            btnRemoveEmployee.Enabled = false;
            init();
            setTextBoxMaxLength();
            clearFields();
            populateDataGridView();
        }

        private void init()
        {
            validation = true;
            employee = null;
            toggleBtnInsertMode = false;

            list = DatabaseManager.getAllEmployees();
        }

        private void populateDataGridView()
        {
            dgvEmployees.Columns.Add("Id", "Id");
            dgvEmployees.Columns.Add("Name", "Naziv");
            dgvEmployees.Columns.Add("Address", "Adresa");
            dgvEmployees.Columns.Add("VAT", "OIB");
            dgvEmployees.Columns.Add("Director", "Direktor");

            for (int i = 0; i <list.Count; i++)
            {
                dgvEmployees.Rows.Add(
                    list[i].Id,
                    list[i].Name,
                    list[i].Address.Street + ", " + list[i].Address.PostalCode + " " + list[i].Address.City,
                    list[i].VAT,
                    list[i].Birthday
                    );
            }
            /*foreach (DataGridViewRow row in dgvEmployers.Rows)
            {
                employer = list.Find(x => x.Id.Equals(row.Cells["Id"].Value.ToString()));
                dgvEmployers[columnName, index].Value = "2";//(employer.Address.Street + ", " + employer.Address.PostalCode + " " + employer.Address.City).ToString();
            }*/
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (toggleBtnInsertMode)
            {
                alterEmployee();
            }
            else
            {
                addEmployee();
                dgvEmployees.Update();
                dgvEmployees.Refresh();
            }
        }

        private void alterEmployee()
        {
            DatabaseManager.alterEmployee(employee);
            
        }

        private void tbEmployerVAT_TextChanged(object sender, EventArgs e)
        {
            long VAT = 12345678912;

            string VATtext = tbEmployeeVAT.Text;
            if (!long.TryParse(VATtext, out VAT) && !VATtext.Equals(""))
            {
                MessageBox.Show("OIB može sadržavati isključivo 11 znamenki.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VATtext = VATtext.Substring(0, VATtext.Length - 1);
                tbEmployeeVAT.Text = VATtext;
                tbEmployeeVAT.SelectionStart = tbEmployeeVAT.TextLength;
                return;
            }
        }

        private void tbEmployerPostal_TextChanged(object sender, EventArgs e)
        {
            long postal;

            string postalText = tbEmployeePostal.Text;
            if (!long.TryParse(postalText, out postal) && !postalText.Equals(""))
            {
                MessageBox.Show("Poštanski broj može sadržavati isključivo 5 znamenki.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                postalText = postalText.Substring(0, postalText.Length - 1);
                tbEmployeePostal.Text = postalText;
                tbEmployeePostal.SelectionStart = tbEmployeePostal.TextLength;
                return;
            }
        }

        private void dgvEmployers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            toggleBtnInsertMode = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvEmployees.Rows[e.RowIndex];
                employee = list.Find(x => x.Id.Equals(row.Cells["Id"].Value.ToString()));

                tbEmployeeName.Text = employee.Name;
                tbEmployeeStreet.Text = employee.Address.Street;
                tbEmployeeCity.Text = employee.Address.City;
                tbEmployeePostal.Text = employee.Address.PostalCode;
                tbEmployeeVAT.Text = employee.VAT;
                tbEmployeeBirthDay.Text = employee.Birthday;
            }
            btnInsert.Text = "Promijeni\npodatke";
            btnRemoveEmployee.Enabled = true;
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void btnRemoveEmployer_Click(object sender, EventArgs e)
        {
            
        }
    }
}
