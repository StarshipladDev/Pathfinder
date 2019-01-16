using System.Drawing;

namespace Pathfinder1._0
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textOutput;
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
            this.button1 = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(400,255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.run_Path);
            // 
            // textBox1
            // 
            this.textOutput.Location = new System.Drawing.Point(400,5);
            this.textOutput.Multiline = true;
            this.textOutput.WordWrap = true;
            this.textOutput.Name = "textBox1";
            this.textOutput.Size = new System.Drawing.Size(450,250);
            this.textOutput.TabIndex = 1;
            this.textOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            //System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#a05b16");
            Color col= Color.FromArgb(160, 91, 22);
            this.BackColor = col;

            this.ResumeLayout(false);
            this.PerformLayout();
            this.Paint += this.Form1_Paint;

        }

        private System.Windows.Forms.TextBox textBox1;
    }

        #endregion

        
}

