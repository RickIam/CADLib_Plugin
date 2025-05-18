using CADLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CADLib_Plugin_Kernel;

namespace CADLib_Plugin_UI
{
    public partial class UI_PluginMenu : Form, ICADLibPlugin
    {
        private UI_PluginMenu_Handler _handler;
        public UI_PluginMenu()
        {
            InitializeComponent();
        }

        public UI_PluginMenu(PluginsManager manager)
        {
            InitializeComponent();
            _handler = new UI_PluginMenu_Handler(manager);
        }

        private void helloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _handler.Function_Handler_Hello();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _handler.Function_Handler_About();
        }
        #region Реализация интерфейса ICADLibPlugin плагина -- для определения стартовой формы
        public MenuStrip GetMenu()
        {
            return this.menuStrip1;
        }

        public ToolStripContainer GetToolbars()
        {
            return null;
        }

        public void TrackInterfaceItems(InterfaceTracker tracker)
        {
            //Помечаем helloToolStripMenuItem как запускаемую только в момент работы с моделью
            tracker.Add(new InterfaceItemState(helloToolStripMenuItem, LibConnectionState.Connected, LibFolderState.DoesNotMatter, LibObjectState.AnySelected, LibRequiredPermission.EditParametersRegistry));

            //Кнопка aboutToolStripMenuItem будет доступа в любом случае
        }
        #endregion

        private void newPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
