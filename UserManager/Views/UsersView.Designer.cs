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
            this.components = new System.ComponentModel.Container();
            this.dgv_userlist = new System.Windows.Forms.DataGridView();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.tlp_createView = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_createFields = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_firstName = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.tb_firstName = new System.Windows.Forms.TextBox();
            this.tb_lastName = new System.Windows.Forms.TextBox();
            this.lbl_lastName = new System.Windows.Forms.Label();
            this.tlp_createActions = new System.Windows.Forms.TableLayoutPanel();
            this.bt_save = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.tlp_view = new System.Windows.Forms.TableLayoutPanel();
            this.ep_createUser = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_userlist)).BeginInit();
            this.tlp_createView.SuspendLayout();
            this.tlp_createFields.SuspendLayout();
            this.tlp_createActions.SuspendLayout();
            this.tlp_view.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ep_createUser)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_userlist
            // 
            this.dgv_userlist.AllowUserToAddRows = false;
            this.dgv_userlist.AllowUserToDeleteRows = false;
            this.dgv_userlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_userlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_userlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_userlist.Location = new System.Drawing.Point(264, 8);
            this.dgv_userlist.Name = "dgv_userlist";
            this.dgv_userlist.ReadOnly = true;
            this.dgv_userlist.Size = new System.Drawing.Size(528, 434);
            this.dgv_userlist.TabIndex = 0;
            // 
            // tb_email
            // 
            this.tb_email.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_email.Location = new System.Drawing.Point(65, 55);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(182, 20);
            this.tb_email.TabIndex = 6;
            // 
            // tlp_createView
            // 
            this.tlp_createView.AutoSize = true;
            this.tlp_createView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_createView.ColumnCount = 1;
            this.tlp_createView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_createView.Controls.Add(this.tlp_createFields, 0, 0);
            this.tlp_createView.Controls.Add(this.tlp_createActions, 0, 1);
            this.tlp_createView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_createView.Location = new System.Drawing.Point(8, 8);
            this.tlp_createView.MaximumSize = new System.Drawing.Size(250, 0);
            this.tlp_createView.MinimumSize = new System.Drawing.Size(250, 0);
            this.tlp_createView.Name = "tlp_createView";
            this.tlp_createView.RowCount = 3;
            this.tlp_createView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createView.Size = new System.Drawing.Size(250, 434);
            this.tlp_createView.TabIndex = 8;
            // 
            // tlp_createFields
            // 
            this.tlp_createFields.AutoSize = true;
            this.tlp_createFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_createFields.ColumnCount = 2;
            this.tlp_createFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_createFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_createFields.Controls.Add(this.tb_email, 1, 2);
            this.tlp_createFields.Controls.Add(this.lbl_firstName, 0, 0);
            this.tlp_createFields.Controls.Add(this.lbl_email, 0, 2);
            this.tlp_createFields.Controls.Add(this.tb_firstName, 1, 0);
            this.tlp_createFields.Controls.Add(this.tb_lastName, 1, 1);
            this.tlp_createFields.Controls.Add(this.lbl_lastName, 0, 1);
            this.tlp_createFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_createFields.Location = new System.Drawing.Point(0, 0);
            this.tlp_createFields.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_createFields.Name = "tlp_createFields";
            this.tlp_createFields.RowCount = 3;
            this.tlp_createFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createFields.Size = new System.Drawing.Size(250, 78);
            this.tlp_createFields.TabIndex = 9;
            // 
            // lbl_firstName
            // 
            this.lbl_firstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_firstName.AutoSize = true;
            this.lbl_firstName.Location = new System.Drawing.Point(3, 6);
            this.lbl_firstName.Name = "lbl_firstName";
            this.lbl_firstName.Size = new System.Drawing.Size(55, 13);
            this.lbl_firstName.TabIndex = 1;
            this.lbl_firstName.Text = "First name";
            // 
            // lbl_email
            // 
            this.lbl_email.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_email.AutoSize = true;
            this.lbl_email.Location = new System.Drawing.Point(3, 58);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(32, 13);
            this.lbl_email.TabIndex = 3;
            this.lbl_email.Text = "Email";
            // 
            // tb_firstName
            // 
            this.tb_firstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_firstName.Location = new System.Drawing.Point(65, 3);
            this.tb_firstName.Name = "tb_firstName";
            this.tb_firstName.Size = new System.Drawing.Size(182, 20);
            this.tb_firstName.TabIndex = 4;
            // 
            // tb_lastName
            // 
            this.tb_lastName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_lastName.Location = new System.Drawing.Point(65, 29);
            this.tb_lastName.Name = "tb_lastName";
            this.tb_lastName.Size = new System.Drawing.Size(182, 20);
            this.tb_lastName.TabIndex = 5;
            // 
            // lbl_lastName
            // 
            this.lbl_lastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_lastName.AutoSize = true;
            this.lbl_lastName.Location = new System.Drawing.Point(3, 32);
            this.lbl_lastName.Name = "lbl_lastName";
            this.lbl_lastName.Size = new System.Drawing.Size(56, 13);
            this.lbl_lastName.TabIndex = 2;
            this.lbl_lastName.Text = "Last name";
            // 
            // tlp_createActions
            // 
            this.tlp_createActions.AutoSize = true;
            this.tlp_createActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_createActions.ColumnCount = 2;
            this.tlp_createActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_createActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_createActions.Controls.Add(this.bt_save, 0, 0);
            this.tlp_createActions.Controls.Add(this.bt_cancel, 1, 0);
            this.tlp_createActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_createActions.Location = new System.Drawing.Point(0, 78);
            this.tlp_createActions.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_createActions.Name = "tlp_createActions";
            this.tlp_createActions.RowCount = 1;
            this.tlp_createActions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_createActions.Size = new System.Drawing.Size(250, 29);
            this.tlp_createActions.TabIndex = 8;
            // 
            // bt_save
            // 
            this.bt_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_save.Location = new System.Drawing.Point(3, 3);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(119, 23);
            this.bt_save.TabIndex = 7;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            // 
            // bt_cancel
            // 
            this.bt_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_cancel.Location = new System.Drawing.Point(128, 3);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(119, 23);
            this.bt_cancel.TabIndex = 8;
            this.bt_cancel.Text = "Cancel";
            this.bt_cancel.UseVisualStyleBackColor = true;
            // 
            // tlp_view
            // 
            this.tlp_view.ColumnCount = 2;
            this.tlp_view.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_view.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_view.Controls.Add(this.dgv_userlist, 1, 0);
            this.tlp_view.Controls.Add(this.tlp_createView, 0, 0);
            this.tlp_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_view.Location = new System.Drawing.Point(0, 0);
            this.tlp_view.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_view.Name = "tlp_view";
            this.tlp_view.Padding = new System.Windows.Forms.Padding(5);
            this.tlp_view.RowCount = 1;
            this.tlp_view.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_view.Size = new System.Drawing.Size(800, 450);
            this.tlp_view.TabIndex = 9;
            // 
            // ep_createUser
            // 
            this.ep_createUser.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ep_createUser.ContainerControl = this;
            // 
            // UsersView
            // 
            this.AcceptButton = this.bt_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_view);
            this.Name = "UsersView";
            this.Text = "UsersView";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_userlist)).EndInit();
            this.tlp_createView.ResumeLayout(false);
            this.tlp_createView.PerformLayout();
            this.tlp_createFields.ResumeLayout(false);
            this.tlp_createFields.PerformLayout();
            this.tlp_createActions.ResumeLayout(false);
            this.tlp_view.ResumeLayout(false);
            this.tlp_view.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ep_createUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_userlist;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.TableLayoutPanel tlp_createView;
        private System.Windows.Forms.TableLayoutPanel tlp_view;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.TextBox tb_lastName;
        private System.Windows.Forms.Label lbl_lastName;
        private System.Windows.Forms.TableLayoutPanel tlp_createFields;
        private System.Windows.Forms.Label lbl_firstName;
        private System.Windows.Forms.TextBox tb_firstName;
        private System.Windows.Forms.TableLayoutPanel tlp_createActions;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.ErrorProvider ep_createUser;
    }
}