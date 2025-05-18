using System.Windows.Forms;

namespace CADLib_Plugin_UI
{
    partial class AddInspectionForm
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
            this.labelInspectionDate = new System.Windows.Forms.Label();
            this.dateTimePickerInspectionDate = new System.Windows.Forms.DateTimePicker();
            this.labelInspectorName = new System.Windows.Forms.Label();
            this.textBoxInspectorName = new System.Windows.Forms.TextBox();
            this.dataGridViewDefects = new System.Windows.Forms.DataGridView();
            this.labelSelectedDefects = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInspectionDate
            // 
            this.labelInspectionDate.AutoSize = true;
            this.labelInspectionDate.Location = new System.Drawing.Point(20, 20);
            this.labelInspectionDate.Name = "labelInspectionDate";
            this.labelInspectionDate.Size = new System.Drawing.Size(100, 13);
            this.labelInspectionDate.TabIndex = 0;
            this.labelInspectionDate.Text = "Дата экспертизы:";
            // 
            // dateTimePickerInspectionDate
            // 
            this.dateTimePickerInspectionDate.Location = new System.Drawing.Point(120, 20);
            this.dateTimePickerInspectionDate.Name = "dateTimePickerInspectionDate";
            this.dateTimePickerInspectionDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerInspectionDate.TabIndex = 1;
            // 
            // labelInspectorName
            // 
            this.labelInspectorName.AutoSize = true;
            this.labelInspectorName.Location = new System.Drawing.Point(20, 50);
            this.labelInspectorName.Name = "labelInspectorName";
            this.labelInspectorName.Size = new System.Drawing.Size(94, 13);
            this.labelInspectorName.TabIndex = 2;
            this.labelInspectorName.Text = "Имя инспектора:";
            // 
            // textBoxInspectorName
            // 
            this.textBoxInspectorName.Location = new System.Drawing.Point(120, 50);
            this.textBoxInspectorName.Name = "textBoxInspectorName";
            this.textBoxInspectorName.Size = new System.Drawing.Size(200, 20);
            this.textBoxInspectorName.TabIndex = 3;
            // 
            // dataGridViewDefects
            // 
            this.dataGridViewDefects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDefects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDefects.Location = new System.Drawing.Point(20, 102);
            this.dataGridViewDefects.Name = "dataGridViewDefects";
            this.dataGridViewDefects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDefects.Size = new System.Drawing.Size(660, 236);
            this.dataGridViewDefects.TabIndex = 7;
            this.dataGridViewDefects.SelectionChanged += new System.EventHandler(this.dataGridViewDefects_SelectionChanged);
            // 
            // labelSelectedDefects
            // 
            this.labelSelectedDefects.AutoSize = true;
            this.labelSelectedDefects.Location = new System.Drawing.Point(20, 82);
            this.labelSelectedDefects.Name = "labelSelectedDefects";
            this.labelSelectedDefects.Size = new System.Drawing.Size(116, 13);
            this.labelSelectedDefects.TabIndex = 8;
            this.labelSelectedDefects.Text = "Выбрано дефектов: 0";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(560, 344);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AddInspectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 381);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelSelectedDefects);
            this.Controls.Add(this.dataGridViewDefects);
            this.Controls.Add(this.textBoxInspectorName);
            this.Controls.Add(this.labelInspectorName);
            this.Controls.Add(this.dateTimePickerInspectionDate);
            this.Controls.Add(this.labelInspectionDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 420);
            this.Name = "AddInspectionForm";
            this.Text = "Добавить экспертизу";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInspectionDate;
        private System.Windows.Forms.Label labelInspectorName;
        private System.Windows.Forms.Label labelSelectedDefects;
        private System.Windows.Forms.DateTimePicker dateTimePickerInspectionDate;
        private System.Windows.Forms.TextBox textBoxInspectorName;
        private System.Windows.Forms.DataGridView dataGridViewDefects;
        private System.Windows.Forms.Button buttonSave;
    }
}