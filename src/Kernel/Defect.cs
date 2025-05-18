using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public class Defect
    {
        public int Id { get; set; }
        public int IdObject { get; set; }
        public int DefectNumber { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string DangerCategory { get; set; }
        public byte[] Document { get; set; }
        public byte[] Photo { get; set; }
        public string Recommendation { get; set; }
        public int? InspectionId { get; set; }
    }
}
