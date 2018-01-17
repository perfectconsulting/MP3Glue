namespace MP3Glue
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabFileInfo = new System.Windows.Forms.TabControl();
            this.tabFileView = new System.Windows.Forms.TabPage();
            this.listFile = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabTagView = new System.Windows.Forms.TabPage();
            this.comboTrack = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textGenre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureArtwork = new System.Windows.Forms.PictureBox();
            this.textAlbum = new System.Windows.Forms.TextBox();
            this.textArtist = new System.Windows.Forms.TextBox();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabStatus = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonJoin = new System.Windows.Forms.Button();
            this.comboConversion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonArtwork = new System.Windows.Forms.Button();
            this.tabFileInfo.SuspendLayout();
            this.tabFileView.SuspendLayout();
            this.tabTagView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArtwork)).BeginInit();
            this.tabStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFileInfo
            // 
            this.tabFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFileInfo.Controls.Add(this.tabFileView);
            this.tabFileInfo.Controls.Add(this.tabTagView);
            this.tabFileInfo.Controls.Add(this.tabStatus);
            this.tabFileInfo.Location = new System.Drawing.Point(17, 9);
            this.tabFileInfo.Name = "tabFileInfo";
            this.tabFileInfo.SelectedIndex = 0;
            this.tabFileInfo.Size = new System.Drawing.Size(608, 526);
            this.tabFileInfo.TabIndex = 0;
            // 
            // tabFileView
            // 
            this.tabFileView.Controls.Add(this.listFile);
            this.tabFileView.Location = new System.Drawing.Point(4, 22);
            this.tabFileView.Name = "tabFileView";
            this.tabFileView.Padding = new System.Windows.Forms.Padding(3);
            this.tabFileView.Size = new System.Drawing.Size(600, 500);
            this.tabFileView.TabIndex = 0;
            this.tabFileView.Text = "File View";
            this.tabFileView.UseVisualStyleBackColor = true;
            // 
            // listFile
            // 
            this.listFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnSize,
            this.columnFormat,
            this.columnPath});
            this.listFile.Cursor = System.Windows.Forms.Cursors.Default;
            this.listFile.FullRowSelect = true;
            this.listFile.GridLines = true;
            this.listFile.HideSelection = false;
            this.listFile.Location = new System.Drawing.Point(0, 3);
            this.listFile.Name = "listFile";
            this.listFile.Size = new System.Drawing.Size(597, 491);
            this.listFile.TabIndex = 0;
            this.listFile.UseCompatibleStateImageBehavior = false;
            this.listFile.View = System.Windows.Forms.View.Details;
            this.listFile.SelectedIndexChanged += new System.EventHandler(this.listFile_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 150;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Size";
            this.columnSize.Width = 166;
            // 
            // columnFormat
            // 
            this.columnFormat.Text = "Format";
            this.columnFormat.Width = 150;
            // 
            // columnPath
            // 
            this.columnPath.Text = "Path";
            this.columnPath.Width = 400;
            // 
            // tabTagView
            // 
            this.tabTagView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabTagView.Controls.Add(this.buttonArtwork);
            this.tabTagView.Controls.Add(this.comboTrack);
            this.tabTagView.Controls.Add(this.label6);
            this.tabTagView.Controls.Add(this.textGenre);
            this.tabTagView.Controls.Add(this.label4);
            this.tabTagView.Controls.Add(this.pictureArtwork);
            this.tabTagView.Controls.Add(this.textAlbum);
            this.tabTagView.Controls.Add(this.textArtist);
            this.tabTagView.Controls.Add(this.textTitle);
            this.tabTagView.Controls.Add(this.label3);
            this.tabTagView.Controls.Add(this.label2);
            this.tabTagView.Controls.Add(this.label1);
            this.tabTagView.Location = new System.Drawing.Point(4, 22);
            this.tabTagView.Name = "tabTagView";
            this.tabTagView.Padding = new System.Windows.Forms.Padding(3);
            this.tabTagView.Size = new System.Drawing.Size(600, 500);
            this.tabTagView.TabIndex = 1;
            this.tabTagView.Text = "ID3 Tag View";
            this.tabTagView.UseVisualStyleBackColor = true;
            // 
            // comboTrack
            // 
            this.comboTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTrack.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboTrack.FormattingEnabled = true;
            this.comboTrack.Location = new System.Drawing.Point(78, 130);
            this.comboTrack.Name = "comboTrack";
            this.comboTrack.Size = new System.Drawing.Size(96, 21);
            this.comboTrack.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Track:";
            // 
            // textGenre
            // 
            this.textGenre.Location = new System.Drawing.Point(78, 99);
            this.textGenre.MaxLength = 128;
            this.textGenre.Name = "textGenre";
            this.textGenre.Size = new System.Drawing.Size(253, 20);
            this.textGenre.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Genre:";
            // 
            // pictureArtwork
            // 
            this.pictureArtwork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureArtwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureArtwork.Location = new System.Drawing.Point(32, 208);
            this.pictureArtwork.Name = "pictureArtwork";
            this.pictureArtwork.Size = new System.Drawing.Size(250, 250);
            this.pictureArtwork.TabIndex = 6;
            this.pictureArtwork.TabStop = false;
            // 
            // textAlbum
            // 
            this.textAlbum.Location = new System.Drawing.Point(78, 71);
            this.textAlbum.MaxLength = 128;
            this.textAlbum.Name = "textAlbum";
            this.textAlbum.Size = new System.Drawing.Size(514, 20);
            this.textAlbum.TabIndex = 5;
            this.textAlbum.TextChanged += new System.EventHandler(this.textAlbum_TextChanged);
            // 
            // textArtist
            // 
            this.textArtist.Location = new System.Drawing.Point(78, 45);
            this.textArtist.MaxLength = 128;
            this.textArtist.Name = "textArtist";
            this.textArtist.Size = new System.Drawing.Size(514, 20);
            this.textArtist.TabIndex = 4;
            this.textArtist.TextChanged += new System.EventHandler(this.textArtist_TextChanged);
            // 
            // textTitle
            // 
            this.textTitle.Location = new System.Drawing.Point(78, 19);
            this.textTitle.MaxLength = 128;
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(514, 20);
            this.textTitle.TabIndex = 3;
            this.textTitle.TextChanged += new System.EventHandler(this.textTitle_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Album:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Artist:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // tabStatus
            // 
            this.tabStatus.Controls.Add(this.progressBar);
            this.tabStatus.Controls.Add(this.textStatus);
            this.tabStatus.Location = new System.Drawing.Point(4, 22);
            this.tabStatus.Name = "tabStatus";
            this.tabStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatus.Size = new System.Drawing.Size(600, 500);
            this.tabStatus.TabIndex = 2;
            this.tabStatus.Text = "Status";
            this.tabStatus.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 474);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(588, 16);
            this.progressBar.TabIndex = 1;
            // 
            // textStatus
            // 
            this.textStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textStatus.Location = new System.Drawing.Point(7, 7);
            this.textStatus.Multiline = true;
            this.textStatus.Name = "textStatus";
            this.textStatus.ReadOnly = true;
            this.textStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textStatus.Size = new System.Drawing.Size(587, 463);
            this.textStatus.TabIndex = 0;
            this.textStatus.WordWrap = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(631, 31);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(141, 30);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add File(s)";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(631, 67);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(143, 30);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "Remove File(s)";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonJoin
            // 
            this.buttonJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJoin.Location = new System.Drawing.Point(631, 505);
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.Size = new System.Drawing.Size(142, 30);
            this.buttonJoin.TabIndex = 7;
            this.buttonJoin.Text = "Join File(s)";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Click += new System.EventHandler(this.buttonJoin_Click);
            // 
            // comboConversion
            // 
            this.comboConversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboConversion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConversion.FormattingEnabled = true;
            this.comboConversion.Location = new System.Drawing.Point(631, 256);
            this.comboConversion.Name = "comboConversion";
            this.comboConversion.Size = new System.Drawing.Size(143, 21);
            this.comboConversion.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(631, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Format:";
            // 
            // buttonUp
            // 
            this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUp.Location = new System.Drawing.Point(631, 117);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(142, 30);
            this.buttonUp.TabIndex = 3;
            this.buttonUp.Text = "Move up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDown.Location = new System.Drawing.Point(631, 153);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(142, 30);
            this.buttonDown.TabIndex = 4;
            this.buttonDown.Text = "Move down";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(631, 189);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(141, 30);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonArtwork
            // 
            this.buttonArtwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonArtwork.Location = new System.Drawing.Point(32, 171);
            this.buttonArtwork.Name = "buttonArtwork";
            this.buttonArtwork.Size = new System.Drawing.Size(142, 30);
            this.buttonArtwork.TabIndex = 15;
            this.buttonArtwork.Text = "Load Artwork";
            this.buttonArtwork.UseVisualStyleBackColor = true;
            this.buttonArtwork.Click += new System.EventHandler(this.buttonArtwork_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonDown);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboConversion);
            this.Controls.Add(this.buttonJoin);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.tabFileInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MP3Glue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabFileInfo.ResumeLayout(false);
            this.tabFileView.ResumeLayout(false);
            this.tabTagView.ResumeLayout(false);
            this.tabTagView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArtwork)).EndInit();
            this.tabStatus.ResumeLayout(false);
            this.tabStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabFileInfo;
        private System.Windows.Forms.TabPage tabFileView;
        private System.Windows.Forms.TabPage tabTagView;
        private System.Windows.Forms.ListView listFile;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnPath;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonJoin;
        private System.Windows.Forms.TabPage tabStatus;
        private System.Windows.Forms.TextBox textStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textAlbum;
        private System.Windows.Forms.TextBox textArtist;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureArtwork;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textGenre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboTrack;
        private System.Windows.Forms.ComboBox comboConversion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ColumnHeader columnFormat;
        private System.Windows.Forms.Button buttonArtwork;
    }
}

