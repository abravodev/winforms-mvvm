
namespace UserManager.Views
{
    partial class RolesView
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
            this.dgv_rolelist = new System.Windows.Forms.DataGridView();
            this.pb_loading = new System.Windows.Forms.ProgressBar();
            this.tlp_view = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rolelist)).BeginInit();
            this.tlp_view.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_rolelist
            // 
            this.dgv_rolelist.AccessibleName = "Roles list";
            this.dgv_rolelist.AllowUserToAddRows = false;
            this.dgv_rolelist.AllowUserToDeleteRows = false;
            this.dgv_rolelist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_rolelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rolelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_rolelist.Location = new System.Drawing.Point(8, 8);
            this.dgv_rolelist.Name = "dgv_rolelist";
            this.dgv_rolelist.ReadOnly = true;
            this.dgv_rolelist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rolelist.Size = new System.Drawing.Size(784, 434);
            this.dgv_rolelist.TabIndex = 0;
            // 
            // pb_loading
            // 
            this.pb_loading.Dock = System.Windows.Forms.DockStyle.Top;
            this.pb_loading.Location = new System.Drawing.Point(0, 0);
            this.pb_loading.MarqueeAnimationSpeed = 10;
            this.pb_loading.Name = "pb_loading";
            this.pb_loading.Size = new System.Drawing.Size(800, 23);
            this.pb_loading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_loading.TabIndex = 1;
            // 
            // tlp_view
            // 
            this.tlp_view.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_view.ColumnCount = 1;
            this.tlp_view.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_view.Controls.Add(this.dgv_rolelist, 0, 0);
            this.tlp_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_view.Location = new System.Drawing.Point(0, 0);
            this.tlp_view.Name = "tlp_view";
            this.tlp_view.Padding = new System.Windows.Forms.Padding(5);
            this.tlp_view.RowCount = 1;
            this.tlp_view.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_view.Size = new System.Drawing.Size(800, 450);
            this.tlp_view.TabIndex = 2;
            // 
            // RolesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pb_loading);
            this.Controls.Add(this.tlp_view);
            this.Name = "RolesView";
            this.Text = "RolesView";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rolelist)).EndInit();
            this.tlp_view.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_rolelist;
        private System.Windows.Forms.ProgressBar pb_loading;
        private System.Windows.Forms.TableLayoutPanel tlp_view;
    }
}