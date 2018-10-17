namespace STRenderWebService
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
            this.btnHtmlToPdf = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelIP = new System.Windows.Forms.Label();
            this.btnImageToPdf = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCombine = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelIsLocalHost = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnHtmlToPdf
            // 
            this.btnHtmlToPdf.Location = new System.Drawing.Point(12, 241);
            this.btnHtmlToPdf.Name = "btnHtmlToPdf";
            this.btnHtmlToPdf.Size = new System.Drawing.Size(109, 36);
            this.btnHtmlToPdf.TabIndex = 1;
            this.btnHtmlToPdf.Text = "Html To Pdf";
            this.btnHtmlToPdf.UseVisualStyleBackColor = true;
            this.btnHtmlToPdf.Click += new System.EventHandler(this.btnHtmlToPdf_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "ImageToBase64";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(104, 21);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(17, 13);
            this.labelIP.TabIndex = 3;
            this.labelIP.Text = "IP";
            // 
            // btnImageToPdf
            // 
            this.btnImageToPdf.Location = new System.Drawing.Point(12, 202);
            this.btnImageToPdf.Name = "btnImageToPdf";
            this.btnImageToPdf.Size = new System.Drawing.Size(109, 33);
            this.btnImageToPdf.TabIndex = 4;
            this.btnImageToPdf.Text = "Image To Pdf";
            this.btnImageToPdf.UseVisualStyleBackColor = true;
            this.btnImageToPdf.Click += new System.EventHandler(this.btnImageToPdf_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "MyIp:";
            // 
            // btnCombine
            // 
            this.btnCombine.Location = new System.Drawing.Point(12, 162);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(109, 34);
            this.btnCombine.TabIndex = 6;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(440, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "IsLocalHost";
            // 
            // labelIsLocalHost
            // 
            this.labelIsLocalHost.AutoSize = true;
            this.labelIsLocalHost.Location = new System.Drawing.Point(532, 21);
            this.labelIsLocalHost.Name = "labelIsLocalHost";
            this.labelIsLocalHost.Size = new System.Drawing.Size(31, 13);
            this.labelIsLocalHost.TabIndex = 8;
            this.labelIsLocalHost.Text = "none";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "port:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(291, 21);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(25, 13);
            this.labelPort.TabIndex = 10;
            this.labelPort.Text = "000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelIsLocalHost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImageToPdf);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnHtmlToPdf);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnHtmlToPdf;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Button btnImageToPdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelIsLocalHost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPort;
    }
}

