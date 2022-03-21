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
        public GfiPod()
        {
            InitializeComponent();
        }

        private void loadProgram()
        {
            gfiPodPath = "";
            btnRunCreator.Enabled = false;
            pbWorkDone.VisualMode = ProgressBarDisplayMode.CustomText;
            pbWorkDone.ProgressColor = Color.Green;
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

        private void performStep(string message, int step)
        {
            pbWorkDone.CustomText = message;
            pbWorkDone.Step = step;
            pbWorkDone.PerformStep();
        }

        private void generateReports()
        {
            pbWorkDone.CustomText = "Izrada izvješća u tijeku";


            int returnCode = PDFCreator.generateGfiReport1(gfiPodPath);

            if (returnCode == ErrorCodes.NoError)
            {
                performStep("Završena izrada odluke o utvrđivanju fin. izv.", 33);
                returnCode = PDFCreator.generateGfiReport2(gfiPodPath);
                performStep("Izrada bilješki uz fin. izv.", 33);
                if (returnCode == ErrorCodes.NoError)
                {
                    performStep("Završena izrada bilješki uz fin. izv.", 66);
                    returnCode = PDFCreator.generateGfiReport3(gfiPodPath);
                    performStep("Izrada odluke o pokriću dobiti i gubitka", 66);
                    if (returnCode == ErrorCodes.NoError)
                    {
                        performStep("Završena izrada odluke o pokriću dobiti i gubitka", 100);
                        //MessageBox.Show("Izvješća u izrađena i nalaze se na lokaciji \n" + path, "Zavrešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        performStep("Izrada izvješća je uspješna. Pronađite ih u mapi Dokumenti!", 100);
                    }
                    else
                    {
                        performStep("Greška prilikom izrade odluke o pokriću dobiti i gubitka", 66);
                        messageBoxReportError();
                    }
                }
                else
                {
                    performStep("Greška prilikom izrade bilješki uz fin. izv.", 33);
                    messageBoxReportError();
                }
            }
            else
            {
                performStep("Greška prilikom izrade\nodluke o utvrđivanju fin. izv.", 0);
                messageBoxReportError();
            }
        }

        private void messageBoxReportError()
        {
            MessageBox.Show("Nešto je pošlo po zlu.\nJedno ili više izvješća možda nije izrađeno!", "Greška prilikom izrade izvješća", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRunCreator_Click(object sender, EventArgs e)
        {
            generateReports();
        }
    }
}
