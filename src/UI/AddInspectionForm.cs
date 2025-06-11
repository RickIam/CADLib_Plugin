using CADLib_Plugin_Kernel;
using CADLibKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        private readonly bool _isViewMode; // Флаг режима просмотра
        private List<int> _selectedDefectIds;

        public AddInspectionForm(IInspectionManager inspectionManager, IDefectManager defectManager, List<int> objectIds, int? inspectionId = null, bool isViewMode = false)
        {
            _inspectionManager = inspectionManager ?? throw new ArgumentNullException(nameof(inspectionManager));
            _defectManager = defectManager ?? throw new ArgumentNullException(nameof(defectManager));
            _objectIds = objectIds ?? new List<int>();
            _inspectionId = inspectionId;
            _isViewMode = isViewMode;
            _selectedDefectIds = new List<int>();
            InitializeComponent();

            // Устанавливаем стиль окна как изменяемый
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true; // Разрешаем максимализацию
            this.MinimizeBox = true; // Разрешаем минимизацию

            dateTimePickerInspectionDate.Value = DateTime.Now;
            dateTimePickerInspectionDate.Enabled = false;

            if (_isViewMode)
            {
                ConfigureViewMode();
            }
            else if (_inspectionId.HasValue)
            {
                ConfigureEditMode();
            }
            else
            {
                ConfigureAddMode();
            }

            LoadDefects();
        }

        private void ConfigureAddMode()
        {
            this.Text = "Добавить экспертизу";
            buttonSave.Visible = true;
            textBoxInspectorName.Enabled = true;
        }

        private void ConfigureEditMode()
        {
            // Этот метод больше не используется, но оставлен для совместимости
            LoadInspectionData();
            this.Text = "Редактировать экспертизу";
            buttonSave.Visible = true;
            textBoxInspectorName.Enabled = true;
        }

        private void ConfigureViewMode()
        {
            LoadInspectionData();
            this.Text = "Просмотр экспертизы";
            buttonSave.Visible = false;
            textBoxInspectorName.Enabled = false;
            dataGridViewDefects.Enabled = true;
            dataGridViewDefects.AllowUserToAddRows = false;
            dataGridViewDefects.AllowUserToDeleteRows = false;
            dataGridViewDefects.ReadOnly = true;

            // Убедимся, что прокрутка активна в таблице
            dataGridViewDefects.ScrollBars = ScrollBars.Both; // Включаем горизонтальную и вертикальную прокрутку
            dataGridViewDefects.AutoSize = false; // Отключаем автоматический размер
            dataGridViewDefects.AllowUserToResizeColumns = true; // Разрешаем изменение размера колонок
            dataGridViewDefects.AllowUserToResizeRows = false; // Запрещаем изменение размера строк
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
                DataTable defects = new DataTable();
                if (_isViewMode || _inspectionId.HasValue)
                {
                    if (_selectedDefectIds.Any())
                    {
                        using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
                        {
                            connection.Open();
                            string query = @"
                                SELECT d.Id, d.idObject, d.DefectNumber, d.Location, d.Description, d.DangerCategory, d.Document, d.Photo, d.Recommendation, d.InspectionId,
                                       o.Name AS ObjectName
                                FROM Defects d
                                LEFT JOIN ObjectsShadow o ON d.idObject = o.idObject
                                WHERE d.Id IN (" + string.Join(",", _selectedDefectIds) + ")";
                            using (var command = new SqlCommand(query, connection))
                            {
                                using (var adapter = new SqlDataAdapter(command))
                                {
                                    adapter.Fill(defects);
                                }
                            }
                        }

                        if (!defects.Columns.Contains("PhotoPreview"))
                            defects.Columns.Add("PhotoPreview", typeof(Image));
                        if (!defects.Columns.Contains("HasDocument"))
                            defects.Columns.Add("HasDocument", typeof(string));

                        using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
                        {
                            connection.Open();
                            foreach (DataRow row in defects.Rows)
                            {
                                int defectId = (int)row["Id"];

                                string photoQuery = "SELECT Photo FROM Defects WHERE Id = @Id";
                                using (var command = new SqlCommand(photoQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@Id", defectId);
                                    var photoData = command.ExecuteScalar() as byte[];
                                    if (photoData != null)
                                    {
                                        using (var ms = new MemoryStream(photoData))
                                        {
                                            Image originalImage = Image.FromStream(ms);
                                            Image thumbnail = originalImage.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                                            row["PhotoPreview"] = thumbnail;
                                        }
                                    }
                                    else
                                    {
                                        row["PhotoPreview"] = null;
                                    }
                                }

                                string documentQuery = "SELECT Document FROM Defects WHERE Id = @Id";
                                using (var command = new SqlCommand(documentQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@Id", defectId);
                                    var documentData = command.ExecuteScalar() as byte[];
                                    row["HasDocument"] = documentData != null ? "Да" : "Нет";
                                }
                            }
                        }

                        dataGridViewDefects.DataSource = defects;
                    }
                    else
                    {
                        dataGridViewDefects.DataSource = null;
                    }
                }
                else if (_objectIds == null || !_objectIds.Any())
                {
                    dataGridViewDefects.DataSource = null;
                    return;
                }
                else
                {
                    foreach (int idObject in _objectIds)
                    {
                        DataTable objectDefects = new DataTable();
                        using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
                        {
                            connection.Open();
                            string query = @"
                                SELECT d.Id, d.idObject, d.DefectNumber, d.Location, d.Description, d.DangerCategory, d.Document, d.Photo, d.Recommendation, d.InspectionId,
                                       o.Name AS ObjectName
                                FROM Defects d
                                LEFT JOIN ObjectsShadow o ON d.idObject = o.idObject
                                WHERE d.idObject = @idObject AND d.InspectionId IS NULL";
                            using (var command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@idObject", idObject);
                                using (var adapter = new SqlDataAdapter(command))
                                {
                                    adapter.Fill(objectDefects);
                                }
                            }
                        }

                        if (objectDefects != null && objectDefects.Rows.Count > 0)
                        {
                            defects.Merge(objectDefects);
                        }
                    }

                    if (!defects.Columns.Contains("PhotoPreview"))
                        defects.Columns.Add("PhotoPreview", typeof(Image));
                    if (!defects.Columns.Contains("HasDocument"))
                        defects.Columns.Add("HasDocument", typeof(string));

                    using (var connection = new SqlConnection(_defectManager.GetType().GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_defectManager).ToString()))
                    {
                        connection.Open();
                        foreach (DataRow row in defects.Rows)
                        {
                            int defectId = (int)row["Id"];

                            string photoQuery = "SELECT Photo FROM Defects WHERE Id = @Id";
                            using (var command = new SqlCommand(photoQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Id", defectId);
                                var photoData = command.ExecuteScalar() as byte[];
                                if (photoData != null)
                                {
                                    using (var ms = new MemoryStream(photoData))
                                    {
                                        Image originalImage = Image.FromStream(ms);
                                        Image thumbnail = originalImage.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                                        row["PhotoPreview"] = thumbnail;
                                    }
                                }
                                else
                                {
                                    row["PhotoPreview"] = null;
                                }
                            }

                            string documentQuery = "SELECT Document FROM Defects WHERE Id = @Id";
                            using (var command = new SqlCommand(documentQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Id", defectId);
                                var documentData = command.ExecuteScalar() as byte[];
                                row["HasDocument"] = documentData != null ? "Да" : "Нет";
                            }
                        }
                    }

                    dataGridViewDefects.DataSource = defects;
                }

                dataGridViewDefects.Columns["Id"].Visible = false;
                dataGridViewDefects.Columns["idObject"].Visible = false;
                dataGridViewDefects.Columns["Document"].Visible = false;
                dataGridViewDefects.Columns["Photo"].Visible = false;
                if (dataGridViewDefects.Columns.Contains("InspectionId"))
                    dataGridViewDefects.Columns["InspectionId"].Visible = false;

                if (dataGridViewDefects.Columns["Description"] != null)
                {
                    dataGridViewDefects.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridViewDefects.Columns["Recommendation"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridViewDefects.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }

                if (dataGridViewDefects.Columns["Description"] != null)
                {
                    dataGridViewDefects.Columns["Description"].MinimumWidth = 200;
                    dataGridViewDefects.Columns["Recommendation"].MinimumWidth = 200;
                }

                if (dataGridViewDefects.Columns["PhotoPreview"] != null)
                {
                    dataGridViewDefects.Columns["PhotoPreview"].HeaderText = "Фото";
                    dataGridViewDefects.Columns["PhotoPreview"].Width = 60;
                }

                if (dataGridViewDefects.Columns["ObjectName"] != null)
                {
                    dataGridViewDefects.Columns["ObjectName"].HeaderText = "Имя объекта";
                    dataGridViewDefects.Columns["ObjectName"].MinimumWidth = 150;
                    dataGridViewDefects.Columns["ObjectName"].DisplayIndex = 0;
                }

                dataGridViewDefects.Columns["DefectNumber"].HeaderText = "№ дефекта";
                dataGridViewDefects.Columns["Location"].HeaderText = "Местоположение";
                dataGridViewDefects.Columns["Description"].HeaderText = "Описание";
                dataGridViewDefects.Columns["DangerCategory"].HeaderText = "Категория опасности";
                dataGridViewDefects.Columns["Recommendation"].HeaderText = "Рекомендация";
                dataGridViewDefects.Columns["HasDocument"].HeaderText = "Документ загружен";

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
            if (_isViewMode) return; // В режиме просмотра не обновляем выбор

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
            if (_isViewMode) return; // В режиме просмотра кнопка неактивна

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