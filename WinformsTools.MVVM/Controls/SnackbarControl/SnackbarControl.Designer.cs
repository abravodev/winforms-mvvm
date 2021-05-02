
namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    partial class SnackbarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_snackbarMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_snackbarMessage
            // 
            this.lbl_snackbarMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_snackbarMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_snackbarMessage.ForeColor = System.Drawing.Color.White;
            this.lbl_snackbarMessage.Location = new System.Drawing.Point(5, 5);
            this.lbl_snackbarMessage.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_snackbarMessage.Name = "lbl_snackbarMessage";
            this.lbl_snackbarMessage.Size = new System.Drawing.Size(490, 40);
            this.lbl_snackbarMessage.TabIndex = 0;
            this.lbl_snackbarMessage.Text = "This is a notification shown in the snackbar";
            this.lbl_snackbarMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SnackbarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lbl_snackbarMessage);
            this.Name = "SnackbarControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(500, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_snackbarMessage;
    }
}
