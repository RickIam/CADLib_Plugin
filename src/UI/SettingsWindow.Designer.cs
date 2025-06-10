namespace CADLib_Plugin_UI
{
    partial class SettingsWindow
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
            this.labelDefects = new System.Windows.Forms.Label();
            this.labelDefectsStatus = new System.Windows.Forms.Label();
            this.labelInspections = new System.Windows.Forms.Label();
            this.labelInspectionsStatus = new System.Windows.Forms.Label();
            this.buttonCreateTables = new System.Windows.Forms.Button();
            this.buttonAddParameters = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDefects
            // 
            this.labelDefects.AutoSize = true;
            this.labelDefects.Location = new System.Drawing.Point(20, 20);
            this.labelDefects.Name = "labelDefects";
            this.labelDefects.Size = new System.Drawing.Size(93, 13);
            this.labelDefects.TabIndex = 0;
            this.labelDefects.Text = "Таблица Defects:";
            // 
            // labelDefectsStatus
            // 
            this.labelDefectsStatus.AutoSize = true;
            this.labelDefectsStatus.Location = new System.Drawing.Point(137, 20);
            this.labelDefectsStatus.Name = "labelDefectsStatus";
            this.labelDefectsStatus.Size = new System.Drawing.Size(66, 13);
            this.labelDefectsStatus.TabIndex = 1;
            this.labelDefectsStatus.Text = "Проверка...";
            // 
            // labelInspections
            // 
            this.labelInspections.AutoSize = true;
            this.labelInspections.Location = new System.Drawing.Point(20, 50);
            this.labelInspections.Name = "labelInspections";
            this.labelInspections.Size = new System.Drawing.Size(110, 13);
            this.labelInspections.TabIndex = 2;
            this.labelInspections.Text = "Таблица Inspections:";
            // 
            // labelInspectionsStatus
            // 
            this.labelInspectionsStatus.AutoSize = true;
            this.labelInspectionsStatus.Location = new System.Drawing.Point(137, 50);
            this.labelInspectionsStatus.Name = "labelInspectionsStatus";
            this.labelInspectionsStatus.Size = new System.Drawing.Size(66, 13);
            this.labelInspectionsStatus.TabIndex = 3;
            this.labelInspectionsStatus.Text = "Проверка...";
            // 
            // buttonCreateTables
            // 
            this.buttonCreateTables.Location = new System.Drawing.Point(20, 80);
            this.buttonCreateTables.Name = "buttonCreateTables";
            this.buttonCreateTables.Size = new System.Drawing.Size(120, 23);
            this.buttonCreateTables.TabIndex = 4;
            this.buttonCreateTables.Text = "Создать таблицы";
            this.buttonCreateTables.UseVisualStyleBackColor = true;
            this.buttonCreateTables.Click += new System.EventHandler(this.buttonCreateTables_Click);
            // 
            // buttonAddParameters
            // 
            this.buttonAddParameters.Location = new System.Drawing.Point(150, 80);
            this.buttonAddParameters.Name = "buttonAddParameters";
            this.buttonAddParameters.Size = new System.Drawing.Size(150, 23);
            this.buttonAddParameters.TabIndex = 5;
            this.buttonAddParameters.Text = "Подготовить для Web";
            this.buttonAddParameters.UseVisualStyleBackColor = true;
            this.buttonAddParameters.Click += new System.EventHandler(this.buttonAddParameters_Click);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.buttonAddParameters);
            this.Controls.Add(this.buttonCreateTables);
            this.Controls.Add(this.labelInspectionsStatus);
            this.Controls.Add(this.labelInspections);
            this.Controls.Add(this.labelDefectsStatus);
            this.Controls.Add(this.labelDefects);
            this.Name = "SettingsWindow";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDefects;
        private System.Windows.Forms.Label labelDefectsStatus;
        private System.Windows.Forms.Label labelInspections;
        private System.Windows.Forms.Label labelInspectionsStatus;
        private System.Windows.Forms.Button buttonCreateTables;
        private System.Windows.Forms.Button buttonAddParameters;
    }
}