
namespace Accountant_s_Assistant.Forms
{
    partial class EmployerForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvEmployers = new System.Windows.Forms.DataGridView();
            this.btnClearFields = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.tbEmployerDirector = new System.Windows.Forms.TextBox();
            this.tbEmployerCity = new System.Windows.Forms.TextBox();
            this.tbEmployerPostal = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbEmployerStreet = new System.Windows.Forms.TextBox();
            this.tbEmployerVAT = new System.Windows.Forms.TextBox();
            this.tbEmployerName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployers)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(923, 46);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::Accountant_s_Assistant.Properties.Resources.buttonX;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(873, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 46);
            this.btnClose.TabIndex = 6;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(784, 259);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 57);
            this.button3.TabIndex = 17;
            this.button3.Text = "Obriši poslodavca";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(228, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Pregled unesenih poslodavaca";
            // 
            // dgvEmployers
            // 
            this.dgvEmployers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployers.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvEmployers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployers.MultiSelect = true;
            this.dgvEmployers.ForeColor = System.Drawing.Color.Black;
            this.dgvEmployers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployers.Location = new System.Drawing.Point(36, 391);
            this.dgvEmployers.Name = "dgvEmployers";
            this.dgvEmployers.ReadOnly = true;
            this.dgvEmployers.RowTemplate.Height = 25;
            this.dgvEmployers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployers.Size = new System.Drawing.Size(855, 188);
            this.dgvEmployers.TabIndex = 15;
            this.dgvEmployers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployers_CellContentClick);
            // 
            // btnClearFields
            // 
            this.btnClearFields.FlatAppearance.BorderSize = 0;
            this.btnClearFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFields.Location = new System.Drawing.Point(784, 167);
            this.btnClearFields.Name = "btnClearFields";
            this.btnClearFields.Size = new System.Drawing.Size(107, 57);
            this.btnClearFields.TabIndex = 14;
            this.btnClearFields.Text = "Očisti polja";
            this.btnClearFields.UseVisualStyleBackColor = true;
            this.btnClearFields.Click += new System.EventHandler(this.btnClearFields_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.FlatAppearance.BorderSize = 0;
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.Location = new System.Drawing.Point(784, 84);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(107, 57);
            this.btnInsert.TabIndex = 13;
            this.btnInsert.Text = "Unesi";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // tbEmployerDirector
            // 
            this.tbEmployerDirector.Location = new System.Drawing.Point(242, 295);
            this.tbEmployerDirector.Name = "tbEmployerDirector";
            this.tbEmployerDirector.Size = new System.Drawing.Size(518, 23);
            this.tbEmployerDirector.TabIndex = 12;
            // 
            // tbEmployerCity
            // 
            this.tbEmployerCity.Location = new System.Drawing.Point(242, 167);
            this.tbEmployerCity.Name = "tbEmployerCity";
            this.tbEmployerCity.Size = new System.Drawing.Size(518, 23);
            this.tbEmployerCity.TabIndex = 10;
            // 
            // tbEmployerPostal
            // 
            this.tbEmployerPostal.Location = new System.Drawing.Point(242, 212);
            this.tbEmployerPostal.Name = "tbEmployerPostal";
            this.tbEmployerPostal.Size = new System.Drawing.Size(518, 23);
            this.tbEmployerPostal.TabIndex = 9;
            this.tbEmployerPostal.TextChanged += new System.EventHandler(this.tbEmployerPostal_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbEmployerStreet);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dgvEmployers);
            this.panel2.Controls.Add(this.btnClearFields);
            this.panel2.Controls.Add(this.btnInsert);
            this.panel2.Controls.Add(this.tbEmployerDirector);
            this.panel2.Controls.Add(this.tbEmployerCity);
            this.panel2.Controls.Add(this.tbEmployerPostal);
            this.panel2.Controls.Add(this.tbEmployerVAT);
            this.panel2.Controls.Add(this.tbEmployerName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(923, 618);
            this.panel2.TabIndex = 18;
            // 
            // tbEmployerStreet
            // 
            this.tbEmployerStreet.Location = new System.Drawing.Point(242, 127);
            this.tbEmployerStreet.Name = "tbEmployerStreet";
            this.tbEmployerStreet.Size = new System.Drawing.Size(518, 23);
            this.tbEmployerStreet.TabIndex = 18;
            // 
            // tbEmployerVAT
            // 
            this.tbEmployerVAT.Location = new System.Drawing.Point(242, 252);
            this.tbEmployerVAT.Name = "tbEmployerVAT";
            this.tbEmployerVAT.Size = new System.Drawing.Size(518, 23);
            this.tbEmployerVAT.TabIndex = 8;
            this.tbEmployerVAT.TextChanged += new System.EventHandler(this.tbEmployerVAT_TextChanged);
            // 
            // tbEmployerName
            // 
            this.tbEmployerName.Location = new System.Drawing.Point(242, 84);
            this.tbEmployerName.Name = "tbEmployerName";
            this.tbEmployerName.Size = new System.Drawing.Size(518, 23);
            this.tbEmployerName.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "OIB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Odgovorna osoba";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Poštanski broj";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mjesto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ulica";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Naziv poslodavca";
            // 
            // EmployerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(923, 618);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "EmployerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployerForm";
            this.Load += new System.EventHandler(this.EmployerForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployers)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbEmployerName;
        private System.Windows.Forms.TextBox tbEmployerVAT;
        private System.Windows.Forms.TextBox tbEmployerPostal;
        private System.Windows.Forms.TextBox tbEmployerCity;
        private System.Windows.Forms.TextBox tbEmployerDirector;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnClearFields;
        private System.Windows.Forms.DataGridView dgvEmployers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbEmployerStreet;
    }
}