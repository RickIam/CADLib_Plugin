using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public interface IDefectManager
    {
        DataTable GetDefectsByObject(int idObject);
        Defect GetDefectById(int defectId);
        void AddDefect(Defect defect);
        void UpdateDefect(Defect defect);
        void DeleteDefect(int defectId);
        byte[] GetDocument(int defectId);
        byte[] GetPhoto(int defectId);
    }
}
