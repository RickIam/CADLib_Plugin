namespace CADLib_Plugin_UI
{
    partial class AddDefectForm
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
            this.labelLocation = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDangerCategory = new System.Windows.Forms.Label();
            this.comboBoxDangerCategory = new System.Windows.Forms.ComboBox();
            this.labelRecommendation = new System.Windows.Forms.Label();
            this.textBoxRecommendation = new System.Windows.Forms.TextBox();
            this.labelDocument = new System.Windows.Forms.Label();
            this.buttonUploadDocument = new System.Windows.Forms.Button();
            this.labelPhoto = new System.Windows.Forms.Label();
            this.buttonUploadPhoto = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(20, 20);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(94, 13);
            this.labelLocation.TabIndex = 0;
            this.labelLocation.Text = "Местоположение:";
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(120, 20);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(260, 20);
            this.textBoxLocation.TabIndex = 1;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(20, 50);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Описание:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(120, 50);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(260, 60);
            this.textBoxDescription.TabIndex = 3;
            // 
            // labelDangerCategory
            // 
            this.labelDangerCategory.AutoSize = true;
            this.labelDangerCategory.Location = new System.Drawing.Point(20, 120);
            this.labelDangerCategory.Name = "labelDangerCategory";
            this.labelDangerCategory.Size = new System.Drawing.Size(94, 13);
            this.labelDangerCategory.TabIndex = 4;
            this.labelDangerCategory.Text = "Категория опасности:";
            // 
            // comboBoxDangerCategory
            // 
            this.comboBoxDangerCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDangerCategory.Items.AddRange(new object[] { "А", "Б", "В" });
            this.comboBoxDangerCategory.Location = new System.Drawing.Point(120, 120);
            this.comboBoxDangerCategory.Name = "comboBoxDangerCategory";
            this.comboBoxDangerCategory.Size = new System.Drawing.Size(50, 21);
            this.comboBoxDangerCategory.TabIndex = 5;
            // 
            // labelRecommendation
            // 
            this.labelRecommendation.AutoSize = true;
            this.labelRecommendation.Location = new System.Drawing.Point(20, 150);
            this.labelRecommendation.Name = "labelRecommendation";
            this.labelRecommendation.Size = new System.Drawing.Size(94, 13);
            this.labelRecommendation.TabIndex = 6;
            this.labelRecommendation.Text = "Рекомендация:";
            // 
            // textBoxRecommendation
            // 
            this.textBoxRecommendation.Location = new System.Drawing.Point(120, 150);
            this.textBoxRecommendation.Multiline = true;
            this.textBoxRecommendation.Name = "textBoxRecommendation";
            this.textBoxRecommendation.Size = new System.Drawing.Size(260, 60);
            this.textBoxRecommendation.TabIndex = 7;
            // 
            // labelDocument
            // 
            this.labelDocument.AutoSize = true;
            this.labelDocument.Location = new System.Drawing.Point(20, 220);
            this.labelDocument.Name = "labelDocument";
            this.labelDocument.Size = new System.Drawing.Size(94, 13);
            this.labelDocument.TabIndex = 8;
            this.labelDocument.Text = "Документ не загружен";
            // 
            // buttonUploadDocument
            // 
            this.buttonUploadDocument.Location = new System.Drawing.Point(120, 220);
            this.buttonUploadDocument.Name = "buttonUploadDocument";
            this.buttonUploadDocument.Size = new System.Drawing.Size(120, 23);
            this.buttonUploadDocument.TabIndex = 9;
            this.buttonUploadDocument.Text = "Загрузить документ";
            this.buttonUploadDocument.UseVisualStyleBackColor = true;
            this.buttonUploadDocument.Click += new System.EventHandler(this.buttonUploadDocument_Click);
            // 
            // labelPhoto
            // 
            this.labelPhoto.AutoSize = true;
            this.labelPhoto.Location = new System.Drawing.Point(20, 250);
            this.labelPhoto.Name = "labelPhoto";
            this.labelPhoto.Size = new System.Drawing.Size(94, 13);
            this.labelPhoto.TabIndex = 10;
            this.labelPhoto.Text = "Фото не загружено";
            // 
            // buttonUploadPhoto
            // 
            this.buttonUploadPhoto.Location = new System.Drawing.Point(120, 250);
            this.buttonUploadPhoto.Name = "buttonUploadPhoto";
            this.buttonUploadPhoto.Size = new System.Drawing.Size(120, 23);
            this.buttonUploadPhoto.TabIndex = 11;
            this.buttonUploadPhoto.Text = "Загрузить фото";
            this.buttonUploadPhoto.UseVisualStyleBackColor = true;
            this.buttonUploadPhoto.Click += new System.EventHandler(this.buttonUploadPhoto_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(260, 280);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AddDefectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonUploadPhoto);
            this.Controls.Add(this.labelPhoto);
            this.Controls.Add(this.buttonUploadDocument);
            this.Controls.Add(this.labelDocument);
            this.Controls.Add(this.textBoxRecommendation);
            this.Controls.Add(this.labelRecommendation);
            this.Controls.Add(this.comboBoxDangerCategory);
            this.Controls.Add(this.labelDangerCategory);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.labelLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDefectForm";
            this.Text = "Добавить дефект";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelDangerCategory;
        private System.Windows.Forms.Label labelRecommendation;
        private System.Windows.Forms.Label labelDocument;
        private System.Windows.Forms.Label labelPhoto;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxRecommendation;
        private System.Windows.Forms.ComboBox comboBoxDangerCategory;
        private System.Windows.Forms.Button buttonUploadDocument;
        private System.Windows.Forms.Button buttonUploadPhoto;
        private System.Windows.Forms.Button buttonSave;
    }
}