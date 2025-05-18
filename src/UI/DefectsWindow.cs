using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                dataGridViewDefects.DataSource = defects;
                dataGridViewDefects.Columns["Id"].Visible = false; // Скрываем Id
                dataGridViewDefects.Columns["idObject"].Visible = false; // Скрываем IdObject
                dataGridViewDefects.Columns["Document"].Visible = false; // Скрываем двоичные данные
                dataGridViewDefects.Columns["Photo"].Visible = false; // Скрываем двоичные данные
                dataGridViewDefects.Columns["DefectNumber"].HeaderText = "№ дефекта";
                dataGridViewDefects.Columns["Location"].HeaderText = "Местоположение";
                dataGridViewDefects.Columns["Description"].HeaderText = "Описание";
                dataGridViewDefects.Columns["DangerCategory"].HeaderText = "Категория опасности";
                dataGridViewDefects.Columns["Recommendation"].HeaderText = "Рекомендация";
                dataGridViewDefects.Columns["InspectionId"].HeaderText = "ID экспертизы";
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
    }
}