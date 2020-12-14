using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMOTestProject.Models;

namespace PMOTestProject.Database.Query
{
    interface IQueryBuilder
    {
        string Query { get; }

        void AddItem(Item item);

        void AddItem(IEnumerable<Item> item);

        void SetColumns(IEnumerable<string> columns);

        void SetTable(string tableName);
    }
}
