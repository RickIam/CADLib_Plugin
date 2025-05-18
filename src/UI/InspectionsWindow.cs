using CADLib;
using CADLib_Plugin_Kernel;
using CADLibKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_UI
{
    public partial class InspectionsWindow : Form
    {
        private readonly IInspectionManager _inspectionManager;
        private readonly IDefectManager _defectManager;
        private readonly IDatabaseBrowser _mainDBBrowser;

        public InspectionsWindow(IInspectionManager inspectionManager, IDefectManager defectManager, IDatabaseBrowser mainDBBrowser)
        {
            _inspectionManager = inspectionManager ?? throw new ArgumentNullException(nameof(inspectionManager));
            _defectManager = defectManager ?? throw new ArgumentNullException(nameof(defectManager));
            _mainDBBrowser = mainDBBrowser; // Может быть null, проверка позже
            InitializeComponent();
            LoadInspections();
        }

        private void LoadInspections()
        {
            try
            {
                DataTable inspections = _inspectionManager.GetAllInspections();
                dataGridViewInspections.DataSource = inspections;

                dataGridViewInspections.Columns["Id"].Visible = false;

                dataGridViewInspections.Columns["InspectionDate"].HeaderText = "Дата экспертизы";
                dataGridViewInspections.Columns["InspectorName"].HeaderText = "Имя инспектора";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке экспертиз: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddInspection_Click(object sender, EventArgs e)
        {
            if (_mainDBBrowser == null)
            {
                MessageBox.Show("Нет доступа к выбору объектов. Проверьте настройки плагина.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<CLibObjectInfo> selectedObjects = _mainDBBrowser.GetSelectedObjects(false);
            if (selectedObjects == null || !selectedObjects.Any())
            {
                MessageBox.Show("Нет выбранных объектов в CadLib. Пожалуйста, выберите объекты перед созданием экспертизы.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Собираем все IdObject из выбранных объектов
            List<int> objectIds = selectedObjects.Select(obj => obj.idObject).ToList();

            using (var addInspectionForm = new AddInspectionForm(_inspectionManager, _defectManager, objectIds))
            {
                if (addInspectionForm.ShowDialog() == DialogResult.OK)
                {
                    LoadInspections();
                }
            }
        }

        private void buttonViewInspection_Click(object sender, EventArgs e)
        {
            if (dataGridViewInspections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите экспертизу для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int inspectionId = (int)dataGridViewInspections.SelectedRows[0].Cells["Id"].Value;
            using (var viewInspectionForm = new AddInspectionForm(_inspectionManager, _defectManager, new List<int>(), inspectionId, true)) // Передаём флаг isViewMode
            {
                viewInspectionForm.ShowDialog(); // Просто показываем форму в режиме просмотра
            }
        }

        private void buttonDeleteInspection_Click(object sender, EventArgs e)
        {
            if (dataGridViewInspections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите экспертизу для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int inspectionId = (int)dataGridViewInspections.SelectedRows[0].Cells["Id"].Value;
            if (MessageBox.Show("Вы уверены, что хотите удалить эту экспертизу?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _inspectionManager.DeleteInspection(inspectionId);
                    LoadInspections();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении экспертизы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}