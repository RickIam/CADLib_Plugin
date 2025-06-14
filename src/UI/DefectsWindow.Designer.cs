﻿namespace CADLib_Plugin_UI
{
    partial class DefectsWindow
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
            this.dataGridViewDefects = new System.Windows.Forms.DataGridView();
            this.buttonAddDefect = new System.Windows.Forms.Button();
            this.buttonEditDefect = new System.Windows.Forms.Button();
            this.buttonDeleteDefect = new System.Windows.Forms.Button();
            this.buttonViewPhoto = new System.Windows.Forms.Button();
            this.buttonDownloadDocument = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDefects
            // 
            this.dataGridViewDefects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDefects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDefects.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewDefects.Name = "dataGridViewDefects";
            this.dataGridViewDefects.Size = new System.Drawing.Size(760, 300);
            this.dataGridViewDefects.TabIndex = 0;
            // 
            // buttonAddDefect
            // 
            this.buttonAddDefect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddDefect.Location = new System.Drawing.Point(20, 330);
            this.buttonAddDefect.Name = "buttonAddDefect";
            this.buttonAddDefect.Size = new System.Drawing.Size(120, 23);
            this.buttonAddDefect.TabIndex = 1;
            this.buttonAddDefect.Text = "Добавить дефект";
            this.buttonAddDefect.UseVisualStyleBackColor = true;
            this.buttonAddDefect.Click += new System.EventHandler(this.buttonAddDefect_Click);
            // 
            // buttonEditDefect
            // 
            this.buttonEditDefect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditDefect.Location = new System.Drawing.Point(150, 330);
            this.buttonEditDefect.Name = "buttonEditDefect";
            this.buttonEditDefect.Size = new System.Drawing.Size(120, 23);
            this.buttonEditDefect.TabIndex = 2;
            this.buttonEditDefect.Text = "Редактировать дефект";
            this.buttonEditDefect.UseVisualStyleBackColor = true;
            this.buttonEditDefect.Click += new System.EventHandler(this.buttonEditDefect_Click);
            // 
            // buttonDeleteDefect
            // 
            this.buttonDeleteDefect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteDefect.Location = new System.Drawing.Point(280, 330);
            this.buttonDeleteDefect.Name = "buttonDeleteDefect";
            this.buttonDeleteDefect.Size = new System.Drawing.Size(120, 23);
            this.buttonDeleteDefect.TabIndex = 3;
            this.buttonDeleteDefect.Text = "Удалить дефект";
            this.buttonDeleteDefect.UseVisualStyleBackColor = true;
            this.buttonDeleteDefect.Click += new System.EventHandler(this.buttonDeleteDefect_Click);
            // 
            // buttonViewPhoto
            // 
            this.buttonViewPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonViewPhoto.Location = new System.Drawing.Point(410, 330);
            this.buttonViewPhoto.Name = "buttonViewPhoto";
            this.buttonViewPhoto.Size = new System.Drawing.Size(120, 23);
            this.buttonViewPhoto.TabIndex = 4;
            this.buttonViewPhoto.Text = "Просмотреть фото";
            this.buttonViewPhoto.UseVisualStyleBackColor = true;
            this.buttonViewPhoto.Click += new System.EventHandler(this.buttonViewPhoto_Click);
            // 
            // buttonDownloadDocument
            // 
            this.buttonDownloadDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDownloadDocument.Location = new System.Drawing.Point(540, 330);
            this.buttonDownloadDocument.Name = "buttonDownloadDocument";
            this.buttonDownloadDocument.Size = new System.Drawing.Size(120, 23);
            this.buttonDownloadDocument.TabIndex = 5;
            this.buttonDownloadDocument.Text = "Скачать документ";
            this.buttonDownloadDocument.UseVisualStyleBackColor = true;
            this.buttonDownloadDocument.Click += new System.EventHandler(this.buttonDownloadDocument_Click);
            // 
            // DefectsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 370);
            this.Controls.Add(this.buttonDownloadDocument);
            this.Controls.Add(this.buttonViewPhoto);
            this.Controls.Add(this.buttonDeleteDefect);
            this.Controls.Add(this.buttonEditDefect);
            this.Controls.Add(this.buttonAddDefect);
            this.Controls.Add(this.dataGridViewDefects);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "DefectsWindow";
            this.Text = "Дефекты объекта";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDefects;
        private System.Windows.Forms.Button buttonAddDefect;
        private System.Windows.Forms.Button buttonEditDefect;
        private System.Windows.Forms.Button buttonDeleteDefect;
        private System.Windows.Forms.Button buttonViewPhoto;
        private System.Windows.Forms.Button buttonDownloadDocument;
    }
}