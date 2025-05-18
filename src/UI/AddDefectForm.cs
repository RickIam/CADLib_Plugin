using CADLib_Plugin_Kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_UI
{
    public partial class AddDefectForm : Form
    {
        private readonly IDefectManager _defectManager;
        private readonly int _idObject;
        private readonly int? _defectId; // Если редактирование, то содержит Id дефекта

        public AddDefectForm(IDefectManager defectManager, int idObject, int? defectId = null)
        {
            _defectManager = defectManager;
            _idObject = idObject;
            _defectId = defectId;
            InitializeComponent();

            if (_defectId.HasValue)
            {
                LoadDefectData();
                this.Text = "Редактировать дефект";
            }
            else
            {
                this.Text = "Добавить дефект";
            }
        }

        private void LoadDefectData()
        {
            var defect = _defectManager.GetDefectById(_defectId.Value);
            textBoxLocation.Text = defect.Location;
            textBoxDescription.Text = defect.Description;
            comboBoxDangerCategory.SelectedItem = defect.DangerCategory;
            textBoxRecommendation.Text = defect.Recommendation;
            // Документ и фото не загружаем в поля, только показываем, что они есть
            labelDocument.Text = defect.Document != null ? "Документ загружен" : "Документ не загружен";
            labelPhoto.Text = defect.Photo != null ? "Фото загружено" : "Фото не загружено";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLocation.Text))
            {
                MessageBox.Show("Укажите местоположение конструкции.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxDangerCategory.SelectedItem == null)
            {
                MessageBox.Show("Выберите категорию опасности.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var defect = new Defect
                {
                    Id = _defectId ?? 0,
                    IdObject = _idObject,
                    Location = textBoxLocation.Text,
                    Description = textBoxDescription.Text,
                    DangerCategory = comboBoxDangerCategory.SelectedItem.ToString(),
                    Recommendation = textBoxRecommendation.Text,
                    Document = labelDocument.Tag as byte[], // Может быть null
                    Photo = labelPhoto.Tag as byte[] // Может быть null
                };

                if (_defectId.HasValue)
                {
                    _defectManager.UpdateDefect(defect);
                }
                else
                {
                    _defectManager.AddDefect(defect);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении дефекта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUploadDocument_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Word Documents|*.docx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                        labelDocument.Text = "Документ загружен: " + Path.GetFileName(openFileDialog.FileName);
                        labelDocument.Tag = fileBytes; // Сохраняем байты в Tag
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке документа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonUploadPhoto_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                        labelPhoto.Text = "Фото загружено: " + Path.GetFileName(openFileDialog.FileName);
                        labelPhoto.Tag = fileBytes; // Сохраняем байты в Tag
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке фото: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
