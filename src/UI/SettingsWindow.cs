using CADLib;
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
    public partial class SettingsWindow : Form
    {
        private readonly IDatabaseInitializer _dbInitializer;
        private readonly CADLibrary _library;

        public SettingsWindow(IDatabaseInitializer dbInitializer, CADLibrary m_library)
        {
            _dbInitializer = dbInitializer;
            _library = m_library;
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


        private void buttonAddParameters_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем строку подключения из _dbInitializer
                string connectionString = (_dbInitializer as DatabaseInitializer)?.GetType()
                    .GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(_dbInitializer) as string
                    ?? throw new InvalidOperationException("Не удалось получить строку подключения.");

                // Собираем все idObject из таблицы ObjectsShadow
                List<int> idObj = new List<int>();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT idObject FROM ObjectsShadow";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idObj.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    connection.Close();
                }

                if (idObj.Count == 0)
                {
                    MessageBox.Show("В таблице ObjectsShadow нет объектов.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем список объектов
                List<CLibObjectInfo> list = _library.GetLibraryObjects(idObj);
                var param = _library.GetParamDef("cdeId");
                if (param == null)
                {
                    MessageBox.Show("Не найдено свойство с именем 'cdeId' в базe.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (list.Count == 0)
                {
                    MessageBox.Show("Не удалось загрузить объекты из библиотеки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Добавляем параметр 10830 для каждого объекта
                foreach (var obj in list)
                {

                    _library.AddParameterToObject(obj, param.midParamDef);
                    _library.SetObjectParameter(obj.UID, "cdeId", obj.idObject.ToString(), null);
                }

                MessageBox.Show($"Параметр CdeId успешно добавлен к {list.Count} объектам!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении параметров: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
