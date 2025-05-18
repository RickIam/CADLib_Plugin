using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public interface IDatabaseInitializer
    {
        void CheckTablesExist(out bool defectsExists, out bool inspectionsExists);
        void CreateTables();
    }
}