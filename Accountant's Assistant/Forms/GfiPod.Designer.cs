
using Accountant_s_Assistant.Resources;

namespace Accountant_s_Assistant.Forms
{
    partial class GfiPod
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoadGfiPod = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbWorkDone = new Accountant_s_Assistant.Resources.TextProgressBar();
            this.btnRunCreator = new System.Windows.Forms.Button();
            this.btnCloseProgram = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadGfiPod
            // 
            this.btnLoadGfiPod.FlatAppearance.BorderSize = 0;
            this.btnLoadGfiPod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadGfiPod.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLoadGfiPod.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoadGfiPod.Location = new System.Drawing.Point(222, 68);
            this.btnLoadGfiPod.Name = "btnLoadGfiPod";
            this.btnLoadGfiPod.Size = new System.Drawing.Size(149, 70);
            this.btnLoadGfiPod.TabIndex = 1;
            this.btnLoadGfiPod.Text = "Učitaj\r\nGFI POD ";
            this.btnLoadGfiPod.UseVisualStyleBackColor = true;
            this.btnLoadGfiPod.Click += new System.EventHandler(this.btnLoadGfiPod_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbWorkDone);
            this.panel1.Controls.Add(this.btnRunCreator);
            this.panel1.Controls.Add(this.btnLoadGfiPod);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 406);
            this.panel1.TabIndex = 3;
            // 
            // pbWorkDone
            // 
            this.pbWorkDone.CustomText = "";
            this.pbWorkDone.Location = new System.Drawing.Point(138, 170);
            this.pbWorkDone.Name = "pbWorkDone";
            this.pbWorkDone.ProgressColor = System.Drawing.Color.LightGreen;
            this.pbWorkDone.Size = new System.Drawing.Size(514, 35);
            this.pbWorkDone.TabIndex = 4;
            this.pbWorkDone.TextColor = System.Drawing.Color.Black;
            this.pbWorkDone.TextFont = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.pbWorkDone.VisualMode = Accountant_s_Assistant.Resources.ProgressBarDisplayMode.CurrProgress;
            // 
            // btnRunCreator
            // 
            this.btnRunCreator.FlatAppearance.BorderSize = 0;
            this.btnRunCreator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunCreator.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRunCreator.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRunCreator.Location = new System.Drawing.Point(452, 68);
            this.btnRunCreator.Name = "btnRunCreator";
            this.btnRunCreator.Size = new System.Drawing.Size(149, 70);
            this.btnRunCreator.TabIndex = 3;
            this.btnRunCreator.Text = "Pokreni izradu izvješća";
            this.btnRunCreator.UseVisualStyleBackColor = true;
            this.btnRunCreator.Click += new System.EventHandler(this.btnRunCreator_Click);
            // 
            // btnCloseProgram
            // 
            this.btnCloseProgram.BackgroundImage = global::Accountant_s_Assistant.Properties.Resources.buttonX;
            this.btnCloseProgram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseProgram.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseProgram.FlatAppearance.BorderSize = 0;
            this.btnCloseProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseProgram.Location = new System.Drawing.Point(761, 0);
            this.btnCloseProgram.Name = "btnCloseProgram";
            this.btnCloseProgram.Size = new System.Drawing.Size(39, 44);
            this.btnCloseProgram.TabIndex = 6;
            this.btnCloseProgram.UseVisualStyleBackColor = true;
            this.btnCloseProgram.Click += new System.EventHandler(this.btnCloseProgram_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // GfiPod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCloseProgram);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GfiPod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GfiPod";
            this.Load += new System.EventHandler(this.GfiPod_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoadGfiPod;
        private TextProgressBar pbWorkDone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRunCreator;
        private System.Windows.Forms.Button btnCloseProgram;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}