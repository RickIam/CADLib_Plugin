﻿using CADLib;
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
            var windowManager = new WindowManager();
            _handler = new UI_PluginMenu_Handler(manager, windowManager);
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
            tracker.Add(new InterfaceItemState(settingsToolStripMenuItem, LibConnectionState.Connected, LibFolderState.DoesNotMatter, LibObjectState.DoesNotMatter, LibRequiredPermission.EditParametersRegistry));
            tracker.Add(new InterfaceItemState(defectsToolStripMenuItem, LibConnectionState.Connected, LibFolderState.DoesNotMatter, LibObjectState.SelectedObject, LibRequiredPermission.EditParametersRegistry));
            tracker.Add(new InterfaceItemState(inspectionsToolStripMenuItem, LibConnectionState.Connected, LibFolderState.DoesNotMatter, LibObjectState.DoesNotMatter, LibRequiredPermission.EditParametersRegistry));
            //Кнопка aboutToolStripMenuItem будет доступа в любом случае
        }
        #endregion

        private void newPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _handler.Function_Handler_Settings();
        }

        private void defectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _handler.Function_Handler_Defects();
        }

        private void inspectionsMenuItem_Click(object sender, EventArgs e)
        {
            _handler.Function_Handler_Inspections();
        }
    }
}
