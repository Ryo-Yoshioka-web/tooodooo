﻿namespace ToDoManager
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.taskListPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.settingButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskListPanel
            // 
            this.taskListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskListPanel.AutoScroll = true;
            this.taskListPanel.ColumnCount = 1;
            this.taskListPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.taskListPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.taskListPanel.Location = new System.Drawing.Point(0, 0);
            this.taskListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.taskListPanel.Name = "taskListPanel";
            this.taskListPanel.RowCount = 1;
            this.taskListPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.taskListPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.taskListPanel.Size = new System.Drawing.Size(280, 32);
            this.taskListPanel.TabIndex = 1;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.Color.DarkOrange;
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainPanel.Controls.Add(this.taskListPanel, 0, 0);
            this.mainPanel.Location = new System.Drawing.Point(2, 40);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 2;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Size = new System.Drawing.Size(280, 518);
            this.mainPanel.TabIndex = 2;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 60000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // settingButton
            // 
            this.settingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingButton.BackColor = System.Drawing.Color.DarkOrange;
            this.settingButton.BackgroundImage = global::ToDoManager.Properties.Resources.setting_enable;
            this.settingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingButton.ForeColor = System.Drawing.Color.DarkOrange;
            this.settingButton.Location = new System.Drawing.Point(253, 9);
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(23, 23);
            this.settingButton.TabIndex = 3;
            this.settingButton.UseVisualStyleBackColor = false;
            this.settingButton.Click += new System.EventHandler(this.settingButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackgroundImage = global::ToDoManager.Properties.Resources.plus_enable;
            this.addButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.SystemColors.Control;
            this.addButton.Location = new System.Drawing.Point(10, 10);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(20, 20);
            this.addButton.TabIndex = 0;
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = global::ToDoManager.Properties.Settings.Default.MyClientSize;
            this.Controls.Add(this.settingButton);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.addButton);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::ToDoManager.Properties.Settings.Default, "MyLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::ToDoManager.Properties.Settings.Default, "MyClientSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::ToDoManager.Properties.Settings.Default.MyLocation;
            this.MinimumSize = new System.Drawing.Size(300, 600);
            this.Name = "MainForm";
            this.Text = "とぅーどぅーりすと";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TableLayoutPanel taskListPanel;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Button settingButton;
        private System.Windows.Forms.ToolTip buttonToolTip;
    }
}

