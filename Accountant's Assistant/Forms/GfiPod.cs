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
        private List<KeyValuePair<string, string>> gfiPodInformation;
        public GfiPod()
        {
            InitializeComponent();
        }

        private void loadProgram()
        {
            gfiPodPath = "";
            gfiPodInformation = null;
            pbWorkDone.VisualMode = ProgressBarDisplayMode.CustomText;
            pbWorkDone.ProgressColor = Color.Green;

            btnValidate.Enabled = false;
            btnRunCreator.Enabled = false;
            tbLossInformation.Enabled = false;
            tbLossInformation.Text = "Prvo učitajte GFI POD obrazac.";
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
                gfiPodInformation = PDFCreator.initGfiInformation(gfiPodPath);

                btnValidate.Enabled = true;
                tbLossInformation.Text = "Validirajte GFI POD obrazac.";
            }
            else
            {
                MessageBox.Show("Greška prilikom učitavanja datoteke", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GfiPod_Load(object sender, EventArgs e)
        {
            btnRunCreator.Enabled = false;
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
            performStep("Izrada izvješća u tijeku", 0);

            int returnCode = PDFCreator.generateGfiReport1(gfiPodInformation);

            if (returnCode == ErrorCodes.NoError)
            {
                performStep("Završena izrada odluke o utvrđivanju fin. izv.", 33);
                returnCode = PDFCreator.generateGfiReport2(gfiPodInformation);
                performStep("Izrada bilješki uz fin. izv.", 33);
                if (returnCode == ErrorCodes.NoError)
                {
                    performStep("Završena izrada bilješki uz fin. izv.", 66);
                    double choice = Convert.ToDouble(gfiPodInformation.Find(x => x.Key == "lossOrGainWithoutTax").Value);
                    if (choice.CompareTo(0.00) < 0)
                    {
                        returnCode = PDFCreator.generateGfiReport3(gfiPodInformation, tbLossInformation.Text);
                    }
                    else
                    {
                        returnCode = PDFCreator.generateGfiReport3(gfiPodInformation, "");
                    }  
                    performStep("Izrada odluke o pokriću dobiti i gubitka", 66);
                    if (returnCode == ErrorCodes.NoError)
                    {
                        performStep("Završena izrada odluke o pokriću dobiti i gubitka", 100);
                        //MessageBox.Show("Izvješća u izrađena i nalaze se na lokaciji \n" + path, "Zavrešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        performStep("Izrada izvješća je uspješna. Pronađite ih u mapi Dokumenti!", 100);
                        MessageBox.Show("Izrada izvješća je uspješna. Pronađite ih u mapi Dokumenti!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ApplicationManager.switchForm(this, new Form1(), true);
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

        private void btnValidate_Click(object sender, EventArgs e)
        {
            double choice = Convert.ToDouble(gfiPodInformation.Find(x => x.Key == "lossOrGainWithoutTax").Value);
            if (choice.CompareTo(0.00) < 0)
            {
                tbLossInformation.Enabled = true;
                tbLossInformation.PlaceholderText = "Poduzeće posluje u gubitku, molim da unesete način pokrivanja gubitka.";
                MessageBox.Show("Poduzeće posluje u gubitku,\nmolim da unesete način pokrivanja gubitka.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbLossInformation.Text = "";
            }
            else
            {
                tbLossInformation.Text = "Poduzeće posluje u dobitku, možete pristupiti izradi obrasca!";
                btnRunCreator.Enabled = true;
                tbLossInformation.Text = "";
            }
        }

        private void tbLossInformation_TextChanged(object sender, EventArgs e)
        {
            if (tbLossInformation.Text.Length > 10 && tbLossInformation.Text != "Prvo učitajte GFI POD obrazac." && tbLossInformation.Text != "Validirajte GFI POD obrazac.")
            {
                btnRunCreator.Enabled = true;
            }
        }
    }
}
