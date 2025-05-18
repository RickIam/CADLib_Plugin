using CADLib_Plugin_Kernel;
using CADLibKernel;
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
        public void OpenWindow(string windowName, WindowContext context)
        {
            var window = WindowFactory.CreateWindow(windowName, context);
            window.Show();
        }
    }
}