using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_Kernel
{
    public class WindowOpener : IWindowOpener
    {
        public void OpenSettingsWindow(CreateSettingsWindowDelegate createSettingsWindow)
        {
            Form settingsWindow = createSettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}