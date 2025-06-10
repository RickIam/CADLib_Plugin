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
        // Словарь для хранения открытых окон
        private readonly Dictionary<string, Form> _openWindows = new Dictionary<string, Form>(StringComparer.OrdinalIgnoreCase);

        public void OpenWindow(string windowName, WindowContext context)
        {
            // Проверяем, существует ли окно и не закрыто ли оно
            if (_openWindows.TryGetValue(windowName, out Form existingWindow) && !existingWindow.IsDisposed)
            {
                // Если окно свернуто, восстанавливаем его
                if (existingWindow.WindowState == FormWindowState.Minimized)
                {
                    existingWindow.WindowState = FormWindowState.Normal;
                }
                // Активируем окно
                existingWindow.Activate();
                return;
            }

            // Создаем новое окно
            var window = WindowFactory.CreateWindow(windowName, context);

            // Подписываемся на событие закрытия окна, чтобы удалить его из словаря
            window.FormClosed += (sender, e) =>
            {
                _openWindows.Remove(windowName);
            };

            // Добавляем окно в словарь
            _openWindows[windowName] = window;

            window.Show();
        }
    }
}