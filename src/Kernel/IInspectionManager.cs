using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public interface IInspectionManager
    {
        DataTable GetAllInspections();
        Inspection GetInspectionById(int id);
        void AddInspection(Inspection inspection);
        void UpdateInspection(Inspection inspection);
        void DeleteInspection(int id);
    }
}