﻿namespace FileExplorer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView_list = new System.Windows.Forms.TreeView();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_open = new System.Windows.Forms.Button();
            this.listView_show = new System.Windows.Forms.ListView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.button_back = new System.Windows.Forms.Button();
            this.richTextBox_txtShow = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_delete = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button_go = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_list
            // 
            this.treeView_list.Location = new System.Drawing.Point(28, 164);
            this.treeView_list.Margin = new System.Windows.Forms.Padding(7);
            this.treeView_list.Name = "treeView_list";
            this.treeView_list.Size = new System.Drawing.Size(536, 1507);
            this.treeView_list.TabIndex = 0;
            this.treeView_list.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_list_AfterSelect);
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(96, 29);
            this.textBox_path.Margin = new System.Windows.Forms.Padding(7);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(1752, 35);
            this.textBox_path.TabIndex = 1;
            // 
            // button_open
            // 
            this.button_open.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_open.Location = new System.Drawing.Point(1794, 92);
            this.button_open.Margin = new System.Windows.Forms.Padding(7);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(175, 56);
            this.button_open.TabIndex = 2;
            this.button_open.Text = "打开";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // listView_show
            // 
            this.listView_show.Location = new System.Drawing.Point(583, 164);
            this.listView_show.Margin = new System.Windows.Forms.Padding(7);
            this.listView_show.Name = "listView_show";
            this.listView_show.Size = new System.Drawing.Size(1446, 872);
            this.listView_show.TabIndex = 3;
            this.listView_show.UseCompatibleStateImageBehavior = false;
            this.listView_show.View = System.Windows.Forms.View.List;
            this.listView_show.SelectedIndexChanged += new System.EventHandler(this.listView_show_SelectedIndexChanged_1);
            // 
            // button_back
            // 
            this.button_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_back.Location = new System.Drawing.Point(28, 94);
            this.button_back.Margin = new System.Windows.Forms.Padding(7);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(175, 56);
            this.button_back.TabIndex = 4;
            this.button_back.Text = "Back";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // richTextBox_txtShow
            // 
            this.richTextBox_txtShow.Location = new System.Drawing.Point(583, 1056);
            this.richTextBox_txtShow.Margin = new System.Windows.Forms.Padding(7);
            this.richTextBox_txtShow.Name = "richTextBox_txtShow";
            this.richTextBox_txtShow.Size = new System.Drawing.Size(1446, 615);
            this.richTextBox_txtShow.TabIndex = 5;
            this.richTextBox_txtShow.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(929, 1056);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(756, 621);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // button_delete
            // 
            this.button_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_delete.Location = new System.Drawing.Point(1444, 92);
            this.button_delete.Margin = new System.Windows.Forms.Padding(7);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(175, 56);
            this.button_delete.TabIndex = 10;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 33, 0);
            this.statusStrip1.Size = new System.Drawing.Size(2070, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.StatuUpdate);
            // 
            // button_go
            // 
            this.button_go.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_go.Location = new System.Drawing.Point(1869, 29);
            this.button_go.Margin = new System.Windows.Forms.Padding(7);
            this.button_go.Name = "button_go";
            this.button_go.Size = new System.Drawing.Size(175, 56);
            this.button_go.TabIndex = 12;
            this.button_go.Text = "Go";
            this.button_go.UseVisualStyleBackColor = true;
            this.button_go.Click += new System.EventHandler(this.button_go_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AcceptButton = this.button_go;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(2070, 1730);
            this.Controls.Add(this.button_go);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox_txtShow);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.listView_show);
            this.Controls.Add(this.button_open);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.treeView_list);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Form1";
            this.Text = "文件保险箱";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_list;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.ListView listView_show;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.RichTextBox richTextBox_txtShow;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_go;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

