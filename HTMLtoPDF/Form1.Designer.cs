﻿namespace HTMLtoPDF
{
    partial class Form1
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
            this.btnBasicHtmlToPdf = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBasicHtmlToPdf
            // 
            this.btnBasicHtmlToPdf.Location = new System.Drawing.Point(13, 13);
            this.btnBasicHtmlToPdf.Name = "btnBasicHtmlToPdf";
            this.btnBasicHtmlToPdf.Size = new System.Drawing.Size(141, 70);
            this.btnBasicHtmlToPdf.TabIndex = 0;
            this.btnBasicHtmlToPdf.Text = "PechkinHtmlToPdf";
            this.btnBasicHtmlToPdf.UseVisualStyleBackColor = true;
            this.btnBasicHtmlToPdf.Click += new System.EventHandler(this.btnBasicHtmlToPdf_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(178, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 70);
            this.button1.TabIndex = 1;
            this.button1.Text = "NReco";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(370, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 70);
            this.button2.TabIndex = 2;
            this.button2.Text = "Image To Pdf";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 383);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 50);
            this.button3.TabIndex = 3;
            this.button3.Text = "Images Dir";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(151, 383);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 50);
            this.button4.TabIndex = 3;
            this.button4.Text = "Pdfs Dir";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBasicHtmlToPdf);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBasicHtmlToPdf;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}
