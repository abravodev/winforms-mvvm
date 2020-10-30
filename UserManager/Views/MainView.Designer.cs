﻿using MvvmTools.Controls;

namespace UserManager.Views
{
    partial class MainView
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
            this.btn_users = new System.Windows.Forms.Button();
            this.ms_menu = new System.Windows.Forms.MenuStrip();
            this.tsmi_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_language = new MvvmTools.Controls.BindableToolStripMenuItem();
            this.ms_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_users
            // 
            this.btn_users.Location = new System.Drawing.Point(348, 92);
            this.btn_users.Name = "btn_users";
            this.btn_users.Size = new System.Drawing.Size(75, 23);
            this.btn_users.TabIndex = 0;
            this.btn_users.Text = "Users";
            this.btn_users.UseVisualStyleBackColor = true;
            this.btn_users.Click += new System.EventHandler(this.btn_users_click);
            // 
            // ms_menu
            // 
            this.ms_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_settings});
            this.ms_menu.Location = new System.Drawing.Point(0, 0);
            this.ms_menu.Name = "ms_menu";
            this.ms_menu.Size = new System.Drawing.Size(800, 24);
            this.ms_menu.TabIndex = 1;
            this.ms_menu.Text = "menuStrip1";
            // 
            // tsmi_settings
            // 
            this.tsmi_settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_language});
            this.tsmi_settings.Name = "tsmi_settings";
            this.tsmi_settings.Size = new System.Drawing.Size(61, 20);
            this.tsmi_settings.Text = "Settings";
            // 
            // tsmi_language
            // 
            this.tsmi_language.Name = "tsmi_language";
            this.tsmi_language.Size = new System.Drawing.Size(180, 22);
            this.tsmi_language.Text = "Language:";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_users);
            this.Controls.Add(this.ms_menu);
            this.MainMenuStrip = this.ms_menu;
            this.Name = "MainView";
            this.Text = "UserManager | winforms-mvvm";
            this.ms_menu.ResumeLayout(false);
            this.ms_menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_users;
        private System.Windows.Forms.MenuStrip ms_menu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_settings;
        private BindableToolStripMenuItem tsmi_language;
    }
}

