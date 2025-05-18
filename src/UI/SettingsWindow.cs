using CADLib_Plugin_Kernel;
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
    public partial class SettingsWindow : Form
    {
        private readonly IDatabaseInitializer _dbInitializer;

        public SettingsWindow(IDatabaseInitializer dbInitializer)
        {
            _dbInitializer = dbInitializer;
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            CheckTableStatus();
        }

        private void CheckTableStatus()
        {
            try
            {
                _dbInitializer.CheckTablesExist(out bool defectsExists, out bool inspectionsExists);

                labelDefectsStatus.Text = defectsExists ? "Существует" : "Не существует";
                labelDefectsStatus.ForeColor = defectsExists ? Color.Green : Color.Red;

                labelInspectionsStatus.Text = inspectionsExists ? "Существует" : "Не существует";
                labelInspectionsStatus.ForeColor = inspectionsExists ? Color.Green : Color.Red;

                buttonCreateTables.Enabled = !defectsExists || !inspectionsExists;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreateTables_Click(object sender, EventArgs e)
        {
            try
            {
                _dbInitializer.CreateTables();
                MessageBox.Show("Таблицы успешно созданы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CheckTableStatus(); // Обновляем статус после создания
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
