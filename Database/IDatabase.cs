using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Database
{
    public interface IDatabase
    {
        Task<IList<Models.Item>> GetData();
        Task SaveData(IList<Models.Item> items);
    }
}
