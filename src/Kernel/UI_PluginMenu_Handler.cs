using CADLib;
using CADLibKernel;
using CADLibKernel.ObjectUpdate;
using CSProject3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_Kernel
{
    public class UI_PluginMenu_Handler
    {
        private readonly IWindowManager _windowManager;
        public UI_PluginMenu_Handler(PluginsManager pluginsManager, IWindowManager windowManager)
        {
            CommonData.m_library = pluginsManager.Library;
            CommonData.m_mainForm = pluginsManager.MainForm;
            CommonData.m_mainDBBrowser = pluginsManager.MainDBBrowser;
            _windowManager = windowManager;
        }
        #region Обработчики нажатия на кнопки
        #endregion

        public void Function_Handler_About()
        {
            MessageBox.Show("Плагин разработан для загрузки эксплуатационной информации модели. Для начала работы откройте вкладку настройки и проверьте наличие необходимых таблиц, при отсутствии добавьте через интерфейс плагина. Для работы плагина необходимо открыть модель. Для загрузки дефекта, должен быть выбран один объект модели. Для создания экспертизы нужно выбрать объекты, дефекты которых, будут добавлены в экспертизу.");
        }

        public void Function_Handler_Settings()
        {
            string connectionString = CommonData.m_library?.GetConnectionStringSQL()
                ?? throw new InvalidOperationException("Строка подключения не доступна.");
            var dbInitializer = new DatabaseInitializer(connectionString);
            var context = new WindowContext
            {
                DatabaseInitializer = new DatabaseInitializer(connectionString),
                m_library = CommonData.m_library
            };
            _windowManager.OpenWindow("settings", context);
        }

        public void Function_Handler_Defects()
        {
            string connectionString = CommonData.m_library?.GetConnectionStringSQL()
                ?? throw new InvalidOperationException("Строка подключения не доступна.");
            var defectManager = new DefectManager(connectionString);
            List<CLibObjectInfo> list = CommonData.m_mainDBBrowser.GetSelectedObjects(false);
            if(list.Count == 0)
            {
                MessageBox.Show("Необходимо выбрать один элемент");
                return;
            }
            if (list.Count > 1)
            {
                MessageBox.Show("Необходимо выбрать один элемент");
                return;
            }
            var selectedIdObject = list.First().idObject;
            var context = new WindowContext
            {
                IdObject = selectedIdObject,
                ConnectionString = connectionString,
                DefectManager = defectManager
            };
            _windowManager.OpenWindow("defects", context);
        }

        public void Function_Handler_Inspections()
        {
            string connectionString = CommonData.m_library?.GetConnectionStringSQL()
                            ?? throw new InvalidOperationException("Строка подключения не доступна.");
            var inspectionManager = new InspectionManager(connectionString);
            var defectManager = new DefectManager(connectionString);

            var context = new WindowContext
            {
                ConnectionString = connectionString,
                InspectionManager = inspectionManager,
                DefectManager = defectManager,
                MainDBBrowser = CommonData.m_mainDBBrowser // Прямая передача m_mainDBBrowser
            };
            _windowManager.OpenWindow("inspections", context);
        }
    }
}
