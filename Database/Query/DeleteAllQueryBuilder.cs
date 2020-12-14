using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Database.Query
{
    class DeleteAllQueryBuilder : IQueryBuilder
    {

        private const string BASE_QUERY = "DELETE FROM {0} WHERE 1;";
        private string table;

        public DeleteAllQueryBuilder()
        {
            ResetBuilder();
        }

        public string Query
        {
            get {

                var result = string.Format(BASE_QUERY, table);
                ResetBuilder();
                return result;
            }
        }

        private void ResetBuilder()
        {
            table = default;
        }

        public void AddItem(Item item) { }
        
        public void AddItem(IEnumerable<Item> item) { }

        public void SetColumns(IEnumerable<string> columnNames) { }

        public void SetTable(string tableName)
        {
            table = tableName;
        }

    }    
}
