using Accountant_s_Assistant.App;
using Accountant_s_Assistant.Database;
using Accountant_s_Assistant.Database.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accountant_s_Assistant.Forms
{
    public partial class EmployerForm : Form
    {
        private bool toggleBtnInsertMode;
        private Employer employer;
        private List<Employer> list;
        public EmployerForm()
        {
            InitializeComponent();
        }

        private bool validation;

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

        private void addEmployer()
        {
            if (makeChecks())
            {
                Employer employer = new Employer();

                employer.Name = tbEmployerName.Text;
                employer.Address.Street = tbEmployerStreet.Text.ToString();
                employer.Address.City = tbEmployerCity.Text;
                employer.Address.PostalCode = tbEmployerPostal.Text;
                employer.VAT = tbEmployerVAT.Text;
                employer.Director = tbEmployerDirector.Text;

                DatabaseManager.insertIntoEmployers(employer);

                MessageBox.Show("Poslodavac unesen.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            tbEmployerName.Text = "";
            tbEmployerStreet.Text = "";
            tbEmployerCity.Text = "";
            tbEmployerPostal.Text = "";
            tbEmployerVAT.Text = "";
            tbEmployerDirector.Text = "";

            if (toggleBtnInsertMode)
            {
                btnInsert.Text = "Unesi";
                toggleBtnInsertMode = false;
            }
            else
            {
                btnInsert.Text = "Unesi";
            }
        }

        private void setTextBoxMaxLength()
        {
            tbEmployerName.MaxLength = 30;
            tbEmployerStreet.MaxLength = 40;
            tbEmployerCity.MaxLength = 15;
            tbEmployerPostal.MaxLength = 5;
            tbEmployerVAT.MaxLength = 11;
            tbEmployerDirector.MaxLength = 50;
        }

        private void EmployerForm_Load(object sender, EventArgs e)
        {
            init();
            setTextBoxMaxLength();
            clearFields();
            populateDataGridView();
        }

        private void init()
        {
            validation = true;
            employer = null;
            toggleBtnInsertMode = false;
            list = DatabaseManager.getAllEmployers();
        }

        private void populateDataGridView()
        {
            dgvEmployers.Columns.Add("Id", "Id");
            dgvEmployers.Columns.Add("Name", "Naziv");
            dgvEmployers.Columns.Add("Address", "Adresa");
            dgvEmployers.Columns.Add("VAT", "OIB");
            dgvEmployers.Columns.Add("Director", "Direktor");

            for (int i = 0; i <list.Count; i++)
            {
                dgvEmployers.Rows.Add(
                    list[i].Id,
                    list[i].Name,
                    list[i].Address.Street + ", " + list[i].Address.PostalCode + " " + list[i].Address.City,
                    list[i].VAT,
                    list[i].Director
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
                alterEmployer();
            }
            else
            {
                addEmployer();
            }
        }

        private void alterEmployer()
        {
            DatabaseManager.alterEmployer(employer);
            
        }

        private void tbEmployerVAT_TextChanged(object sender, EventArgs e)
        {
            long VAT = 12345678912;

            string VATtext = tbEmployerVAT.Text;
            if (!long.TryParse(VATtext, out VAT) && !VATtext.Equals(""))
            {
                MessageBox.Show("OIB može sadržavati isključivo 11 znamenki.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VATtext = VATtext.Substring(0, VATtext.Length - 1);
                tbEmployerVAT.Text = VATtext;
                tbEmployerVAT.SelectionStart = tbEmployerVAT.TextLength;
                return;
            }
        }

        private void tbEmployerPostal_TextChanged(object sender, EventArgs e)
        {
            long postal;

            string postalText = tbEmployerPostal.Text;
            if (!long.TryParse(postalText, out postal) && !postalText.Equals(""))
            {
                MessageBox.Show("Poštanski broj može sadržavati isključivo 5 znamenki.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                postalText = postalText.Substring(0, postalText.Length - 1);
                tbEmployerPostal.Text = postalText;
                tbEmployerPostal.SelectionStart = tbEmployerPostal.TextLength;
                return;
            }
        }

        private void dgvEmployers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            toggleBtnInsertMode = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvEmployers.Rows[e.RowIndex];
                employer = list.Find(x => x.Id.Equals(row.Cells["Id"].Value.ToString()));

                tbEmployerName.Text = employer.Name;
                tbEmployerStreet.Text = employer.Address.Street;
                tbEmployerCity.Text = employer.Address.City;
                tbEmployerPostal.Text = employer.Address.PostalCode;
                tbEmployerVAT.Text = employer.VAT;
                tbEmployerDirector.Text = employer.Director;
            }
            btnInsert.Text = "Promijeni\npodatke";
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }
}
