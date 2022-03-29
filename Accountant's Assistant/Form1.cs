using Accountant_s_Assistant.App;
using Accountant_s_Assistant.Database;
using Accountant_s_Assistant.Database.Tables;
using Accountant_s_Assistant.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accountant_s_Assistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplicationManager.setUpEnviroment();
            App.EventHandler.sentFromForm1 = false;
        }

        private void btnCloseProgram_Click(object sender, EventArgs e)
        {
            ApplicationManager.exitApplication();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            ApplicationManager.minimizeWindow(this);
        }

        private void btnContractOnDefinitvePeriod_Click(object sender, EventArgs e)
        {
            labelTitle.Text = "Ugovor o radu na određeno vrijeme";
            ApplicationManager.switchForm(this, new ContractOnDefiniteTime(), true);
        }

        private void btnCloseProgram_MouseHover(object sender, EventArgs e)
        {
            mainFormHelper.Show("Izlaz iz programa", btnCloseProgram);
        }

        private void btnMinimize_MouseHover(object sender, EventArgs e)
        {
            mainFormHelper.Show("Minimiziraj prozor", btnMinimize);
        }

        private void btnEmployers_Click(object sender, EventArgs e)
        {
            ApplicationManager.putFromIntoPanel(new EmployerForm(), panelForm);
            btnEmployers.Hide();
            App.EventHandler.sentFromForm1 = true;
        }

        private void btnGfiPod_Click(object sender, EventArgs e)
        {
            ApplicationManager.switchForm(this, new GfiPod(), true);
        }

        private void btnContractOnIndefinitivePeriod_Click(object sender, EventArgs e)
        {
            ApplicationManager.switchForm(this, new ContractOnIndefiniteTime(), true);
        }
    }
}
