using Accountant_s_Assistant.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accountant_s_Assistant.Resources;

namespace Accountant_s_Assistant.Forms
{
    public partial class GfiPod : Form
    {
        private string gfiPodPath;
        PDFCreator pdfCreator;
        public GfiPod()
        {
            InitializeComponent();
        }

        private void loadProgram()
        {
            gfiPodPath = "";
            btnRunCreator.Enabled = false;
            pbWorkDone.VisualMode = ProgressBarDisplayMode.CustomText;
        }


        private void btnCloseProgram_Click(object sender, EventArgs e)
        {
            ApplicationManager.switchForm(this, new Form1(), true);
        }

        private void btnLoadGfiPod_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fileDialog.Filter = "excel files (*.xlsx, *.xls)|*.xlsx; *.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                gfiPodPath = fileDialog.FileName;
                pbWorkDone.CustomText = gfiPodPath;
                btnRunCreator.Enabled = true;
            }
            else
            {
                MessageBox.Show("Greška prilikom učitavanja datoteke", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GfiPod_Load(object sender, EventArgs e)
        {
            loadProgram();
        }

        private void btnRunCreator_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pdfCreator = new PDFCreator(path);
            pbWorkDone.CustomText = "Izrada izvješća u tijeku";

            int returnCode = pdfCreator.generateGfiPodReport(gfiPodPath);
            if (returnCode == ErrorCodes.NoError)
            {
                pbWorkDone.CustomText = "Završeno";
                MessageBox.Show("Izvješća u izrađena i nalaze se na lokaciji \n" + path, "Zavrešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                pbWorkDone.CustomText = "Greška prilikom izrade, ponovite postupak.";
                MessageBox.Show("Nešto je pošlo po zlu.\nJedno ili više izvješća možda nije izrađeno!", "Greška prilikom izrade izvješća", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
