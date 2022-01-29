using Accountant_s_Assistant.App;
using Accountant_s_Assistant.Database;
using Accountant_s_Assistant.Database.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accountant_s_Assistant.Forms
{
    public partial class ContractOnDefiniteTime : Form
    {
        private bool validation;
        List<Employer> employers;
        List<Employee> employees;
        private void setUpUI()
        {
           employers = DatabaseManager.getAllEmployers();
            if (!employers.Any())
            {
                MessageBox.Show("Lista poslodavaca je prazna.\nMolim da dodate poslodavce u listu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnValidate.Enabled = false;
            }
            else
            {
                foreach (Employer employer in employers)
                {
                    cbEmployer.Items.Add(employer.Name);
                    cbEmployer.ValueMember = employer.Id.ToString();
                }
            }

            employees = DatabaseManager.getAllEmployees();
            if (!employees.Any())
            {
                MessageBox.Show("Lista zaposlenika je prazna.\nMolim da dodate zaposlenike u listu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnValidate.Enabled = false;
            }
            else
            {
                foreach (Employee employee in employees)
                { 
                    cbEmployee.Items.Add(employee.Name);
                    cbEmployee.ValueMember = employee.Id.ToString();
                }
            }

            List<string> estates = DatabaseManager.getEstates();
            foreach (string estate in estates)
            {
                cbEmployeeWorkplace.Items.Add(estate);
            }

            btnPrint.Enabled = false;
            btnSaveToPDF.Enabled = false;

            tbJobDescription.Text = "-";
            tbStartOfDeploymentDescription.Text = "-";
            tbVacationDescription.Text = "-";
            tbRightsAndObligations.Text = "-";
            cbCompetentCourt.Text = "-";
        }

        private List<KeyValuePair<string, string>> getContractFormData()
        {
            string contractDate = dtpContractDate.Value.ToShortDateString();
            string endofEmployment = dtpEndOfEmployment.Value.ToShortDateString();
            string jobDescription = tbJobDescription.Text.ToString();
            string trialWorkDuration = cbTrialWorkDuration.Text;
            string employeeWorkPlace = cbEmployeeWorkplace.Text;
            string startOfEmployment = dtpStartOfEmployment.Value.ToShortDateString();
            string startOfEmploymentDescription = tbStartOfDeploymentDescription.Text.ToString();
            string sallary = numSallary.Value.ToString();
            string stimulation = numStimulation.Value.ToString();
            string sallaryFitA = nummSalaryFitA.Value.ToString();
            string sallaryFitB = nummSalaryFitB.Value.ToString();
            string sallaryFitC = nummSalaryFitC.Value.ToString();
            string sallaryFitD = nummSalaryFitD.Value.ToString();
            string sallaryFitE = nummSalaryFitE.Value.ToString();
            string sallaryFitF = nummSalaryFitF.Value.ToString();
            string workTimeHalfOrFull = cbWorkTimeHalfOrFull.Text;
            string workHoursPerWeek = numWorkHoursPerWeek.Value.ToString();
            string workTime = cbWorkTime.Text;
            string workTimeStartA = dtpWorkTimeStartA.Value.ToShortTimeString();
            string workTimeStartB = dtpWorkTimeStartB.Value.ToShortTimeString();
            string workTimeEndA = dtpWorkTimeEndA.Value.ToShortTimeString();
            string workTimeEndB = dtpWorkTimeEndB.Value.ToShortTimeString();
            string weeklyTimeOff = cbWeeklyTimeOff.Text;
            string vacation = cbVacation.Text;
            string vacationDescription = tbVacationDescription.Text.ToString();
            string contractCancelation = cbContractCancelation.Text;
            string noticePeriodA = numNoticePeriodA.Value.ToString();
            string noticePeriodB = numNoticePeriodB.Value.ToString();
            string rightsAndObligations = tbRightsAndObligations.Text.ToString();
            string competentCourt = cbCompetentCourt.Text;
            string contractEntry = dtpContractEntry.Value.ToShortDateString();

            var list = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("contractDate", contractDate),
                new KeyValuePair<string, string>("endOfEmployment", endofEmployment),
                new KeyValuePair<string, string>("jobDescription", jobDescription),
                new KeyValuePair<string, string>("trialWorkDuration", trialWorkDuration),
                new KeyValuePair<string, string>("employeeWorkPlace", employeeWorkPlace),
                new KeyValuePair<string, string>("startOfEmployment", startOfEmployment),
                new KeyValuePair<string, string>("startOfEmploymentDescription", startOfEmploymentDescription),
                new KeyValuePair<string, string>("sallary", sallary),
                new KeyValuePair<string, string>("stimulation", stimulation),
                new KeyValuePair<string, string>("sallaryFitA", sallaryFitA),
                new KeyValuePair<string, string>("sallaryFitB", sallaryFitB),
                new KeyValuePair<string, string>("sallaryFitC", sallaryFitC),
                new KeyValuePair<string, string>("sallaryFitD", sallaryFitD),
                new KeyValuePair<string, string>("sallaryFitE", sallaryFitE),
                new KeyValuePair<string, string>("sallaryFitF", sallaryFitF),
                new KeyValuePair<string, string>("workTimeHalfOrFull", workTimeHalfOrFull),
                new KeyValuePair<string, string>("workHoursPerWeek", workHoursPerWeek),
                new KeyValuePair<string, string>("workTime", workTime),
                new KeyValuePair<string, string>("workTimeStartA", workTimeStartA),
                new KeyValuePair<string, string>("workTimeEndB", workTimeEndB),
                new KeyValuePair<string, string>("weeklyTimeOff", weeklyTimeOff),
                new KeyValuePair<string, string>("vacation", vacation),
                new KeyValuePair<string, string>("vacationDescription", vacationDescription),
                new KeyValuePair<string, string>("contractCancelation", contractCancelation),
                new KeyValuePair<string, string>("noticePeriodA", noticePeriodA),
                new KeyValuePair<string, string>("noticePeriodB", noticePeriodB),
                new KeyValuePair<string, string>("rightsAndObligations", rightsAndObligations),
                new KeyValuePair<string, string>("competentCourt", competentCourt),
                new KeyValuePair<string, string>("contractEntry", contractEntry)
            };

            if (dtpWorkTimeEndB.Enabled)
            {

                list.Add(new KeyValuePair<string, string>("workTimeStartB", workTimeStartB));
                list.Add(new KeyValuePair<string, string>("workTimeEndA", workTimeEndA));
            }
            return list;
        }

        private Employer getContractFormEmployer()
        {
            string selectedValue = cbEmployer.Text;
            Employer employer = employers.Find(x => x.Name == selectedValue);
            return employer;
        }

        private Employee getContractFormEmployee()
        {
            string selectedValue = cbEmployee.Text;
            Employee employee = employees.Find(x => x.Name == selectedValue);
            return employee;
        }

        public ContractOnDefiniteTime()
        {
            InitializeComponent();
        }

        void AddOnChangeHandlerToInputControls(Control ctrl)
        {
            foreach (Control subctrl in ctrl.Controls)
            {
                if (subctrl is TextBox)
                    subctrl.TextChanged +=
                        new System.EventHandler(InputControls_OnChange);
                else if (subctrl is CheckBox)
                    ((CheckBox)subctrl).CheckedChanged +=
                        new System.EventHandler(InputControls_OnChange);
                else if (subctrl is RadioButton)
                    ((RadioButton)subctrl).CheckedChanged +=
                        new System.EventHandler(InputControls_OnChange);
                else if (subctrl is ListBox)
                    ((ListBox)subctrl).SelectedIndexChanged +=
                        new System.EventHandler(InputControls_OnChange);
                else if (subctrl is ComboBox)
                    ((ComboBox)subctrl).SelectedIndexChanged +=
                        new System.EventHandler(InputControls_OnChange);
                else
                {
                    if (subctrl.Controls.Count > 0)
                        this.AddOnChangeHandlerToInputControls(subctrl);
                }
            }
        }

        private void InputControls_OnChange(object sender, EventArgs e)
        {
            btnPrint.Enabled = false;
            btnSaveToPDF.Enabled = false;
        }

        private void ContractOnDefiniteTime_Load(object sender, EventArgs e)
        {
            setUpUI();
            validation = false;
            AddOnChangeHandlerToInputControls(this);
        }

        private void cbEmployer_MouseHover(object sender, EventArgs e)
        {
            definitiveContractFormHelper.Show("Naziv i sjedište poslodavca", cbEmployer);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            validation = false;
            foreach (Panel pnl in Controls.OfType<Panel>())
            {
                foreach (ComboBox cb in pnl.Controls.OfType<ComboBox>())
                {
                    if (string.IsNullOrEmpty(cb.Text) || cb.Text.Equals("Odaberite"))
                    {
                        validation = true;
                    }
                }
                foreach (TextBox tb in pnl.Controls.OfType<TextBox>())
                {
                    if (string.IsNullOrEmpty(tb.Text))
                    {
                        validation = true;
                    }
                }
                foreach(NumericUpDown n in pnl.Controls.OfType<NumericUpDown>())
                {
                    if (n.Value.Equals(0) && n != numStimulation)
                    {
                        validation = true;
                    }
                }
            }
            if (validation)
            {
                MessageBox.Show("Niste ispravno ispunili sva polja!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Polja su ispravno popunjena!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnPrint.Enabled = true;
                btnSaveToPDF.Enabled = true;
            }
        }

        private void btnValidate_MouseHover(object sender, EventArgs e)
        {
            definitiveContractFormHelper.Show("Provjera jesu li\nsva polja ispravno unešena", btnValidate);
        }

        private void btnCloseContractForm_Click(object sender, EventArgs e)
        {
            ApplicationManager.switchForm(this, new Form1(), true);
        }

        private void cbEmployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employer employer = getContractFormEmployer();
            tbEmployerHq.Text = employer.Address.Street + ", " + employer.Address.City;
            tbEmployerVAT.Text = employer.VAT;
            tbEmployerCEO.Text = employer.Director;
        }

        private void btnSaveToPDF_Click(object sender, EventArgs e)
        {
            Employer employer = getContractFormEmployer();
            Employee employee = getContractFormEmployee();
            List<KeyValuePair<string, string>> list = getContractFormData();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            PDFCreator pdfCreator = new PDFCreator(path);

            string pathToContract = pdfCreator.generateContractOnDefinitiveTime(list, employer, employee);
            MessageBox.Show("Ugovor je spremljen u mapi Moji Dokumenti na Vašem računalu. \nPutanja: " + pathToContract, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbWorkTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cbWorkTime.Text;

            if (selectedValue.Equals("klizno") || selectedValue.Equals("dvokratno"))
            {
                dtpWorkTimeStartB.Enabled = true;
                dtpWorkTimeEndA.Enabled = true;
            }
            else
            {
                dtpWorkTimeStartB.Enabled = false;
                dtpWorkTimeEndA.Enabled = false;
            }
        }

        private void cbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee employee = getContractFormEmployee();
            tbEmployeeVAT.Text = employee.VAT;
        }

        private void cbWorkTimeHalfOrFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cbWorkTimeHalfOrFull.Text;
            if (selectedValue.Equals("puno"))
            {
                numWorkHoursPerWeek.Minimum = 40;
                numWorkHoursPerWeek.Value = 40;
            }
            else
            {
                numWorkHoursPerWeek.Minimum = 1;
                numWorkHoursPerWeek.Value = 1;
            }
        }

        private void btnOpenEmployerForm_Click(object sender, EventArgs e)
        {
            ApplicationManager.switchForm(this, new EmployerForm(), true);
        }
    }
}
