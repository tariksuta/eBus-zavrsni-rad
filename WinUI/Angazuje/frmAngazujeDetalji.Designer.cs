﻿namespace eBus.WinUI.Angazuje
{
    partial class frmAngazujeDetalji
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAngazujeDetalji));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDatumIsteka = new System.Windows.Forms.DateTimePicker();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDatumAngazovanja = new System.Windows.Forms.DateTimePicker();
            this.cmbVozila = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLinije = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSmanji = new System.Windows.Forms.Button();
            this.btn_Izadji = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpDatumIsteka);
            this.groupBox1.Controls.Add(this.btnSacuvaj);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpDatumAngazovanja);
            this.groupBox1.Controls.Add(this.cmbVozila);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbLinije);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 441);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Angažman";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Datum završetka angažovanja";
            // 
            // dtpDatumIsteka
            // 
            this.dtpDatumIsteka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDatumIsteka.Location = new System.Drawing.Point(38, 297);
            this.dtpDatumIsteka.Name = "dtpDatumIsteka";
            this.dtpDatumIsteka.Size = new System.Drawing.Size(390, 26);
            this.dtpDatumIsteka.TabIndex = 7;
            this.dtpDatumIsteka.Validating += new System.ComponentModel.CancelEventHandler(this.dtpDatumIsteka_Validating);
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSacuvaj.Location = new System.Drawing.Point(322, 361);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(106, 28);
            this.btnSacuvaj.TabIndex = 6;
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;
            this.btnSacuvaj.Click += new System.EventHandler(this.btnSacuvaj_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Datum početka angažovanja";
            // 
            // dtpDatumAngazovanja
            // 
            this.dtpDatumAngazovanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDatumAngazovanja.Location = new System.Drawing.Point(38, 226);
            this.dtpDatumAngazovanja.Name = "dtpDatumAngazovanja";
            this.dtpDatumAngazovanja.Size = new System.Drawing.Size(390, 26);
            this.dtpDatumAngazovanja.TabIndex = 4;
            this.dtpDatumAngazovanja.Validating += new System.ComponentModel.CancelEventHandler(this.dtpDatumAngazovanja_Validating);
            // 
            // cmbVozila
            // 
            this.cmbVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVozila.FormattingEnabled = true;
            this.cmbVozila.Location = new System.Drawing.Point(38, 149);
            this.cmbVozila.Name = "cmbVozila";
            this.cmbVozila.Size = new System.Drawing.Size(390, 28);
            this.cmbVozila.TabIndex = 3;
            this.cmbVozila.Validating += new System.ComponentModel.CancelEventHandler(this.cmbVozila_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vozilo";
            // 
            // cmbLinije
            // 
            this.cmbLinije.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLinije.FormattingEnabled = true;
            this.cmbLinije.Location = new System.Drawing.Point(38, 72);
            this.cmbLinije.Name = "cmbLinije";
            this.cmbLinije.Size = new System.Drawing.Size(390, 28);
            this.cmbLinije.TabIndex = 1;
            this.cmbLinije.Validating += new System.ComponentModel.CancelEventHandler(this.cmbLinije_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Linija";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Firebrick;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnSmanji);
            this.panel1.Controls.Add(this.btn_Izadji);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 37);
            this.panel1.TabIndex = 7;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnSmanji
            // 
            this.btnSmanji.BackColor = System.Drawing.Color.Firebrick;
            this.btnSmanji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmanji.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmanji.ForeColor = System.Drawing.Color.White;
            this.btnSmanji.Location = new System.Drawing.Point(430, 2);
            this.btnSmanji.Name = "btnSmanji";
            this.btnSmanji.Size = new System.Drawing.Size(33, 31);
            this.btnSmanji.TabIndex = 6;
            this.btnSmanji.Text = "-";
            this.btnSmanji.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSmanji.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSmanji.UseVisualStyleBackColor = false;
            this.btnSmanji.Click += new System.EventHandler(this.btnSmanji_Click);
            // 
            // btn_Izadji
            // 
            this.btn_Izadji.BackColor = System.Drawing.Color.Firebrick;
            this.btn_Izadji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Izadji.ForeColor = System.Drawing.Color.White;
            this.btn_Izadji.Location = new System.Drawing.Point(469, 2);
            this.btn_Izadji.Name = "btn_Izadji";
            this.btn_Izadji.Size = new System.Drawing.Size(34, 32);
            this.btn_Izadji.TabIndex = 6;
            this.btn_Izadji.Text = "X";
            this.btn_Izadji.UseVisualStyleBackColor = false;
            this.btn_Izadji.Click += new System.EventHandler(this.btn_Izadji_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Mistral", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(208, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "ANGAŽUJE";
            // 
            // frmAngazujeDetalji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 493);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAngazujeDetalji";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAngazujeDetalji";
            this.Load += new System.EventHandler(this.frmAngazujeDetalji_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbVozila;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLinije;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDatumAngazovanja;
        private System.Windows.Forms.Button btnSacuvaj;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDatumIsteka;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSmanji;
        private System.Windows.Forms.Button btn_Izadji;
        private System.Windows.Forms.Label label5;
    }
}