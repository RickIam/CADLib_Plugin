using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public class Inspection
    {
        public int Id { get; set; }
        public string InspectionDate { get; set; } // Дата экспертизы
        public string InspectorName { get; set; } // Имя инспектора
    }
}