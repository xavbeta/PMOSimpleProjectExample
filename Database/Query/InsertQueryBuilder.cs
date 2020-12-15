using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Database.Query
{
    class InsertQueryBuilder : IQueryBuilder
    {

        private const string BASE_QUERY = "INSERT INTO {0} ({1}) VALUES {2}";
        private IList<Item> items;
        private string table;
        private string columns;

        public InsertQueryBuilder()
        {
            ResetBuilder();
        }

        public string Query
        {
            get {

                var result = string.Format(BASE_QUERY, table, columns, ItemsToSQLValues());
                ResetBuilder();
                return result;
            }
        }

        private void ResetBuilder()
        {
            items = new List<Item>();
            columns = default;
            table = default;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void AddItem(IEnumerable<Item> item)
        {
            foreach(var i in item)
                items.Add(i);
        }

        public void SetColumns(IEnumerable<string> columnNames)
        {
            columns = string.Join(",", columnNames);
        }

        public void SetTable(string tableName)
        {
            table = tableName;
        }

        private string ItemsToSQLValues()
        {
            return string.Join(", ", items.Select(i => i.SQLRepresentation()));
        }

    }

    static class ItemExtensions
    {
        public static string SQLRepresentation(this Item item)
        {

            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Item must have a name.");

            List<string> fields = new List<string>
        {
            item.Name,
            item.Description ?? "NULL",
            item.Price.ToString() ?? "NULL",
            item.Quantity.ToString() ?? "NULL",
            (item.Picture ?? "NULL").ToDB(),
        };

            return $"('{string.Join("','", fields)}')";

        }
    }
    
}
