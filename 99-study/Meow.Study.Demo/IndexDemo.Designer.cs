namespace Meow.Study.Demo
{
    partial class IndexDemo
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
            this.EmailButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EmailButton
            // 
            this.EmailButton.Location = new System.Drawing.Point(13, 13);
            this.EmailButton.Name = "EmailButton";
            this.EmailButton.Size = new System.Drawing.Size(75, 23);
            this.EmailButton.TabIndex = 0;
            this.EmailButton.Text = "Email";
            this.EmailButton.UseVisualStyleBackColor = true;
            this.EmailButton.Click += new System.EventHandler(this.EmailButton_Click);
            // 
            // IndexDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EmailButton);
            this.Name = "IndexDemo";
            this.Text = "Demo首页";
            this.Load += new System.EventHandler(this.IndexDemo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EmailButton;
    }
}