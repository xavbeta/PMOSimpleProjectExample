using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Database.Query
{
    class SelectAllQueryBuilder : IQueryBuilder
    {

        private const string BASE_QUERY = "SELECT {0} FROM {1} WHERE 1;";
        private string table;
        private string columns;

        public SelectAllQueryBuilder()
        {
            ResetBuilder();
        }

        public string Query
        {
            get {

                var result = string.Format(BASE_QUERY, columns, table);
                ResetBuilder();
                return result;
            }
        }

        private void ResetBuilder()
        {
            columns = default;
            table = default;
        }

        public void AddItem(Item item) { }
        
        public void AddItem(IEnumerable<Item> item) { }

        public void SetColumns(IEnumerable<string> columnNames) 
        {
            columns = string.Join(",", columnNames);
        }

        public void SetTable(string tableName)
        {
            table = tableName;
        }

    }    
}
