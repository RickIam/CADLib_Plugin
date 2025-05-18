using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CADLib_Plugin_Kernel;

namespace CADLib_Plugin_UI
{
    public partial class DefectsWindow : Form
    {
        private readonly IDefectManager _defectManager;
        private readonly int _idObject;

        public DefectsWindow(IDefectManager defectManager, int idObject)
        {
            _defectManager = defectManager ?? throw new ArgumentNullException(nameof(defectManager));
            _idObject = idObject;
            InitializeComponent();
            LoadDefects();
        }

        private void LoadDefects()
        {
            try
            {
                DataTable defects = _defectManager.GetDefectsByObject(_idObject);

                // Добавляем пользовательские столбцы в DataTable
                if (!defects.Columns.Contains("PhotoPreview"))
                    defects.Columns.Add("PhotoPreview", typeof(Image)); // Столбец для отображения фото
                if (!defects.Columns.Contains("HasDocument"))
                    defects.Columns.Add("HasDocument", typeof(string));

                // Заполняем данные и изображения
                foreach (DataRow row in defects.Rows)
                {
                    int defectId = (int)row["Id"];
                    byte[] photoData = _defectManager.GetPhoto(defectId);
                    if (photoData != null)
                    {
                        using (var ms = new MemoryStream(photoData))
                        {
                            Image originalImage = Image.FromStream(ms);
                            // Масштабируем изображение для отображения в таблице (например, 50x50 пикселей)
                            Image thumbnail = originalImage.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                            row["PhotoPreview"] = thumbnail;
                        }
                    }
                    else
                    {
                        row["PhotoPreview"] = null; // Если фото нет, оставляем пустым
                    }
                    row["HasDocument"] = _defectManager.GetDocument(defectId) != null ? "Да" : "Нет";
                }
                dataGridViewDefects.DataSource = defects;

                // Настраиваем видимые столбцы
                dataGridViewDefects.Columns["Id"].Visible = false; // Скрываем Id
                dataGridViewDefects.Columns["idObject"].Visible = false; // Скрываем IdObject
                dataGridViewDefects.Columns["Document"].Visible = false; // Скрываем двоичные данные
                dataGridViewDefects.Columns["Photo"].Visible = false; // Скрываем двоичные данные
                dataGridViewDefects.Columns["InspectionId"].Visible = false;

                // Настраиваем колонку Description для переноса текста
                if (dataGridViewDefects.Columns["Description"] != null)
                {
                    dataGridViewDefects.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridViewDefects.Columns["Recommendation"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridViewDefects.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Автоматическая подстройка высоты строк
                }

                // Устанавливаем минимальную ширину для колонки Description
                if (dataGridViewDefects.Columns["Description"] != null)
                {
                    dataGridViewDefects.Columns["Description"].MinimumWidth = 200; // Минимальная ширина для отображения текста
                    dataGridViewDefects.Columns["Recommendation"].MinimumWidth = 200;
                }

                // Настраиваем колонку PhotoPreview
                if (dataGridViewDefects.Columns["PhotoPreview"] != null)
                {
                    dataGridViewDefects.Columns["PhotoPreview"].HeaderText = "Фото";
                    dataGridViewDefects.Columns["PhotoPreview"].Width = 60; // Устанавливаем ширину для колонки с фото
                }

                dataGridViewDefects.Columns["DefectNumber"].HeaderText = "№ дефекта";
                dataGridViewDefects.Columns["Location"].HeaderText = "Местоположение";
                dataGridViewDefects.Columns["Description"].HeaderText = "Описание";
                dataGridViewDefects.Columns["DangerCategory"].HeaderText = "Категория опасности";
                dataGridViewDefects.Columns["Recommendation"].HeaderText = "Рекомендация";
                dataGridViewDefects.Columns["HasDocument"].HeaderText = "Документ загружен";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке дефектов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddDefect_Click(object sender, EventArgs e)
        {
            using (var addDefectForm = new AddDefectForm(_defectManager, _idObject))
            {
                if (addDefectForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDefects(); // Обновляем список после добавления
                }
            }
        }

        private void buttonEditDefect_Click(object sender, EventArgs e)
        {
            if (dataGridViewDefects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите дефект для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int defectId = (int)dataGridViewDefects.SelectedRows[0].Cells["Id"].Value;
            using (var editDefectForm = new AddDefectForm(_defectManager, _idObject, defectId))
            {
                if (editDefectForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDefects(); // Обновляем список после редактирования
                }
            }
        }

        private void buttonDeleteDefect_Click(object sender, EventArgs e)
        {
            if (dataGridViewDefects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите дефект для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int defectId = (int)dataGridViewDefects.SelectedRows[0].Cells["Id"].Value;
            if (MessageBox.Show("Вы уверены, что хотите удалить этот дефект?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _defectManager.DeleteDefect(defectId);
                    LoadDefects(); // Обновляем список после удаления
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении дефекта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonViewPhoto_Click(object sender, EventArgs e)
        {
            if (dataGridViewDefects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите дефект для просмотра фото.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int defectId = (int)dataGridViewDefects.SelectedRows[0].Cells["Id"].Value;
            try
            {
                byte[] photoData = _defectManager.GetPhoto(defectId);
                if (photoData == null)
                {
                    MessageBox.Show("Фото не загружено для этого дефекта.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tempFilePath = Path.Combine(Path.GetTempPath(), $"defect_photo_{defectId}.jpg");
                File.WriteAllBytes(tempFilePath, photoData);
                Process.Start(new ProcessStartInfo
                {
                    FileName = tempFilePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при просмотре фото: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDownloadDocument_Click(object sender, EventArgs e)
        {
            if (dataGridViewDefects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите дефект для скачивания документа.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int defectId = (int)dataGridViewDefects.SelectedRows[0].Cells["Id"].Value;
            try
            {
                byte[] documentData = _defectManager.GetDocument(defectId);
                if (documentData == null)
                {
                    MessageBox.Show("Документ не загружен для этого дефекта.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Documents|*.docx";
                    saveFileDialog.FileName = $"defect_document_{defectId}.docx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, documentData);
                        MessageBox.Show("Документ успешно сохранен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при скачивании документа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}