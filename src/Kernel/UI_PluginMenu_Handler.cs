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
        public void Function_Handler_Hello()
        {
            //MessageBox.Show("Hello, CADLib!");
            //var g = new Guid("1DDB2EB8-4331-4E1A-99D9-20925016AE1E");
            //CLibObjectInfo obj = CommonData.m_library.GetLibraryObject(g);
            
            //List<CLibObjectInfo> list = CommonData.m_mainDBBrowser.GetSelectedObjects(false);
            //list.Add(obj);
            //CommonData.m_library.GetParamDefId("Description_Defect_2");
            //CommonData.m_library.GetParamDef(10830);
            //CommonData.m_library.AddParameterToObject(list.First(), 10830);
            //CommonData.m_library.SetObjectName(list, "Колонна К14 К14 - 23 upd 2");
            //CADLibKernel.ObjectData obj2 = CommonData.m_library.GetObject("1DDB2EB8-4331-4E1A-99D9-20925016AE1E",0);
            //CommonData.m_library.UpdateObjectName(obj, "Колонна К14 К14 - 23 upd 3");
            //CommonData.m_library.GetObjectParameters(list.First().idObject);
            //CommonData.m_mainDBBrowser.UpdateWindow();
            //CommonData.m_library.UpdateObjectNames(list);
            //CommonData.m_mainForm.Update();
            //string connect = CommonData.m_library.GetConnectionString();
        }

        public void Function_Handler_About()
        {
            var ass_info = Assembly.GetExecutingAssembly().GetName();
            MessageBox.Show("Версия плагина: " + ass_info.Version.ToString());
        }

        public void Function_Handler_Settings()
        {
            string connectionString = CommonData.m_library?.GetConnectionStringSQL()
                ?? throw new InvalidOperationException("Строка подключения не доступна.");
            var dbInitializer = new DatabaseInitializer(connectionString);
            var context = new WindowContext
            {
                DatabaseInitializer = new DatabaseInitializer(connectionString)
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
    }
}
