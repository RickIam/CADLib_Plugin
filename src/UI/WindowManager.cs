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
        public void OpenWindow(string windowName)
        {
            switch (windowName.ToLower())
            {
                case "settings":
                    new SettingsWindow().Show();
                    break;
                default:
                    throw new ArgumentException($"Неизвестное окно: {windowName}");
            }
        }
    }
}