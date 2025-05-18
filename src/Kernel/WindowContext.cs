using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public class WindowContext
    {
        public IDatabaseInitializer DatabaseInitializer { get; set; }
        public int? IdObject { get; set; }
        public string ConnectionString { get; set; }
        public IDefectManager DefectManager { get; set; }

        // Добавьляем дополнительные свойства для будущих окон
        // public object AdditionalData { get; set; }
    }
}
