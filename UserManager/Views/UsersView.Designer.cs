using WinformsTools.MVVM.Controls.DataGridViewControl;

namespace UserManager.Views
{
    partial class UsersView
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
            this.dgv_userlist = new Zuby.ADGV.AdvancedDataGridView();
            this.tlp_view = new System.Windows.Forms.TableLayoutPanel();
            this.createUserView = new UserManager.Views.CreateUserView();
            this.pb_loading = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_userlist)).BeginInit();
            this.tlp_view.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_userlist
            // 
            this.dgv_userlist.AccessibleName = "Users list";
            this.dgv_userlist.AllowUserToAddRows = false;
            this.dgv_userlist.AllowUserToDeleteRows = false;
            this.dgv_userlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_userlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_userlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_userlist.FilterAndSortEnabled = true;
            this.dgv_userlist.Location = new System.Drawing.Point(264, 8);
            this.dgv_userlist.MultiSelect = false;
            this.dgv_userlist.Name = "dgv_userlist";
            this.dgv_userlist.ReadOnly = true;
            this.dgv_userlist.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgv_userlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_userlist.Size = new System.Drawing.Size(562, 445);
            this.dgv_userlist.TabIndex = 0;
            // 
            // tlp_view
            // 
            this.tlp_view.ColumnCount = 2;
            this.tlp_view.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_view.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_view.Controls.Add(this.dgv_userlist, 1, 0);
            this.tlp_view.Controls.Add(this.createUserView, 0, 0);
            this.tlp_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_view.Location = new System.Drawing.Point(0, 0);
            this.tlp_view.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_view.Name = "tlp_view";
            this.tlp_view.Padding = new System.Windows.Forms.Padding(5);
            this.tlp_view.RowCount = 1;
            this.tlp_view.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_view.Size = new System.Drawing.Size(834, 461);
            this.tlp_view.TabIndex = 9;
            // 
            // createUserView
            // 
            this.createUserView.AccessibleName = "Create User Form";
            this.createUserView.AutoSize = true;
            this.createUserView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.createUserView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createUserView.Location = new System.Drawing.Point(8, 8);
            this.createUserView.Name = "createUserView";
            this.createUserView.Size = new System.Drawing.Size(250, 445);
            this.createUserView.TabIndex = 1;
            // 
            // pb_loading
            // 
            this.pb_loading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_loading.Location = new System.Drawing.Point(0, 0);
            this.pb_loading.MarqueeAnimationSpeed = 10;
            this.pb_loading.Name = "pb_loading";
            this.pb_loading.Size = new System.Drawing.Size(834, 15);
            this.pb_loading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_loading.TabIndex = 10;
            // 
            // UsersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.pb_loading);
            this.Controls.Add(this.tlp_view);
            this.Name = "UsersView";
            this.Text = "UsersView";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_userlist)).EndInit();
            this.tlp_view.ResumeLayout(false);
            this.tlp_view.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgv_userlist;
        private System.Windows.Forms.TableLayoutPanel tlp_view;
        private System.Windows.Forms.ProgressBar pb_loading;
        private CreateUserView createUserView;
    }
}