using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Database
{
    interface IDatabase
    {
        IList<Models.Item> GetData();
        void SaveData(IList<Models.Item> items);
    }
}
