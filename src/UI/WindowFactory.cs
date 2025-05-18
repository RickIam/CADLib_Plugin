using CADLib_Plugin_Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADLib_Plugin_UI
{
    public class WindowFactory
    {
        public static Form CreateWindow(string windowName, WindowContext context)
        {
            switch (windowName.ToLower())
            {
                case "settings":
                    if (context?.DatabaseInitializer == null)
                        throw new ArgumentNullException(nameof(context.DatabaseInitializer), "Для окна настроек требуется IDatabaseInitializer.");
                    return new SettingsWindow(context.DatabaseInitializer);

                case "defects":
                    if (!context?.IdObject.HasValue ?? true)
                        throw new ArgumentException("Для окна дефектов требуется IdObject.");
                    if (context?.DefectManager == null)
                        throw new ArgumentNullException(nameof(context.DefectManager), "Для окна дефектов требуется IDefectManager.");
                    return new DefectsWindow(context.DefectManager, context.IdObject.Value);

                default:
                    throw new ArgumentException($"Неизвестное окно: {windowName}");
            }
        }
    }
}
