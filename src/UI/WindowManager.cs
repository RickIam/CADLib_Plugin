using CADLib_Plugin_Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_UI
{
    public class WindowManager : IWindowManager
    {
        public void OpenWindow(string windowName, IDatabaseInitializer dbInitializer)
        {
            switch (windowName.ToLower())
            {
                case "settings":
                    if (dbInitializer == null)
                        throw new ArgumentNullException(nameof(dbInitializer), "Для окна настроек требуется IDatabaseInitializer.");
                    new SettingsWindow(dbInitializer).Show();
                    break;
                default:
                    throw new ArgumentException($"Неизвестное окно: {windowName}");
            }
        }
    }
}