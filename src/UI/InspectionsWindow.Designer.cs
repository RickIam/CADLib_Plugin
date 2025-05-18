namespace CADLib_Plugin_UI
{
    partial class InspectionsWindow
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
            this.dataGridViewInspections = new System.Windows.Forms.DataGridView();
            this.buttonAddInspection = new System.Windows.Forms.Button();
            this.buttonEditInspection = new System.Windows.Forms.Button();
            this.buttonDeleteInspection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInspections)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewInspections
            // 
            this.dataGridViewInspections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInspections.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewInspections.Name = "dataGridViewInspections";
            this.dataGridViewInspections.Size = new System.Drawing.Size(760, 300);
            this.dataGridViewInspections.TabIndex = 0;
            this.dataGridViewInspections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // buttonAddInspection
            // 
            this.buttonAddInspection.Location = new System.Drawing.Point(20, 330);
            this.buttonAddInspection.Name = "buttonAddInspection";
            this.buttonAddInspection.Size = new System.Drawing.Size(120, 23);
            this.buttonAddInspection.TabIndex = 1;
            this.buttonAddInspection.Text = "Добавить экспертизу";
            this.buttonAddInspection.UseVisualStyleBackColor = true;
            this.buttonAddInspection.Click += new System.EventHandler(this.buttonAddInspection_Click);
            this.buttonAddInspection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // buttonEditInspection
            // 
            this.buttonEditInspection.Location = new System.Drawing.Point(150, 330);
            this.buttonEditInspection.Name = "buttonEditInspection";
            this.buttonEditInspection.Size = new System.Drawing.Size(120, 23);
            this.buttonEditInspection.TabIndex = 2;
            this.buttonEditInspection.Text = "Редактировать";
            this.buttonEditInspection.UseVisualStyleBackColor = true;
            this.buttonEditInspection.Click += new System.EventHandler(this.buttonEditInspection_Click);
            this.buttonEditInspection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // buttonDeleteInspection
            // 
            this.buttonDeleteInspection.Location = new System.Drawing.Point(280, 330);
            this.buttonDeleteInspection.Name = "buttonDeleteInspection";
            this.buttonDeleteInspection.Size = new System.Drawing.Size(120, 23);
            this.buttonDeleteInspection.TabIndex = 3;
            this.buttonDeleteInspection.Text = "Удалить";
            this.buttonDeleteInspection.UseVisualStyleBackColor = true;
            this.buttonDeleteInspection.Click += new System.EventHandler(this.buttonDeleteInspection_Click);
            this.buttonDeleteInspection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // InspectionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 370);
            this.Controls.Add(this.buttonDeleteInspection);
            this.Controls.Add(this.buttonEditInspection);
            this.Controls.Add(this.buttonAddInspection);
            this.Controls.Add(this.dataGridViewInspections);
            this.Name = "InspectionsWindow";
            this.Text = "Экспертизы";
            this.MinimumSize = new System.Drawing.Size(600, 300);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInspections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewInspections;
        private System.Windows.Forms.Button buttonAddInspection;
        private System.Windows.Forms.Button buttonEditInspection;
        private System.Windows.Forms.Button buttonDeleteInspection;
    }
}