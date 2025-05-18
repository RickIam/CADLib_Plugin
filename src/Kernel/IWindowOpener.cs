using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_Kernel
{
    public interface IWindowOpener
    {
        void OpenSettingsWindow(CreateSettingsWindowDelegate createSettingsWindow);
    }

    public delegate Form CreateSettingsWindowDelegate();
}