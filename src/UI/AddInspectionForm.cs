using CADLib_Plugin_Kernel;
using CADLibKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_UI
{
    public partial class AddInspectionForm : Form
    {
        private readonly IInspectionManager _inspectionManager;
        private readonly IDefectManager _defectManager;
        private readonly List<int> _objectIds;
        private readonly int? _inspectionId;
        private List<int> _selectedDefectIds;

        public AddInspectionForm(IInspectionManager inspectionManager, IDefectManager defectManager, List<int> objectIds, int? inspectionId = null)
        {
            _inspectionManager = inspectionManager ?? throw new ArgumentNullException(nameof(inspectionManager));
            _defectManager = defectManager ?? throw new ArgumentNullException(nameof(defectManager));
            _objectIds = objectIds ?? throw new ArgumentNullException(nameof(objectIds));
            _inspectionId = inspectionId;
            _selectedDefectIds = new List<int>();
            InitializeComponent();

            dateTimePickerInspectionDate.Value = DateTime.Now; // Устанавливаем текущую дату
            dateTimePickerInspectionDate.Enabled = false; // Делаем поле только для чтения

            if (_inspectionId.HasValue)
            {
                LoadInspectionData();
                this.Text = "Редактировать экспертизу";
            }
            else
            {
                this.Text = "Добавить экспертизу";
            }

            LoadDefects();
        }

        private void LoadInspectionData()
        {
            var inspection = _inspectionManager.GetInspectionById(_inspectionId.Value);
            dateTimePickerInspectionDate.Value = DateTime.Parse(inspection.InspectionDate);
            textBoxInspectorName.Text = inspection.InspectorName;

            // Загружаем дефекты, связанные с этой экспертизой
            using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
            {
                connection.Open();
                string query = "SELECT Id FROM Defects WHERE InspectionId = @InspectionId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InspectionId", _inspectionId.Value);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _selectedDefectIds.Add((int)reader["Id"]);
                        }
                    }
                }
            }
            UpdateSelectedDefectsLabel();
        }

        private void LoadDefects()
        {
            try
            {
                if (_objectIds == null || !_objectIds.Any())
                {
                    MessageBox.Show("Нет выбранных объектов в CadLib. Пожалуйста, выберите объекты перед созданием экспертизы.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем все дефекты для выбранных объектов
                DataTable defects = new DataTable();
                foreach (int idObject in _objectIds)
                {
                    DataTable objectDefects = _defectManager.GetDefectsByObject(idObject);
                    if (objectDefects != null && objectDefects.Rows.Count > 0)
                    {
                        defects.Merge(objectDefects);
                    }
                }

                dataGridViewDefects.DataSource = defects;

                // Проверяем наличие колонок перед настройкой
                if (dataGridViewDefects.Columns.Contains("Id")) dataGridViewDefects.Columns["Id"].Visible = false;
                if (dataGridViewDefects.Columns.Contains("IdObject")) dataGridViewDefects.Columns["IdObject"].Visible = false;
                if (dataGridViewDefects.Columns.Contains("Document")) dataGridViewDefects.Columns["Document"].Visible = false;
                if (dataGridViewDefects.Columns.Contains("Photo")) dataGridViewDefects.Columns["Photo"].Visible = false;

                if (dataGridViewDefects.Columns.Contains("DefectNumber")) dataGridViewDefects.Columns["DefectNumber"].HeaderText = "№ дефекта";
                if (dataGridViewDefects.Columns.Contains("Location")) dataGridViewDefects.Columns["Location"].HeaderText = "Местоположение";
                if (dataGridViewDefects.Columns.Contains("Description")) dataGridViewDefects.Columns["Description"].HeaderText = "Описание";
                if (dataGridViewDefects.Columns.Contains("DangerCategory")) dataGridViewDefects.Columns["DangerCategory"].HeaderText = "Категория опасности";
                if (dataGridViewDefects.Columns.Contains("Recommendation")) dataGridViewDefects.Columns["Recommendation"].HeaderText = "Рекомендация";

                // Восстанавливаем выбор дефектов при редактировании
                if (dataGridViewDefects.Rows != null)
                {
                    foreach (DataGridViewRow row in dataGridViewDefects.Rows)
                    {
                        if (row.Cells["Id"] != null && row.Cells["Id"].Value != DBNull.Value)
                        {
                            int defectId = Convert.ToInt32(row.Cells["Id"].Value);
                            row.Selected = _selectedDefectIds.Contains(defectId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке дефектов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDefects_SelectionChanged(object sender, EventArgs e)
        {
            _selectedDefectIds.Clear();
            if (dataGridViewDefects.SelectedRows != null)
            {
                foreach (DataGridViewRow row in dataGridViewDefects.SelectedRows)
                {
                    if (row.Cells["Id"] != null && row.Cells["Id"].Value != DBNull.Value)
                    {
                        int defectId = Convert.ToInt32(row.Cells["Id"].Value);
                        _selectedDefectIds.Add(defectId);
                    }
                }
            }
            UpdateSelectedDefectsLabel();
        }

        private void UpdateSelectedDefectsLabel()
        {
            labelSelectedDefects.Text = $"Выбрано дефектов: {_selectedDefectIds.Count}";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxInspectorName.Text))
            {
                MessageBox.Show("Укажите имя инспектора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var inspection = new Inspection
                {
                    Id = _inspectionId ?? 0,
                    InspectionDate = dateTimePickerInspectionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    InspectorName = textBoxInspectorName.Text
                };

                if (_inspectionId.HasValue)
                {
                    _inspectionManager.UpdateInspection(inspection);
                    // Обновляем InspectionId для всех выбранных дефектов
                    UpdateDefectInspectionIds(_inspectionId.Value);
                }
                else
                {
                    _inspectionManager.AddInspection(inspection);
                    int newInspectionId = GetLastInsertedInspectionId();
                    UpdateDefectInspectionIds(newInspectionId);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении экспертизы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetLastInsertedInspectionId()
        {
            using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
            {
                connection.Open();
                string query = "SELECT MAX(Id) FROM Inspections";
                using (var command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? (int)result : 0;
                }
            }
        }

        private void UpdateDefectInspectionIds(int inspectionId)
        {
            using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
            {
                connection.Open();
                string query = "UPDATE Defects SET InspectionId = @InspectionId WHERE Id IN (" + string.Join(",", _selectedDefectIds) + ")";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InspectionId", inspectionId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}